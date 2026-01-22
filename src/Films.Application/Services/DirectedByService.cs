using AutoMapper;
using Microsoft.Extensions.Logging;
using Films.Domain.DTOs;
using Films.Domain.Entities;
using Films.Domain.Interfaces.Repositories;
using Films.Domain.Interfaces.Services;

namespace Films.Application.Services;

public class DirectedByService : IDirectedByService
{
    private readonly IDirectedByRepository _directedByRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DirectedByService> _logger;

    public DirectedByService(
        IDirectedByRepository directedByRepository,
        IMapper mapper,
        ILogger<DirectedByService> logger)
    {
        _directedByRepository = directedByRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<DirectedByDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var directors = await _directedByRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<DirectedByDto>>(directors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllAsync");
            throw;
        }
    }

    public async Task<DirectedByDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var director = await _directedByRepository.GetByIdAsync(id, cancellationToken);
            return director != null ? _mapper.Map<DirectedByDto>(director) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetByIdAsync for ID {DirectorId}", id);
            throw;
        }
    }

    public async Task<DirectedByDto> CreateAsync(DirectedByCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var director = _mapper.Map<DirectedBy>(createDto);
            director.CreatedDate = DateTime.UtcNow;
            director.IsActive = true;

            var result = await _directedByRepository.AddAsync(director, cancellationToken);
            return _mapper.Map<DirectedByDto>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateAsync");
            throw;
        }
    }

    public async Task UpdateAsync(int id, DirectedByUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var director = await _directedByRepository.GetByIdAsync(id, cancellationToken);
            if (director == null)
            {
                throw new KeyNotFoundException($"Director with ID {id} not found");
            }

            _mapper.Map(updateDto, director);
            director.ModifiedDate = DateTime.UtcNow;

            await _directedByRepository.UpdateAsync(director, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateAsync for ID {DirectorId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _directedByRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteAsync for ID {DirectorId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<DirectedByDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            var directors = await _directedByRepository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<DirectedByDto>>(directors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SearchAsync with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
