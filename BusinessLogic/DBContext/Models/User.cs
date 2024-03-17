using System;
using System.Collections.Generic;

namespace KFitServer.BusinessLogic.DBContext.Models;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }

    public string AuthToken { get; set; }

    public virtual ICollection<UsersNutrition> UsersNutritions { get; set; } = new List<UsersNutrition>();

    public virtual ICollection<UsersParameter> UsersParameters { get; set; } = new List<UsersParameter>();
}
