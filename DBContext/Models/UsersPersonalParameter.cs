using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class UsersPersonalParameter
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonIgnore]
    public string UserToken { get; set; } = null!;

    [JsonProperty(PropertyName = "gender")]
    public string? UserGender { get; set; }

    [JsonProperty(PropertyName = "dateOfBirth")]
    public DateOnly? UserDateOfBirth { get; set; }

    [JsonProperty(PropertyName = "height")]
    public decimal? UserHeight { get; set; }

    [JsonProperty(PropertyName = "target")]
    public int? UserTarget { get; set; }

    [JsonIgnore]
    public virtual Gender? UserGenderNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserPersonalStatistic> UserPersonalStatistics { get; set; } = new List<UserPersonalStatistic>();

    [JsonIgnore]
    public virtual Target? UserTargetNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserTrainingStatistic> UserTrainingStatistics { get; set; } = new List<UserTrainingStatistic>();

    [JsonIgnore]
    public virtual ICollection<UsersNutritionStatistic> UsersNutritionStatistics { get; set; } = new List<UsersNutritionStatistic>();
}
