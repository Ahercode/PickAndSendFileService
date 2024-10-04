namespace PickFileService.Services;

public interface ISendFileService
{
    // void SendEmailWithAttachment(string to, string subject, string body, string filePath);
    void CheckFolderAndSendFile(string filePath, string emailSubject);
}