using PickFileService.Services;

namespace PickFileService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISendFileService _sendFileService;

    public Worker(ILogger<Worker> logger, ISendFileService sendFileService)
    {
        _logger = logger;
        _sendFileService = sendFileService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        // File paths for each time slot
        var fileToSendAt10Am = "/Users/philip/Desktop/TestFileUpload1/";
        var fileToSendAt1220Pm = "/Users/philip/Desktop/TestFileUpload2/";
        var fileToSendAt530Pm = "/Users/philip/Desktop/TestFileUpload3/";
        var fileToSendAt7Pm = "/Users/philip/Desktop/TestFileUpload4/";

        while (!stoppingToken.IsCancellationRequested)
        {
            var currentTime = DateTime.Now.TimeOfDay;
            
            //
            //
            // // Check if it's time to send the file at 10:00 AM
            // if (currentTime.Hours == 18 && currentTime.Minutes == 32)
            // {
            //     _sendFileService.CheckFolderAndSendFile(fileToSendAt10Am, "VAG Data");
            // }
            // // Check if it's time to send the file at 12:20 PM
            // else if (currentTime.Hours == 18 && currentTime.Minutes == 34)
            // {
            //     _sendFileService.CheckFolderAndSendFile(fileToSendAt1220Pm, "Noon Data");
            // }
            // // Check if it's time to send the file at 5:30 PM
            // else if (currentTime.Hours == 18 && currentTime.Minutes == 36)
            // {
            //    
            //     _sendFileService.CheckFolderAndSendFile(fileToSendAt530Pm, "Daywa Data");
            // }
            // // Check if it's time to send the file at 7:00 PM
            // else if (currentTime.Hours == 18 && currentTime.Minutes == 38)
            // {
            //  
            //     _sendFileService.CheckFolderAndSendFile(fileToSendAt7Pm, "Original Data");
            // }
            
            _sendFileService.CheckFolderAndSendFile(fileToSendAt10Am, "VAG Data");

            _logger.LogInformation("Current Hour: {time}", currentTime.Hours);
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            // Delay for 1 minute to avoid multiple checks within the same minute
            await Task.Delay(1000, stoppingToken);
        }
    }
}