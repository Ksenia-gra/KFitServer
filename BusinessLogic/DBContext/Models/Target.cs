using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class Target
{
    public int Id { get; set; }

    public string TargetName { get; set; }

    public virtual ICollection<UsersParameter> UsersParameters { get; set; } = new List<UsersParameter>();
}
