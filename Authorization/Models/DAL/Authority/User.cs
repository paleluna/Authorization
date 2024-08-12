using System;
using System.Collections.Generic;

namespace Authorization.Models.DAL.Authority;

public partial class User
{
    public int UserId { get; set; }

    public string UserLogin { get; set; } = null!;

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<RolesUsersApp> RolesUsersApps { get; set; } = new List<RolesUsersApp>();

    public virtual Employe UserLoginNavigationEmploye { get; set; } = null!;
}
