using PickFileService;
using PickFileService.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ISendFileService, SendFileService>();
    })
    .Build();

host.Run();