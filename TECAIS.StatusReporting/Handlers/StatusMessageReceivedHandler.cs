using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.StatusReporting.Data;
using TECAIS.StatusReporting.Models;
using TECAIS.StatusReporting.Repositories;
using Status = TECAIS.StatusReporting.Data.Status;

namespace TECAIS.StatusReporting.Handlers
{
    public class StatusMessageReceivedHandler : IEventHandler<StatusReportMessage>
    {
        private readonly StatusDbContext _dbContext;

        public StatusMessageReceivedHandler(StatusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(StatusReportMessage @event)
        {
            var statusEntity = new Status
            {
                CurrentAmount = @event.CurrentAmount,
                DeviceId = @event.DeviceId,
                DeviceStatus = @event.Status.ToString(),
                HouseId = @event.HouseId
            };
            await _dbContext.Statuses.AddAsync(statusEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}