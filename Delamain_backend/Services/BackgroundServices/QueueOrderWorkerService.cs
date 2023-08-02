using System.Security.Cryptography;
using Delamain_backend.Services.QueueWorkerInterface;

namespace Delamain_backend.Services;
//Background services to allow the server to run async while this works neets to inherit backgroundService
// background service also needs to run in the ExecuteAsync because if I run in the startAsync that is a Ihosted
// method which will not allow the server to run whilst that is active.
public class BackgroundWorkerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BackgroundWorkerService(IServiceProvider context)
    {
        _serviceProvider = context;
    }


    //When creating ran into issue where server would not startup after building this whole method put it down to method not being async whilst i was using struct objects
    //Modified the code to use awaits making it an asyncronous method which means it will allow the server to load whilst this runs in the background.
    protected override async Task ExecuteAsync(CancellationToken stoptoken)
    {
        while (!stoptoken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IQueueService queueService =
                    scope.ServiceProvider.GetRequiredService<IQueueService>();

                await queueService.Queuemethod();
            }
            await Task.Delay(1000, stoptoken);
        }
    }
}