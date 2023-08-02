﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Delamain_backend.Models
{
	public class Login
	{
		[Key]
		public string Email { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; } = new byte[32];
		public byte[] PasswordSalt { get; set; } = new byte[32];

		public string? VerificationToken { get; set; }
		public DateTime? VerifieAt { get; set; }
		public string? PasswordResetToken { get; set; }
		public DateTime? ResetTokenExpires { get; set; }
	}
}

