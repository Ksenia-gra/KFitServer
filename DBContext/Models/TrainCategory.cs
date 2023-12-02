using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class TrainCategory
{
    public int Id { get; set; }

    public string? TrainCategoryName { get; set; }

    public virtual ICollection<TrainingMetadatum> TrainingMetadata { get; set; } = new List<TrainingMetadatum>();
}
