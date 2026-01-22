using AutoMapper;
using Microsoft.Extensions.Logging;
using Films.Domain.DTOs;
using Films.Domain.Entities;
using Films.Domain.Interfaces.Repositories;
using Films.Domain.Interfaces.Services;

namespace Films.Application.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ActorService> _logger;

    public ActorService(
        IActorRepository actorRepository,
        IMapper mapper,
        ILogger<ActorService> logger)
    {
        _actorRepository = actorRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ActorDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var actors = await _actorRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ActorDto>>(actors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllAsync");
            throw;
        }
    }

    public async Task<ActorDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var actor = await _actorRepository.GetByIdAsync(id, cancellationToken);
            return actor != null ? _mapper.Map<ActorDto>(actor) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetByIdAsync for ID {ActorId}", id);
            throw;
        }
    }

    public async Task<ActorDto> CreateAsync(ActorCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var actor = _mapper.Map<Actor>(createDto);
            actor.CreatedDate = DateTime.UtcNow;
            actor.IsActive = true;

            var result = await _actorRepository.AddAsync(actor, cancellationToken);
            return _mapper.Map<ActorDto>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateAsync");
            throw;
        }
    }

    public async Task UpdateAsync(int id, ActorUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var actor = await _actorRepository.GetByIdAsync(id, cancellationToken);
            if (actor == null)
            {
                throw new KeyNotFoundException($"Actor with ID {id} not found");
            }

            _mapper.Map(updateDto, actor);
            actor.ModifiedDate = DateTime.UtcNow;

            await _actorRepository.UpdateAsync(actor, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateAsync for ID {ActorId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _actorRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteAsync for ID {ActorId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<ActorDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            var actors = await _actorRepository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<ActorDto>>(actors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SearchAsync with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
