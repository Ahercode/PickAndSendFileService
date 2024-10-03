using PickFileService;
using PickFileService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ISendFileService>(provider =>
            new SendFileService(new List<(string FolderPath, TimeSpan Time)>
            {
                ("/path/to/folder1", new TimeSpan(8, 0, 0)), // 8:00 AM
                ("/path/to/folder2", new TimeSpan(12, 0, 0)), // 12:00 PM
                ("/path/to/folder3", new TimeSpan(16, 0, 0)), // 4:00 PM
                ("/path/to/folder4", new TimeSpan(20, 0, 0))  // 8:00 PM
            }, "recipient@example.com"));
    })
    .Build();

host.Run();