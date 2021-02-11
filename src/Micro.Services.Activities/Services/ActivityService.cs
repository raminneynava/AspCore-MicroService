using Micro.Common.Exeptions;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(category);

            if (activityCategory == null)
            {
                throw new MicroExeption("category_not_found",
                    $"Category {category} was not found .");
            }

            var activity = new Activity(id, activityCategory, userId,
                name, description, createAt);
            await _activityRepository.AddAsync(activity);


        }
    }
}
