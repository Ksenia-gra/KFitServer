using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class TypeOfMeal
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<UsersNutritionStatistic> UsersNutritionStatistics { get; set; } = new List<UsersNutritionStatistic>();
}
