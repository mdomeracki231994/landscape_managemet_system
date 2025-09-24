using Backend.Models;

namespace Backend.Repositories;

public interface ILandscapeRepository
{
    Task<IEnumerable<Landscape>> GetAllAsync(CancellationToken ct = default);
    Task<Landscape?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<long> CreateAsync(Landscape landscape, CancellationToken ct = default);
    Task<bool> UpdateAsync(Landscape landscape, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}

