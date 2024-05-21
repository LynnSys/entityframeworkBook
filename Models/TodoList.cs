using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;

public partial class TodoList
{
    public int MapId { get; set; }

    public int? EmpId { get; set; }

    public int? ProjId { get; set; }

    public virtual Employee1? Emp { get; set; }

    public virtual Assignment? Proj { get; set; }
}
