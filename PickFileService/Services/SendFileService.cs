using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace PickFileService.Services;

public class SendFileService : ISendFileService
{
    private readonly IConfiguration _configuration;

    public SendFileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void CheckFolderAndSendFile(string filePath, string emailSubject)
    {
        // Check if the file exists before trying to send it
        
        var emailTo = _configuration["EmailConfiguration:To"];
        var files = Directory.GetFiles(filePath);

        if (files.Length > 0)
        {
            var fileToSend = files[0]; // Pick the first file
            if (emailTo != null)
            {
                
                SendEmailWithAttachment(emailTo, emailSubject, "Please find the attached file.", fileToSend);
                File.Delete(fileToSend);
            }
            else
            {
                Console.WriteLine("No email address found to send the file to. The program will continue running.");
            }
        }
        else
        {
            Console.WriteLine($"No file found to send for {emailSubject}. The program will continue running.");
        }
    }

    public void SendEmailWithAttachment(string to, string subject, string body, string filePath)
    {
        try
        {
            var from = _configuration["EmailConfiguration:From"];
            var smtpServer = _configuration["EmailConfiguration:SmtpServer"];
            var port = _configuration["EmailConfiguration:Port"];
            var smtpUsername = _configuration["EmailConfiguration:SmtpUsername"];
            var smtpPassword = _configuration["EmailConfiguration:SmtpPassword"];

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
        
            var builder = new BodyBuilder { TextBody = body };
            builder.Attachments.Add(filePath);
            email.Body = builder.ToMessageBody();
            using var smtpClient = new SmtpClient();
        
            smtpClient.Connect(smtpServer, int.Parse(port), SecureSocketOptions.StartTls);
            smtpClient.Authenticate(smtpUsername, smtpPassword);
        
            smtpClient.Send(email);
            smtpClient.Disconnect(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}