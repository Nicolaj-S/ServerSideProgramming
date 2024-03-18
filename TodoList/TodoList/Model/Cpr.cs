using System;
using System.Collections.Generic;

namespace TodoList.Model;

public partial class Cpr
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string Cpr1 { get; set; } = null!;
}
