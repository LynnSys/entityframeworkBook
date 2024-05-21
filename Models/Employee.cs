using System;
using System.Collections.Generic;

namespace BookEntityFramework.Models;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int? EmployeeAge { get; set; }

    public int? EmployeeSalary { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public int? Dept { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime? CreatedDate { get; set; }
}
