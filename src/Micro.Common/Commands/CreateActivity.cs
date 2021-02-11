using System;

namespace Micro.Common.Commands
{
    public class CreateActivity : IAuthenticatedCommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
         public string Name { get; set; }
        public string Category { get; set; }
        public string Desceription { get; set; }
        public DateTime CreateAt { get; set; }

    }
}