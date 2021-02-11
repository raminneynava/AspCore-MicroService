namespace Micro.Common.Events
{
    public class UserAuthenticate:IEvent
    {
        public string Email { get; set; }

        public UserAuthenticate()
        {
            
        }
        public UserAuthenticate(string email)
        {
            Email=email;
        }
    }
}