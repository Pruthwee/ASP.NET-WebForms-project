using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Actors
{
    public class DetailsModel : PageModel
    {
        private readonly IActorService _actorService;
        public ActorDto? Actor { get; set; }

        public DetailsModel(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Actor = await _actorService.GetByIdAsync(id.Value);
            return Actor == null ? NotFound() : Page();
        }
    }
}
