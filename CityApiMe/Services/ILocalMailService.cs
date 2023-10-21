namespace CityApiMe
{
	public interface ILocalMailService
	{
		void Send(string subject, string message);
	}
}