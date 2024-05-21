using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee1> Employee1s { get; set; } = new List<Employee1>();
}
