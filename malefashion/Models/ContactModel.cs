using System.ComponentModel.DataAnnotations;

namespace malefashion.Models
{
	public class ContactModel
	{
		[Required(ErrorMessage = "Обьязательно заполните поле")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Обьязательно заполните поле email")]
		[EmailAddress(ErrorMessage = "Указан некорректный email")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Обьязательно заполните поле message")]
		public string Message { get; set; }
	}
}


