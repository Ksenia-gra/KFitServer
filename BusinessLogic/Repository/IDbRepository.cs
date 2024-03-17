namespace KFitServer.BusinessLogic.Repository
{
	public interface IDbRepository
	{
		public IUserRepository UserRepository { get; }
		public INutritionRepository NutritionStatisticRepository { get; }
	}
}
