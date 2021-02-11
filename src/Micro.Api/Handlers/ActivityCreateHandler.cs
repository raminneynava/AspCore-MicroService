using System;
using System.Threading.Tasks;
using Micro.Api.Models;
using Micro.Api.Repositories;
using Micro.Common.Events;

namespace Micro.Api.Handlers
{
    public class ActivityCreateHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreateHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity()
            {
                Id= @event.Id,
                UserId= @event.UserId,
                Name= @event.Name,
                Category= @event.Category,
                CreateAt= @event.CreateAt,
                Description= @event.Desceription
            });
            Console.WriteLine($"Activity Createed: {@event.Name}");
        }
    }
}