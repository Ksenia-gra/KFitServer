using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class YoutubeVideo
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string ThumbnailUrl { get; set; }

    public int? WeekDay { get; set; }

    public virtual ICollection<UserTrainingStatistic> UserTrainingStatistics { get; set; } = new List<UserTrainingStatistic>();

    public virtual WeekDay WeekDayNavigation { get; set; }
}
