using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Scheduled.Quartz.TimedTask
{
    public class MailQueueJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {

            return Task.CompletedTask;
        }
    }
}
