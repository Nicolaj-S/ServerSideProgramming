using System.ComponentModel.DataAnnotations;

namespace TodoList.Model;

public partial class Cpr
{
    [Key]
    public string Id { get; set; }

    public string User { get; set; } = null!;

    public string? Cpr1 { get; set; }
}
