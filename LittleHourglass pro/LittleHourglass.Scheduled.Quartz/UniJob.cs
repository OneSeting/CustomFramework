using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Scheduled.Quartz
{
    public class UniJob
    {
        #region Fields
        /// <summary>
        /// 任务名称，必填
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 触发器名称，必填
        /// </summary>
        public string TriggerName { get; set; }

        /// <summary>
        /// 触发器分组
        /// </summary>
        public string TriggerGroup { get; set; }

        public long NextFireTime { get; set; }

        public long PrevFireTime { get; set; }

        /// <summary>
        /// 触发器类型，必填。！建议使用 cron触发器，simple触发器只执行一次
        /// </summary>
        public TriggerTypeEnum TriggerType { get; set; }

        /// <summary>
        /// 触发器类型为 simple 时有效
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// 触发器类型为 cron 时有效
        /// </summary>
        public string CronExpression { get; set; }

        public DateTime NextFireDateTime => new DateTime(NextFireTime);
        public DateTime PrevFireDateTime => new DateTime(PrevFireTime);

        /// <summary>
        /// 构造 jobKey
        /// </summary>
        public JobKey GetJobKey => JobKey.Create(JobName, JobGroup);

        /// <summary>
        /// 构造 jobKey
        /// </summary>
        public TriggerKey TriggerKey => new TriggerKey(TriggerName, TriggerGroup);

        #region Constructors
        public UniJob() { }
        #endregion

        #region Utilities
        #endregion

        #region Methods
        #endregion
        #endregion

        public enum TriggerTypeEnum
        {
            Simple,
            Cron
        }
    }
}
