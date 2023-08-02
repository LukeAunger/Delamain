using System;
namespace Delamain_backend.Services.QueueWorkerInterface
{
	public interface IQueueService
	{
        Task<string> Queuemethod();
    }
}

