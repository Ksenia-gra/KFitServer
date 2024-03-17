using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class WeekDay
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<YoutubeVideo> YoutubeVideos { get; set; } = new List<YoutubeVideo>();
}
