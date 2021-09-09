using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LittleHourglass.Scheduled.Quartz
{
    public sealed class ScheduledManager
    {
        /// <summary>
        /// 定时调度底层方法 参考连接：https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html#trying-out-the-application-and-adding-jobs
        /// </summary>

        #region Fields
        static NameValueCollection nameValueCollection = new NameValueCollection();
        static readonly object _lockObj = new object();

        /// <summary>
        /// 保证内存中变量唯一，内部调用，暂不予开放
        /// </summary>
        static IScheduler _scheduler;
        static IScheduler Scheduler
        {
            get
            {
                var scheduler = _scheduler;
                if (scheduler == null)
                {
                    lock (_lockObj)
                    {
                        scheduler = Volatile.Read(ref _scheduler);
                        if (scheduler == null)
                        {
                            scheduler = GetScheduler().Result;
                            Volatile.Write(ref _scheduler, scheduler);
                        }
                    }
                }
                return scheduler;
            }
        }
        #endregion

        //读取配置文件
        //static IEnumerable<IConfigurationSection> configurationSections= AppConfigurtaionServices.Configuration.GetSection("SchedulerConfig").GetChildren();

        //private ScheduledManager() { }

        /// <summary>
        /// 构造并生成 IScheduler
        /// </summary>
        static async Task<IScheduler> GetScheduler()
        {
            //Quartz.Logging.LogProvider.SetCurrentLogProvider(new Unibone.Scheduler.Log.LogProvider());

            //foreach (var item in collection)
            //{
            //    configCollection.Add($"[{item.Key}]", $"{item.Value}");
            //}

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            //添加scheduler、trigge、job监听事件
            //scheduler.ListenerManager.AddSchedulerListener(new SchedulerListener());
            //scheduler.ListenerManager.AddTriggerListener(new TriggerListener());
            //scheduler.ListenerManager.AddJobListener(new JobListener());

            return scheduler;
        }

        #region Methods

        /// <summary>
        /// 手动启动调用服务
        /// </summary>
        /// <returns></returns>
        public static async Task Start()
        {
            await Scheduler.Start();
        }

        /// <summary>
        /// 让任务开始跑
        /// </summary>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        public static async Task<SchedulerResponse> RunJob(IJobDetail job, ITrigger trigger)
        {
            var response = new SchedulerResponse();
            try
            {
                //看当前定时任务的job对应的key 在不在队列里
                var isExist = await Scheduler.CheckExists(job.Key);
                var time = DateTimeOffset.Now;
                if (isExist)
                {
                    //恢复已存在的任务
                    await Scheduler.ResumeJob(job.Key);
                }
                else
                {
                    //添加给定的Quartz。IJobDetail到调度器，并关联给定的Quartz。ITrigger
                    time = await Scheduler.ScheduleJob(job, trigger);
                }
                response.IsSuccess = true;
                response.Msg = time.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                response.Msg = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static async Task<SchedulerResponse> UpdateJob(JobKey jobKey, ITrigger newtrigger)
        {
            var response = new SchedulerResponse();
            try
            {
                var isExist = await Scheduler.CheckExists(jobKey);
                if (isExist)
                {
                    //得到所有石英。与标识的Quartz.IJobDetail相关联的触发器。
                    var triger = (await Scheduler.GetTriggersOfJob(jobKey)).FirstOrDefault();
                    if (triger != null)
                    {
                        //删除石英。用给定键触发，并存储新的给定键
                        // one -它必须与相同的作业相关联(新触发器必须具有
                        //指定作业名称和组)——但是，新触发器不需要相同的
                        //指定旧触发器的名称。
                        await Scheduler.RescheduleJob(triger.Key, newtrigger);
                    }
                }
                response.IsSuccess = true;
                response.Msg = "更新成功！！";
            }
            catch (Exception ex)
            {
                response.Msg = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 重启任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static async Task<SchedulerResponse> ResumeJob(JobKey jobKey)
        {
            var response = new SchedulerResponse();
            try
            {
                var isExist = await Scheduler.CheckExists(jobKey);
                if (isExist)
                {
                    //恢复(不暂停)石英。IJobDetail与给定的键。
                    await Scheduler.ResumeJob(jobKey);
                }
                response.IsSuccess = true;
                response.Msg = "重启成功！！";
            }
            catch (Exception ex)
            {
                response.Msg = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static async Task<SchedulerResponse> StopJob(JobKey jobKey)
        {
            var response = new SchedulerResponse();
            try
            {
                var isExist = await Scheduler.CheckExists(jobKey);
                if (isExist)
                {
                    //暂停石英。IJobDetail与给定的键-通过暂停它的所有当前
                    //Quartz.ITrigger
                    await Scheduler.PauseJob(jobKey);
                }
                response.IsSuccess = true;
                response.Msg = "暂停成功！！";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Msg = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static async Task<(bool, string)> DelJob(JobKey jobKey)
        {
            var response = new SchedulerResponse();
            try
            {
                var isExist = await Scheduler.CheckExists(jobKey);
                if (isExist)
                {
                    //删除任务
                    response.IsSuccess = await Scheduler.DeleteJob(jobKey);
                    response.Msg = "删除成功";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Msg = ex.Message;
            }
            return (response.IsSuccess, response.Msg);
        }

        public static async Task<UniJob> GetJob(string jobName, string jobGroup) => await GetJob(JobKey.Create(jobName, jobGroup));

        public static async Task<UniJob> GetJob(JobKey jobKey)
        {
            UniJob model = new UniJob();
            var trigger = (await Scheduler.GetTriggersOfJob(jobKey)).FirstOrDefault();
            var prevTime = trigger.GetPreviousFireTimeUtc().GetValueOrDefault().UtcTicks;
            var nextTime = trigger.GetNextFireTimeUtc().GetValueOrDefault().UtcTicks;

            model.NextFireTime = nextTime;
            model.PrevFireTime = prevTime;
            model.TriggerName = trigger.Key.Name;
            model.TriggerGroup = trigger.Key.Group;
            model.JobName = trigger.JobKey.Name;
            model.JobGroup = trigger.JobKey.Group;

            return model;
        }

        public static IScheduler UseScheduler() => Scheduler;
        #endregion

        public class SchedulerResponse
        {
            public bool IsSuccess { get; set; }
            public string Msg { get; set; }
        }
    }
}
