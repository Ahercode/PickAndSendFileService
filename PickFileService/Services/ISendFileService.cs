namespace PickFileService.Services;

public interface ISendFileService
{
    Task SendFileAsync(string filePath);
}