using System.Collections.Generic;
using System.Threading.Tasks;
using TECAIS.StatusReporting.Models;

namespace TECAIS.StatusReporting.Repositories
{
    public interface IStatusRepository
    {
        Task SaveStatusMessageAsync(StatusReportMessage statusReportMessage);
        Task SaveStatusMessageBatchAsync(IEnumerable<StatusReportMessage> statusReportMessages);
    }
}