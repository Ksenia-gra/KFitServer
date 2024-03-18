using KFitServer.BusinessLogic.DBContext.Models;

namespace KFitServer.BusinessLogic.Helpers
{
	public interface IJwtGenerator
	{
		string CreateToken(User user);
	}
}