using System;
using System.ComponentModel.DataAnnotations;

namespace Delamain_backend.Models
{
	public class ResetPasswordRequest
	{
        [Required]

        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter 6 characters or more")]

        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

