using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class UsersNutritionStatistic
{
    public int Id { get; set; }

    public DateOnly? NutritionDate { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public int? TypeOfMealId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual TypeOfMeal? TypeOfMeal { get; set; }

    public virtual UsersPersonalParameter? User { get; set; }
}
