using System;
namespace Micro.Common.Events
{
    public interface IAuthenticatedEvent:IEvent
    {
         Guid UserId {get;}
    }
}