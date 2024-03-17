using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class Product
{
    public string Id { get; set; }

    public int Calories { get; set; }

    public int Proteins { get; set; }

    public int Lipids { get; set; }

    public int Carbohydrates { get; set; }

    public string ProductName { get; set; }

    public string ImageUrl { get; set; }

    public virtual ICollection<ProductsInNutrition> ProductsInNutritions { get; set; } = new List<ProductsInNutrition>();
}
