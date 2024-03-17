using KFitServer.BusinessLogic.Helpers;
using KFitServer.BusinessLogic.Repository;
using KFitServer.DBContext.Models;

namespace KFitServer.BusinessLogic.Services
{
	public class AuthentificationService
	{
		private readonly IHashCreator hashCreator;
		private readonly IDbRepository dbRepository;

		public AuthentificationService(IHashCreator hashCreator,IDbRepository dbRepository)
        {
			this.hashCreator = hashCreator;
			this.dbRepository = dbRepository;
		}

		public async Task<User> AuthorizeAsync(string email, string password)
		{
			User user = await dbRepository.UserRepository.FindByEmailAsync(email).ConfigureAwait(false);
			if (user != null &&
				user.PasswordHash.Equals(hashCreator.Hash(password, user.Salt)))
			{
				return user;
			}

			return null;
		}

		public async Task<User> AuthorizeByTokenAsync(string token)
		{
			User user = await dbRepository.UserRepository.FindByTokenAsync(token).ConfigureAwait(false);
			if (user != null)
			{
				return user;
			}

			return null;
		}

		public async Task<User> RegisterAsync(string login, string email, string password)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login, nameof(login));
			ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
			ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));

			User user = new User(login, email, password);
			if (!await dbRepository.UserRepository.AddAsync(user).ConfigureAwait(false))
			{
				return null;
			}

			await dbRepository.UserRepository.SaveChangesAsync().ConfigureAwait(false);
			user = await dbRepository.UserRepository.FindByEmailAsync(user.Email).ConfigureAwait(false);
			return user;
		}
	}
}
