﻿using System;
using System.Collections.Generic;

namespace Repository.Model;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
