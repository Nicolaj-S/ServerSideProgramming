using System.ComponentModel.DataAnnotations;

namespace TodoList.Model;

public partial class ToDo
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; }

    public string ItemName { get; set; } = null!;
}
