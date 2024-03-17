using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class ProductsInNutrition
{
    public string ProductId { get; set; }

    public int NutritionId { get; set; }

    public int? ProductCount { get; set; }

    public int? MealId { get; set; }

    public virtual TypeOfMeal Meal { get; set; }

    public virtual UsersNutrition Nutrition { get; set; }

    public virtual Product Product { get; set; }
}
