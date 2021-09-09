using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static LittleHourglass.Scheduled.Quartz.UniJob;

namespace LittleHourglass.Scheduled.Quartz
{
    public class QuartzService : IQuartzService
    {
        /// <summary>
        /// 添加job、trigger参数需在添加任务之前
        /// </summary>
        public static Dictionary<string, object> jobDetail_Collection = new Dictionary<string, object>();
        public static Dictionary<string, object> trigger_collection = new Dictionary<string, object>();

        public async Task<(bool, string)> AddJobAsync<T>(UniJob model) where T : IJob
        {
            var job = JobBuilder.Create<T>().WithIdentity(model.JobName, model.JobGroup).Build();
            var trigger = BuildTrigger(model);

            var jobPars = jobDetail_Collection.GetEnumerator();
            var triggerPar = trigger_collection.GetEnumerator();

            while (jobPars.MoveNext())
            {
                var current = jobPars.Current;
                //如果有key就全部删掉
                if (!string.IsNullOrEmpty(job.JobDataMap.GetString(current.Key)))
                {
                    //获取或设置与Quartz.IJob关联的Quartz.IJobDetail.JobDataMap。
                    job.JobDataMap.Remove(current.Key);
                }
                //再重新加入
                job.JobDataMap.Add($"{current.Key}", $"{current.Value}");
            }
            while (triggerPar.MoveNext())
            {
                // 获取枚举数当前位置的元素。
                var current = triggerPar.Current;
                // 如果有key就全部删掉
                if (!string.IsNullOrEmpty(trigger.JobDataMap.GetString(current.Key)))
                {
                    //获取或设置与Quartz.IJob关联的Quartz.IJobDetail.JobDataMap。
                    trigger.JobDataMap.Remove(current.Key);
                }
                //再重新加入
                trigger.JobDataMap.Add($"{current.Key}", $"{current.Value}");
            }
            var res = await ScheduledManager.RunJob(job, trigger);
            return (res.IsSuccess, res.Msg);
        }

        public async Task<bool> DelJobAsync(UniJob model)
        {
            var (isSuccess, msg) = await ScheduledManager.DelJob(model.GetJobKey);
            return isSuccess;
        }

        public async Task<bool> StopJobAsync(UniJob model)
        {
            var res = await ScheduledManager.StopJob(model.GetJobKey);
            return res.IsSuccess;
        }

        public async Task<(bool, string)> UpdateJobTriggerAsync<T>(UniJob model)
        {
            var newTrigger = BuildTrigger(model);
            var res = await ScheduledManager.UpdateJob(model.GetJobKey, newTrigger);
            return (res.IsSuccess, res.Msg);
        }

        public ITrigger BuildTrigger(UniJob model)
        {
            TriggerBuilder builder = TriggerBuilder.Create().WithIdentity(model.TriggerName, model.TriggerGroup);
            ITrigger trigger = null;
            if (model.TriggerType == TriggerTypeEnum.Simple)
            {
                //设置时间扳机应该开始 - 触发器可能或不可能开火
                //这一次-取决于为触发器配置的调度。然而,
                //触发器在此之前不会触发，无论触发器的时间表。
                trigger = builder.StartAt(DateBuilder.FutureDate(model.Seconds, IntervalUnit.Second)).Build();
            }
            else
            {
                trigger = builder.WithCronSchedule(model.CronExpression).StartNow().Build();
            }
            return trigger;
        }

    }
}
