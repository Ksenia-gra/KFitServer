using KFitServer.BusinessLogic.Repository;
using KFitServer.BusinessLogic.DBContext.Models;

namespace KFitServer.BusinessLogic.Services
{
	public class NutritionService
	{
		private const string productsAPIProductInfoURL = "https://api.edamam.com/api/food-database/v2/parser?";
		private const string productsAPISearchingURL = "https://api.edamam.com/auto-complete?";
		private const string appIdForProductsAPI = "4686b325";
		private const string productsAPIKeyValue = "d4121399676eb88f5b4826f17c570a1e";

		private readonly IDbRepository dbRepository;
		public NutritionService(IDbRepository dbRepository)
        {
			this.dbRepository = dbRepository;
		}

		public async Task<bool> AddNutritionStatisticAsync(string token,List<UsersNutrition> usersNutritions)
		{
			
			if(usersNutritions != null && string.IsNullOrEmpty(token))
			{
				return false;
			}

			foreach (UsersNutrition userstat in usersNutritions)
			{
				userstat.UserId = (await dbRepository.UserRepository.FindByTokenAsync(token).ConfigureAwait(false)).Id;
				if (!await dbRepository.NutritionStatisticRepository.AddAsync(userstat))
				{
					return false;
				}
			}
			await dbRepository.NutritionStatisticRepository.SaveChangesAsync();
			return true;

		}

	}
}
