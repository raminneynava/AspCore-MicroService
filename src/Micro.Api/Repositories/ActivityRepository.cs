using Micro.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity)
        {
             await collection.InsertOneAsync(activity);
        }

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        {
            return await collection.AsQueryable()
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }


        private IMongoCollection<Activity> collection => _database.GetCollection<Activity>("Activities");
    }
}
