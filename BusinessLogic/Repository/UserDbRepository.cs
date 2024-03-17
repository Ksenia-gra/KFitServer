using KFitServer.BusinessLogic.Helpers;
using KFitServer.DBContext;
using KFitServer.DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace KFitServer.BusinessLogic.Repository
{
	public class UserDbRepository : IUserRepository
	{
		private readonly ILogger<UserDbRepository> logger;
		private readonly KfitContext kfitContext;
		private readonly IHashCreator hashCreator;
		public UserDbRepository(ILogger<UserDbRepository> logger, KfitContext kfitContext, IHashCreator hashCreator)
		{
			this.logger = logger;
			this.kfitContext = kfitContext;
			this.hashCreator = hashCreator;
		}
		public async Task<bool> AddAsync(User item)
		{
			try
			{
				item.Salt = hashCreator.GenerateSalt();
				item.PasswordHash = hashCreator.Hash(item.PasswordHash, item.Salt);

				if (await kfitContext.Users.AnyAsync(x => x.Id == item.Id || x.Email.Equals(item.Email)))
				{
					return false;
				}

				await kfitContext.Users.AddAsync(item);
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
				User u = await kfitContext.Users.FindAsync(id);
				if (u != null)
				{
					kfitContext.Users.Remove(u);
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

		public async Task<User> FindByEmailAsync(string email)
		{
			try
			{
				return await kfitContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return default;
			}
		}

		public async Task<User> FindByIdAsync(int id)
		{
			try
			{
				return await kfitContext.Users.FirstOrDefaultAsync(x => x.Id == id);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return default;
			}
		}

		public async Task<User> FindByTokenAsync(string token)
		{
			try
			{
				return await kfitContext.Users.FirstOrDefaultAsync(x => x.AuthToken==token);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, null, null);
				return default;
			}
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			try
			{
				return await kfitContext.Users.ToListAsync();
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

		public async Task<bool> UpdateAsync(User item)
		{
			try
			{
				User changedUser = await kfitContext.Users.FirstOrDefaultAsync(x => x.Id == item.Id);
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
