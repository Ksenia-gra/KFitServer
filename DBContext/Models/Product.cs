using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class Product
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonProperty(PropertyName ="name")]
    public string? ProductName { get; set; }

    [JsonProperty(PropertyName = "calories")]
    public decimal? ProductCalories { get; set; }

    [JsonProperty(PropertyName = "fat_total_g")]
    public decimal? ProductFats { get; set; }

    [JsonProperty(PropertyName = "protein_g")]
    public decimal? ProductProtein { get; set; }

    [JsonProperty(PropertyName = "carbohydrates_total_g")]
    public decimal? ProductCarbohydrates { get; set; }

    [JsonProperty(PropertyName = "serving_size_g")]
    public decimal? ProductDefaultG { get; set; }

    [JsonIgnore]
    public virtual ICollection<UsersNutritionStatistic> UsersNutritionStatistics { get; set; } = new List<UsersNutritionStatistic>();
}
