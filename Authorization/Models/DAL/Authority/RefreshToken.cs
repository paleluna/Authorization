using System;
using System.Collections.Generic;

namespace Authorization.Models.DAL.Authority;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expiration { get; set; }

    public DateTime DataCreated { get; set; }

    public DateTime? DataRevoked { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsExpired { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
