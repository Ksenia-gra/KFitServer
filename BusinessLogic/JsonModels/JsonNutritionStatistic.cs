namespace KFitServer.JsonModels
{
    public record class JsonNutritionStatistic (DateOnly? NutritionDate, string ProductId, string TypeOfMealName,int? CaloriesCount,decimal? Water,string ProductName,
        int? proteins,int? lipids,int? carbohidrates);
}
