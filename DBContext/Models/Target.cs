using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class Target
{
    public int Id { get; set; }

    public string TargetName { get; set; } = null!;

    public virtual ICollection<UsersPersonalParameter> UsersPersonalParameters { get; set; } = new List<UsersPersonalParameter>();
}
