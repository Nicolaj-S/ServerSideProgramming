using System;
using System.Collections.Generic;

namespace TodoList.Model;

public partial class ToDo
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string ItemName { get; set; } = null!;
}
