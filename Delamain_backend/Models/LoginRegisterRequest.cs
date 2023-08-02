using System;
using System.ComponentModel.DataAnnotations;

namespace Delamain_backend.Models
{
	public class LoginRegisterRequest
	{
		[Required, EmailAddress]

		public string Email { get; set; } = string.Empty;
		[Required,MinLength(6)]

		public string Password { get; set; } = string.Empty;
		[Required, Compare("Password")]

		public string ConfirmPassword { get; set; } = string.Empty;
	}
}

