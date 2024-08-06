using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Dtos.Account
{
	public class RegisterAccountDto
	{
		[Required]
		[EmailAddress]
		public string? EmailAddress { get; set; }

		[Required]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters")]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
