using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class UserPersonalStatistic
{
    public int Id { get; set; }

    public DateOnly? RecordDate { get; set; }

    public int? UserId { get; set; }

    public decimal? UserWeight { get; set; }

    public decimal? UserChestGirth { get; set; }

    public decimal? UserHipGirth { get; set; }

    public decimal? UserWaistCircumference { get; set; }

    public int? CaloriesNorm { get; set; }

    public int? Proteins { get; set; }

    public int? Lipids { get; set; }

    public int? Carbohydrates { get; set; }

    public decimal? WaterNorm { get; set; }
}
