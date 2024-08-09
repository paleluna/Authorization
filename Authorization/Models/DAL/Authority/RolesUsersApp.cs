using System;
using System.Collections.Generic;

namespace Authorization.Models.DAL.Authority;

public partial class RolesUsersApp
{
    public int ReleId { get; set; }

    public int UserId { get; set; }

    public int AppId { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Role Rele { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
