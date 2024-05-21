using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;

public partial class Employee1
{
    public int EmpId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int? DepartmentId { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? JoinDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();
}
