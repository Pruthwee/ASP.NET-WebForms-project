using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly IFilmService _filmService;
        private readonly IActorService _actorService;
        private readonly IDirectedByService _directedByService;
        
        public FilmDto? Film { get; set; }
        public List<ActorDto> Actors { get; set; } = new();
        public List<DirectedByDto> Directors { get; set; } = new();

        public DetailsModel(IFilmService filmService, IActorService actorService, IDirectedByService directedByService)
        {
            _filmService = filmService;
            _actorService = actorService;
            _directedByService = directedByService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Film = await _filmService.GetByIdAsync(id.Value);
            if (Film == null) return NotFound();
            
            // Get related data
            Actors = (await _actorService.GetAllAsync()).ToList();
            Directors = (await _directedByService.GetAllAsync()).ToList();
            
            return Page();
        }
    }
}
