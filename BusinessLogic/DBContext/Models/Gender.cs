using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class Gender
{
    public string GenderName { get; set; }

    public string Id { get; set; }

    public virtual ICollection<UsersParameter> UsersParameters { get; set; } = new List<UsersParameter>();
}
