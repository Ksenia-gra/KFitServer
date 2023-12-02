using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class UserPersonalStatistic
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "statisticDate")]
    public DateOnly? RecordDate { get; set; }

    [JsonIgnore]
    public int? UserId { get; set; }

    [JsonProperty(PropertyName = "userWeight")]
    public decimal? UserWeight { get; set; }

    public decimal? UserChestGirth { get; set; }

    public decimal? UserHipGirth { get; set; }

    public decimal? UserWaistCircumference { get; set; }

    [JsonIgnore]
    public virtual UsersPersonalParameter? User { get; set; }
}
