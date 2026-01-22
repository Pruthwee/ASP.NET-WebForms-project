using Films.Domain.DTOs;

namespace Films.Domain.Interfaces.Services;

public interface IDirectedByService
{
    Task<IEnumerable<DirectedByDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DirectedByDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<DirectedByDto> CreateAsync(DirectedByCreateDto createDto, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, DirectedByUpdateDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DirectedByDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
}
