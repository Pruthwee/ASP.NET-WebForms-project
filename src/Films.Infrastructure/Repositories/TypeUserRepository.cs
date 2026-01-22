using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Films.Domain.Entities;
using Films.Domain.Interfaces.Repositories;
using Films.Infrastructure.Data;

namespace Films.Infrastructure.Repositories;

public class TypeUserRepository : ITypeUserRepository
{
    private readonly FilmsDbContext _context;
    private readonly ILogger<TypeUserRepository> _logger;

    public TypeUserRepository(FilmsDbContext context, ILogger<TypeUserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TypeUser>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TypeUsers
                .Where(t => t.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all type users");
            throw;
        }
    }

    public async Task<TypeUser?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TypeUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving type user with ID {TypeUserId}", id);
            throw;
        }
    }

    public async Task<TypeUser> AddAsync(TypeUser typeUser, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.TypeUsers.Add(typeUser);
            await _context.SaveChangesAsync(cancellationToken);
            return typeUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding type user");
            throw;
        }
    }

    public async Task UpdateAsync(TypeUser typeUser, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.TypeUsers.Update(typeUser);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating type user with ID {TypeUserId}", typeUser.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var typeUser = await _context.TypeUsers.FindAsync(new object[] { id }, cancellationToken);
            if (typeUser != null)
            {
                typeUser.IsActive = false;
                typeUser.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting type user with ID {TypeUserId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TypeUsers.AnyAsync(t => t.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if type user exists with ID {TypeUserId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<TypeUser>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.TypeUsers
                .Where(t => t.IsActive && t.Name.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching type users with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
