using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Scheduled.Quartz
{
    public interface IQuartzService
    {
        Task<(bool, string)> AddJobAsync<T>(UniJob model) where T : IJob;

        Task<(bool, string)> UpdateJobTriggerAsync<T>(UniJob model);

        Task<bool> StopJobAsync(UniJob model);

        Task<bool> DelJobAsync(UniJob model);

        ITrigger BuildTrigger(UniJob model);
    }
}
