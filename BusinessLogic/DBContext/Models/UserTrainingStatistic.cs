using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class UserTrainingStatistic
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateOnly? TrainingDate { get; set; }

    public string TrainingId { get; set; }

    public virtual YoutubeVideo Training { get; set; }
}
