using System;
namespace Micro.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public Guid Id { get; set; }
        public string Reason { get; }

        public string Code { get; }

        public CreateActivityRejected()
        {

        }
        public CreateActivityRejected(Guid id, string reason, string code)
        {
            Reason = reason;
            Id = id;
            Code = code;
        }
    }
}