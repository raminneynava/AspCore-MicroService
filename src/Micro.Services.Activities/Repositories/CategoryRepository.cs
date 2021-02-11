using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repository;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Micro.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private IMongoDatabase _database;
        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Name == name.ToLowerInvariant());
        public async Task<IEnumerable<Category>> BrowseAsync()
            => await Collection.AsQueryable().ToListAsync();

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        private IMongoCollection<Category> Collection =>
            _database.GetCollection<Category>("Categories");
    }
}
