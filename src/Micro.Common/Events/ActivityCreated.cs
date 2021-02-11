using System;

namespace Micro.Common.Events
{
    public class ActivityCreated : IAuthenticatedEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Desceription { get; set; }
        public DateTime CreateAt { get; set; }
        public ActivityCreated()
        {

        }
        public ActivityCreated(Guid id, Guid userid, string category, string name, string description, DateTime createAt)
        {
            this.Id = id;
            this.UserId = userid;
            this.Category = category;
            this.Name = name;
            this.Desceription = description;
            this.CreateAt = createAt;
        }
    }
}