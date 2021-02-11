using System.Threading.Tasks;
using Micro.Common.Commands;
using RawRabbit;
using System;
using Micro.Common.Events;
using Micro.Common.Exeptions;
using Micro.Services.Activities.Domain.Repository;
using Micro.Services.Activities.Services;
using Microsoft.Extensions.Logging;

namespace Micro.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;
        public CreateActivityHandler(IBusClient busClient, IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }
          public async Task HandleAsync(CreateActivity command)
        {
           _logger.LogInformation($"Creating Activity : '{command.Id}' for user: '{command.UserId}'");

           try
           {
               await _activityService.AddAsync(command.Id, command.UserId,
                   command.Category, command.Name, command.Desceription, command.CreateAt);
               await _busClient.PublishAsync(new ActivityCreated(command.Id,
                   command.UserId,
                   command.Category,
                   command.Name,
                   command.Desceription,
                   command.CreateAt
               ));
               _logger.LogInformation($"Activity {command.Id} was create for user {command.UserId}");
               return;
           }
           catch (MicroExeption ex)
           {
               _logger.LogError(ex,ex.Message);
               await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, ex.Code));
           }
           catch (Exception ex)
           {
               _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, "error"));
           }
        }
        
    }
}