using System;

namespace MonstraCorporatePortal.Models;

public class Collaborator
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    
    public int PositionId { get; set; }
    public Position? Position { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    
    public int? WorkFormatId { get; set; } 
    public WorkFormat? WorkFormat { get; set; }

    public int OrganizationId { get; set; }
    public Organization? Organization { get; set; }
    
    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public string Login { get; set; } = string.Empty;
    public string LoginLowercase { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? MobilePhone { get; set; }
    public string? InternalPhone { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}