namespace CompareX.Jobs
{
    public interface IEmailSender
    {
        void Send(string emailAddress1, string emailAddress2, string subject, string body);
    }

}
