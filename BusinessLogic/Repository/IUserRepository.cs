using KFitServer.BusinessLogic.DBContext.Models;

namespace KFitServer.BusinessLogic.Repository
{
	public interface IUserRepository
	{
		Task<bool> AddAsync(User item);
		Task<bool> DeleteAsync(int id);
		Task<User> FindByIdAsync(int id);
		Task<User> FindByEmailAsync(string email);
		Task<User> FindByTokenAsync(string token);
		Task<IEnumerable<User>> GetAllAsync();
		Task<bool> SaveChangesAsync();
		Task<bool> UpdateAsync(User item);
	}
}
