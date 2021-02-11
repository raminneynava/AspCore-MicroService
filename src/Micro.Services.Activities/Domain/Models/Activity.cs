using Micro.Common.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Category { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreateAt { get; protected set; }

        public Activity()
        {

        }
        public Activity(Guid id, Category category, Guid userId,
            string name, string description, DateTime createAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new MicroExeption("empty_activity_name",
             "Activity Name Can Not Be Empty");
            }

            Id = id;
            Category = category.Name;
            UserId = userId;
            Name = name;
            Description = description;
            CreateAt = createAt;
        }
    }
}
