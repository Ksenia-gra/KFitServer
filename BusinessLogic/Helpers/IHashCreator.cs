namespace KFitServer.BusinessLogic.Helpers
{
	public interface IHashCreator
	{
		string GenerateSalt();
		string Hash(string data, string salt);
		string Hash(string data);
	}
}
