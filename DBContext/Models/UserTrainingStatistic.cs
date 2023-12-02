using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class UserTrainingStatistic
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateOnly? TrainingDate { get; set; }

    public string? TrainingId { get; set; }

    public virtual TrainingMetadatum? Training { get; set; }

    public virtual UsersPersonalParameter? User { get; set; }
}
