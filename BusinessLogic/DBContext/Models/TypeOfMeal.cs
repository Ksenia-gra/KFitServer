using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class TypeOfMeal
{
    public int Id { get; set; }

    public string TypeName { get; set; }

    public virtual ICollection<ProductsInNutrition> ProductsInNutritions { get; set; } = new List<ProductsInNutrition>();
}
