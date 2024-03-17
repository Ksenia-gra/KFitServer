using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class UsersNutrition
{
    public int Id { get; set; }

    public DateOnly? NutritionDate { get; set; }

    public int? UserId { get; set; }

    public decimal? Water { get; set; }

    public virtual ICollection<ProductsInNutrition> ProductsInNutritions { get; set; } = new List<ProductsInNutrition>();

    public virtual User User { get; set; }
}
