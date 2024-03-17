using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class UsersParameter
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public decimal? Height { get; set; }

    public int? Target { get; set; }

    public decimal? TargetWeight { get; set; }

    public decimal? WaterNorm { get; set; }

    public int? CaloriesNorm { get; set; }

    public int? ProteinsNorm { get; set; }

    public int? LipidsNorm { get; set; }

    public int? CarbohydratesNorm { get; set; }

    public virtual Gender GenderNavigation { get; set; }

    public virtual Target TargetNavigation { get; set; }

    public virtual User User { get; set; }
}
