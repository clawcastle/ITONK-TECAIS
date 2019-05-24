using System.Collections.Generic;
using System.Threading.Tasks;
using TECAIS.StatusReporting.Models;

namespace TECAIS.StatusReporting.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        public async Task SaveStatusMessageAsync(StatusReportMessage statusReportMessage)
        {
            //Need to setup db connection for this.
            await Task.CompletedTask;
        }

        public async Task SaveStatusMessageBatchAsync(IEnumerable<StatusReportMessage> statusReportMessages)
        {
            //Need to setup db connection for this.
            await Task.CompletedTask;
        }
    }
}