using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Micro.Services.Activities.Repositories
{
    public class ActivityRepository: IActivityRepository
    {

        private IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task DeleteAsync(Guid id)
            => await Collection.DeleteOneAsync(x => x.Id == id);


        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");
    }
}
