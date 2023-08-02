using System;
namespace Delamain_backend.Services.LoginService
{
	public interface ILoginService
	{
        Task<string> Register(LoginRegisterRequest request);
        Task<string> Login(LoginRequest request);
        Task<string> Verify(string token);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(ResetPasswordRequest request);
    }
}

