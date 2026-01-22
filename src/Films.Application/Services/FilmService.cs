using AutoMapper;
using Microsoft.Extensions.Logging;
using Films.Domain.DTOs;
using Films.Domain.Entities;
using Films.Domain.Interfaces.Repositories;
using Films.Domain.Interfaces.Services;

namespace Films.Application.Services;

public class FilmService : IFilmService
{
    private readonly IFilmRepository _filmRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FilmService> _logger;

    public FilmService(
        IFilmRepository filmRepository,
        IMapper mapper,
        ILogger<FilmService> logger)
    {
        _filmRepository = filmRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<FilmDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var films = await _filmRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<FilmDto>>(films);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllAsync");
            throw;
        }
    }

    public async Task<FilmDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var film = await _filmRepository.GetByIdAsync(id, cancellationToken);
            return film != null ? _mapper.Map<FilmDto>(film) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetByIdAsync for ID {FilmId}", id);
            throw;
        }
    }

    public async Task<FilmDto> CreateAsync(FilmCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var film = _mapper.Map<Film>(createDto);
            film.CreatedDate = DateTime.UtcNow;
            film.IsActive = true;

            var result = await _filmRepository.AddAsync(film, cancellationToken);
            return _mapper.Map<FilmDto>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateAsync");
            throw;
        }
    }

    public async Task UpdateAsync(int id, FilmUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var film = await _filmRepository.GetByIdAsync(id, cancellationToken);
            if (film == null)
            {
                throw new KeyNotFoundException($"Film with ID {id} not found");
            }

            _mapper.Map(updateDto, film);
            film.ModifiedDate = DateTime.UtcNow;

            await _filmRepository.UpdateAsync(film, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateAsync for ID {FilmId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _filmRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteAsync for ID {FilmId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<FilmDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            var films = await _filmRepository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<FilmDto>>(films);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SearchAsync with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
