using System;
using System.Collections.Generic;

namespace Authorization.Models.DAL.Authority;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public int AppId { get; set; }

    public string? RoleDescription { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual ICollection<RolesUsersApp> RolesUsersApps { get; set; } = new List<RolesUsersApp>();
}
