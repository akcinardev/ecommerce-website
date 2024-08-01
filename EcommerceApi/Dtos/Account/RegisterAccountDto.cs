using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Dtos.Account
{
	public class RegisterAccountDto
	{
		[Required]
		[EmailAddress]
		public string? EmailAddress { get; set; }

		[Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
