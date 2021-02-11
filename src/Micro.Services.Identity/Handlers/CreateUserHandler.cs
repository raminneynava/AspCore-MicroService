using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Common.Commands;
using Micro.Common.Events;
using Micro.Common.Exeptions;
using Micro.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Micro.Services.Identity.Handlers
{
    public class CreateUserHandler: ICommandHandler<CreateUser>
    {
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public CreateUserHandler(ILogger<CreateUserHandler> logger, IBusClient busClient, IUserService userService)
        {
            _logger = logger;
            _busClient = busClient;
            _userService = userService;
        }
        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating User : '{command.Email}' with name : '{command.Name}'");
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                _logger.LogInformation($"User '{command.Email}' was created with name : '{command.Name}'");
                return;
            }
            catch (MicroExeption ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserReject(command.Email, ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await _busClient.PublishAsync(new CreateUserReject(command.Email, ex.Message, "Error"));
            }
        }
    }
}
