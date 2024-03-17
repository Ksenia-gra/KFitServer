using KFitServer.BusinessLogic.Helpers;
using KFitServer.BusinessLogic.DBContext;
using KFitServer.BusinessLogic.DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace KFitServer.BusinessLogic.Repository
{
	public class NutritionDbRepository : INutritionRepository
	{
		private readonly ILogger<UserDbRepository> logger;
		private readonly KfitContext kfitContext;
		public NutritionDbRepository(ILogger<UserDbRepository> logger, KfitContext kfitContext)
		{
			this.logger = logger;
			this.kfitContext = kfitContext;
		}
		public async Task<bool> AddAsync(UsersNutrition item)
		{
			try
			{
				if (await kfitContext.UsersNutritions.AnyAsync(x => x.Id == item.Id))
				{
					return false;
				}

				await kfitContext.UsersNutritions.AddAsync(item);
				return true;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return false;
			}
		}

		public async Task<bool> DeleteAsync(int id)
		{
			try
			{
				UsersNutrition nutritionStatistic = await kfitContext.UsersNutritions.FindAsync(id);
				if (nutritionStatistic != null)
				{
					kfitContext.UsersNutritions.Remove(nutritionStatistic);
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return false;
			}
		}

		public async Task<UsersNutrition> FindByIdAsync(int id)
		{
			try
			{
				return await kfitContext.UsersNutritions.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return default;
			}
		}

		public async Task<IEnumerable<UsersNutrition>> GetAllAsync()
		{
			try
			{
				return await kfitContext.UsersNutritions.ToListAsync();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return default;
			}
		}

		public async Task<bool> SaveChangesAsync()
		{
			try
			{
				await kfitContext.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return false;
			}
		}

		public async Task<bool> UpdateAsync(UsersNutrition item)
		{
			try
			{
				UsersNutrition changedUser = await kfitContext.UsersNutritions.FirstOrDefaultAsync(x => x.Id == item.Id);
				if (changedUser != null)
				{
					System.Reflection.PropertyInfo[] props = changedUser.GetType().GetProperties();
					foreach (System.Reflection.PropertyInfo prop in props)
					{
						if (prop.Name.Equals(nameof(User.Id))) continue;
						prop.SetValue(changedUser, prop.GetValue(item));
					}

					await kfitContext.SaveChangesAsync();
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return false;
			}
		}
	}
}
