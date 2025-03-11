namespace malefashion_master.Models.Interfaces
{
	public interface IMessage
	{
		public bool SendMessage(string to, string messageBody, string subject);
	}
}
