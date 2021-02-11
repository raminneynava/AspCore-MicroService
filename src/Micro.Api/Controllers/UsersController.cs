using System.Threading.Tasks;
using Micro.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Micro.Api.Controllers
{

    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusClient _busClient;

        public UsersController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
         {
            await _busClient.PublishAsync(command);
            return Accepted();
        }
    }
}