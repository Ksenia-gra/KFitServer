using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class TrainingMetadatum
{
    public string Id { get; set; } = null!;

    public string? Titlle { get; set; }

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public int? TrainCategoryId { get; set; }

    public virtual TrainCategory? TrainCategory { get; set; }

    public virtual ICollection<UserTrainingStatistic> UserTrainingStatistics { get; set; } = new List<UserTrainingStatistic>();
}
