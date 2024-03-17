using KFitServer.BusinessLogic.DBContext.Models;

namespace KFitServer.BusinessLogic.Repository
{
	public interface INutritionRepository
	{
		Task<bool> AddAsync(UsersNutrition item);
		Task<bool> DeleteAsync(int id);
		Task<UsersNutrition> FindByIdAsync(int id);
		Task<IEnumerable<UsersNutrition>> GetAllAsync();
		Task<bool> SaveChangesAsync();
		Task<bool> UpdateAsync(UsersNutrition item);
	}
}
