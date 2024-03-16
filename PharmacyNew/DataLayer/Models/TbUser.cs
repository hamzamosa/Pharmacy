using System;
using System.Collections.Generic;

namespace DataLayer.Models;

public partial class TbUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
