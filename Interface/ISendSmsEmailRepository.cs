namespace TP1examuml.Interface
{
    public interface ISendSmsEmailRepository

    {
        Task<string> SendEmailAsync(string email,string subject,string body);
        Task<string> SendSmsAsync(string telephone,string body);
        Task<string> SendSmsAsync(string body);

    }
}
