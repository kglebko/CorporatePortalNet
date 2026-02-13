using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonstraCorporatePortal.Data;
using MonstraCorporatePortal.DTO;
using MonstraCorporatePortal.Models;

namespace MonstraCorporatePortal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CollaboratorsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Collaborators
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var collaborators = await _context.Collaborators
            .Include(c => c.Position)
            .Include(c => c.Department)
            .Include(c => c.WorkFormat)
            .Include(c => c.Organization)
            .Include(c => c.Role)
            .Select(c => new CollaboratorDto
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                Position = c.Position!.Name,
                Department = c.Department!.Name,
                WorkFormat = c.WorkFormat != null ? c.WorkFormat.Name : null,
                Organization = c.Organization!.Name,
                Role = c.Role!.Name,
                Login = c.Login,
                Email = c.Email,
                MobilePhone = c.MobilePhone,
                InternalPhone = c.InternalPhone,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();

        return Ok(collaborators);
    }

    // GET: api/Collaborators/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var collaborator = await _context.Collaborators
            .Include(c => c.Position)
            .Include(c => c.Department)
            .Include(c => c.WorkFormat)
            .Include(c => c.Organization)
            .Include(c => c.Role)
            .Where(c => c.Id == id)
            .Select(c => new CollaboratorDto
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                Position = c.Position!.Name,
                Department = c.Department!.Name,
                WorkFormat = c.WorkFormat != null ? c.WorkFormat.Name : null,
                Organization = c.Organization!.Name,
                Role = c.Role!.Name,
                Login = c.Login,
                Email = c.Email,
                MobilePhone = c.MobilePhone,
                InternalPhone = c.InternalPhone,
                CreatedAt = c.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (collaborator == null)
            return NotFound();

        return Ok(collaborator);
    }

    // POST: api/Collaborators
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCollaboratorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var collaborator = new Collaborator
        {
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            BirthDate = dto.BirthDate,
            PositionId = dto.PositionId,
            DepartmentId = dto.DepartmentId,
            WorkFormatId = dto.WorkFormatId,
            OrganizationId = dto.OrganizationId,
            RoleId = dto.RoleId,
            Login = dto.Login,
            LoginLowercase = dto.Login.ToLower(),
            Email = dto.Email,
            MobilePhone = dto.MobilePhone,
            InternalPhone = dto.InternalPhone,
            CreatedAt = DateTime.UtcNow
        };

        _context.Collaborators.Add(collaborator);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = collaborator.Id },
            new CollaboratorDto
            {
                Id = collaborator.Id,
                FullName = collaborator.FullName,
                BirthDate = collaborator.BirthDate,
                Position = (await _context.Positions.FindAsync(collaborator.PositionId))!.Name,
                Department = (await _context.Departments.FindAsync(collaborator.DepartmentId))!.Name,
                WorkFormat = collaborator.WorkFormatId != null
                    ? (await _context.WorkFormats.FindAsync(collaborator.WorkFormatId))?.Name
                    : null,
                Organization = (await _context.Organizations.FindAsync(collaborator.OrganizationId))!.Name,
                Role = (await _context.Roles.FindAsync(collaborator.RoleId))!.Name,
                Login = collaborator.Login,
                Email = collaborator.Email,
                MobilePhone = collaborator.MobilePhone,
                InternalPhone = collaborator.InternalPhone,
                CreatedAt = collaborator.CreatedAt
            });
    }
}
