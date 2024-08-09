using System;
using System.Collections.Generic;

namespace Authorization.Models.DAL.Authority;

public partial class Employe
{
    public string UserLogin { get; set; } = null!;

    public string? EmpName { get; set; }

    public string? EmpSurname { get; set; }

    public string? EmpEmail { get; set; }

    public string? EmpPhone { get; set; }

    public string? RoleName { get; set; }

    public bool? EmpIsBlocked { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
