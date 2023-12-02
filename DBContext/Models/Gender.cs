using System;
using System.Collections.Generic;

namespace KFitServer.DBContext.Models;

public partial class Gender
{
    public string GenderName { get; set; } = null!;

    public string Id { get; set; } = null!;

    public virtual ICollection<UsersPersonalParameter> UsersPersonalParameters { get; set; } = new List<UsersPersonalParameter>();
}
