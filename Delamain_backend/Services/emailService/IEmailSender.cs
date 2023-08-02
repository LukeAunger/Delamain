using System;
namespace Delamain_backend.Services.emailService
{
	public interface IEmailSender
	{
		void SendEmailAsync(string email, string subject, string message);
	}
}

