namespace KFitServer.BusinessLogic.Repository
{
	public class DbRepository : IDbRepository
	{
		public IUserRepository UserRepository {  get; private set; }
		public INutritionRepository NutritionStatisticRepository { get; private set; }

		public DbRepository(INutritionRepository nutritionStatisticRepository, IUserRepository userRepository)
		{
			NutritionStatisticRepository = nutritionStatisticRepository ?? throw new ArgumentNullException(nameof(nutritionStatisticRepository));
			UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}
	}
}
