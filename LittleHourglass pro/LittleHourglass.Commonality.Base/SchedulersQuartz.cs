using LittleHourglass.Scheduled.Quartz;
using LittleHourglass.Scheduled.Quartz.TimedTask;
using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static LittleHourglass.Scheduled.Quartz.UniJob;

namespace LittleHourglass.Commonality.Base
{
    public class SchedulersQuartz
    {
        #region Fields
        private static IQuartzService service = new QuartzService();
        #endregion

        #region Constructors
        private SchedulersQuartz()
        {

        }
        #endregion

        #region Methods
        public static SchedulersQuartz _()
        {
            return new SchedulersQuartz();
        }

        public async Task Initialize()
        {
            //系统启动时把所有的key的定时任务全部拿出来 然后删掉 再重新加载进入
            await ScheduledManager.Start();
            //把所有石英的钥匙都拿来。匹配组中的IJobDetails。 //创建一个匹配所有的GroupMatcher。
            var jonKeys = await ScheduledManager.UseScheduler().GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            foreach (var item in jonKeys)
            {
                await ScheduledManager.DelJob(item);
            }

            await JobConfiguration();
        }

        public async Task JobConfiguration()
        {
            await service.AddJobAsync<MailQueueJob>(new UniJob
            {
                TriggerType = TriggerTypeEnum.Cron,
                CronExpression = "0 0/1  * * * ?",
                JobName = "MailQueueJob",
                TriggerName = "MailQueueJobTrigger",
                //JobGroup = "group1",
                //TriggerGroup = "group1",
            });


        }
        #endregion
    }
}
