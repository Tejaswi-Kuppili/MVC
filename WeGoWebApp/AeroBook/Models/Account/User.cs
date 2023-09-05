using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroBook.Models.Account
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]).{5,}$",
                            ErrorMessage = "Enter a valid Password with at least one lowercase letter, one uppercase letter, one digit, and one special character, with a minimum length of 5 characters.")]
        public string Password { get; set; }

		[Required]
		[NotMapped]
		[Compare("Password", ErrorMessage = "Passwords do not match.")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
