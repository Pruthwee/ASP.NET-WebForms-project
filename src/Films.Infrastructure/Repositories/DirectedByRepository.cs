using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Films.Domain.Entities;
using Films.Domain.Interfaces.Repositories;
using Films.Infrastructure.Data;

namespace Films.Infrastructure.Repositories;

public class DirectedByRepository : IDirectedByRepository
{
    private readonly FilmsDbContext _context;
    private readonly ILogger<DirectedByRepository> _logger;

    public DirectedByRepository(FilmsDbContext context, ILogger<DirectedByRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DirectedBy>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Directors
                .Where(d => d.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all directors");
            throw;
        }
    }

    public async Task<DirectedBy?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Directors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving director with ID {DirectorId}", id);
            throw;
        }
    }

    public async Task<DirectedBy> AddAsync(DirectedBy directedBy, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Directors.Add(directedBy);
            await _context.SaveChangesAsync(cancellationToken);
            return directedBy;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding director");
            throw;
        }
    }

    public async Task UpdateAsync(DirectedBy directedBy, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Directors.Update(directedBy);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating director with ID {DirectorId}", directedBy.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var director = await _context.Directors.FindAsync(new object[] { id }, cancellationToken);
            if (director != null)
            {
                director.IsActive = false;
                director.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting director with ID {DirectorId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Directors.AnyAsync(d => d.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if director exists with ID {DirectorId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<DirectedBy>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Directors
                .Where(d => d.IsActive && d.Name.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching directors with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
