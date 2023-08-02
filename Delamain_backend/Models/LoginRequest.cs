using System;
using System.ComponentModel.DataAnnotations;

namespace Delamain_backend.Models
{
	public class LoginRequest
	{
        [Required, EmailAddress]

        public string Email { get; set; } = string.Empty;
        [Required]

        public string Password { get; set; } = string.Empty;
    }
}

