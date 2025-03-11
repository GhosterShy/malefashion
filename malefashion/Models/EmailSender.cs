using malefashion_master.Models.Interfaces;
using System.Net.Mail;
using System.Net;

namespace malefashion.Models
{
	public class EmailSender: IMessage
	{
		public bool SendMessage(string to, string messageBody, string subject)
		{
			var fromAddress = new MailAddress("erzhurekshingis2005@gmail.com", "Malefashion");
			var toAddress = new MailAddress(to, "progerShy@gmail.com");
			const string fromPassword = "jgio ibsi bioq mpgc";
	
			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = messageBody
			})
			{
				try
				{
					smtp.Send(message);
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
		}
	}
}
