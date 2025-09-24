using Backend.Models;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LandscapesController : ControllerBase
{
    private readonly ILandscapeRepository _repo;
    private readonly ILogger<LandscapesController> _logger;

    public LandscapesController(ILandscapeRepository repo, ILogger<LandscapesController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Landscape>>> GetAll(CancellationToken ct)
    {
        var items = await _repo.GetAllAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Landscape>> GetById(long id, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    public record CreateLandscapeRequest(string Name, string? Description);
    public record UpdateLandscapeRequest(string Name, string? Description);

    [HttpPost]
    public async Task<ActionResult<Landscape>> Create([FromBody] CreateLandscapeRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Name))
            return ValidationProblem("Name is required");

        var id = await _repo.CreateAsync(new Landscape
        {
            Name = req.Name.Trim(),
            Description = string.IsNullOrWhiteSpace(req.Description) ? null : req.Description.Trim(),
            CreatedAt = DateTime.UtcNow
        }, ct);

        var created = await _repo.GetByIdAsync(id, ct);
        return CreatedAtAction(nameof(GetById), new { id }, created);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateLandscapeRequest req, CancellationToken ct)
    {
        var existing = await _repo.GetByIdAsync(id, ct);
        if (existing is null) return NotFound();

        existing.Name = req.Name.Trim();
        existing.Description = string.IsNullOrWhiteSpace(req.Description) ? null : req.Description.Trim();

        var ok = await _repo.UpdateAsync(existing, ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        var ok = await _repo.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}

