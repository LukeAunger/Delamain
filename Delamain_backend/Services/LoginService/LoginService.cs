using System;
using System.Security.Cryptography;
using Delamain_backend.Services.emailService;

namespace Delamain_backend.Services.LoginService
{
	public class LoginService : ILoginService
	{
        private readonly DataContext _context;
        private readonly IEmailSender _emailSender;

        public LoginService(DataContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;

        }

        public async Task<string> ForgotPassword(string email)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                return ("User not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            var subject = "Delamain Reset Token.";
            var message = user.PasswordResetToken;
            _emailSender.SendEmailAsync(email, subject, message);

            return ("You may now reset your password");
        }

        public async Task<string> Login(LoginRequest request)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (user == null)
            {
                return ("Login failed");
            }
            if (user.VerifieAt == null)
            {
                return ("Not verified");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return ("Login failed");
            }
            return ($" {user.Email} Logged in");
        }

        public async Task<string> Register(LoginRegisterRequest request)
        {
            if (_context.Logins.Any(u => u.Email.ToLower() == request.Email.ToLower()))
            {
                return ("Error");
            }

            CreatePasswordHash(request.Password,
                out byte[] passwordHash, out byte[] passwordSalt);

            var user = new Login
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            _context.Logins.Add(user);
            await _context.SaveChangesAsync();

            return ("Success");
        }

        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(user => user.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return ("Invalid token.");
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return ("Password changed! ");
        }

        public async Task<string> Verify(string token)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(user => user.VerificationToken == token);
            if (user == null)
            {
                return ("Invalid token");
            }

            user.VerifieAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return ("Varified");
        }

        //encrypting the password with hash and salt and making it so there is not one salt for all the hashes
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}

