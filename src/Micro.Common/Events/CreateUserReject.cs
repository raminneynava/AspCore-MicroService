namespace Micro.Common.Events
{
    public class CreateUserReject : IRejectedEvent
    {
        public string Email { get;}
        public string Reason { get; }

        public string Code { get; }
        public CreateUserReject()
        {
            
        }
        public CreateUserReject(string email,string reason,string code)

        {
         Reason=reason;
         Email=email;   
         Code=code;
        }
    }
}