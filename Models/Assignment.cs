using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;

public partial class Assignment
{
    public string ProjectName { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int ProjId { get; set; }

    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();
}
