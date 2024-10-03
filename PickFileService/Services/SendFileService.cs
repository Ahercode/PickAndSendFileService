namespace PickFileService.Services;

public class SendFileService: ISendFileService
{
    private readonly List<(string FolderPath, TimeSpan Time)> _folderSchedules;
    private readonly string _emailRecipient;
    private readonly List<Timer> _timers;

    public SendFileService(List<(string FolderPath, TimeSpan Time)> folderSchedules, string emailRecipient)
    {
        _folderSchedules = folderSchedules;
        _emailRecipient = emailRecipient;
        _timers = new List<Timer>();

        foreach (var schedule in _folderSchedules)
        {
            var dueTime = GetDueTime(schedule.Time);
            var timer = new Timer(CheckFolderAndSendFile, schedule.FolderPath, dueTime, TimeSpan.FromDays(1));
            _timers.Add(timer);
        }
    }
    
    private TimeSpan GetDueTime(TimeSpan scheduleTime)
    {
        var now = DateTime.Now.TimeOfDay;
        return scheduleTime > now ? scheduleTime - now : scheduleTime + TimeSpan.FromDays(1) - now;
    }

    public async Task SendFileAsync(string filePath)
    {
        Console.WriteLine($"Sending file: {filePath}");
        await Task.CompletedTask;
    }

    private void CheckFolderAndSendFile(object state)
    {
        var folderPath = (string)state;
        var files = Directory.GetFiles(folderPath);
        if (files.Length > 0)
        {
            var fileToSend = files[0]; // Pick the first file
            SendEmailWithAttachment(fileToSend);
        }
    }
    
    private void SendEmailWithAttachment(string filePath)
    {
  
    }
}