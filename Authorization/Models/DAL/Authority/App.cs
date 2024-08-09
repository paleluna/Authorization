using System;
using System.Collections.Generic;

namespace Authorization.Models.DAL.Authority;

public partial class App
{
    public int AppId { get; set; }

    public string AppName { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<RolesUsersApp> RolesUsersApps { get; set; } = new List<RolesUsersApp>();
}
