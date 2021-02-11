using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Micro.Api.Repositories;
using Micro.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Micro.Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ActivityController : Controller
    {
        private readonly IBusClient _busclient;
        private readonly IActivityRepository _activityRepository;
        public ActivityController(IBusClient busclient, IActivityRepository activityRepository)
        {
            _busclient = busclient;
            _activityRepository = activityRepository;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var activities = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));
            return Json(activities.Select(x => new
            {
                x.Id,
                x.Name,
                x.Category,
                x.CreateAt
            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);
            if (activity == null)
                return NotFound();

            if (activity.UserId != Guid.Parse(User.Identity.Name))
                return Unauthorized();
            return Json(activity);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {

            command.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            command.Id = Guid.NewGuid();
            command.CreateAt = DateTime.Now;
            await _busclient.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }
    }
}