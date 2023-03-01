using System;
using System.Collections.Generic;

namespace P10DatabaseFrist.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswrodSalt { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string Role { get; set; } = null!;
}
