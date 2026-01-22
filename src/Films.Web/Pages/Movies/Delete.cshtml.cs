using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly IFilmService _filmService;
        public FilmDto? Film { get; set; }

        public DeleteModel(IFilmService filmService)
        {
            _filmService = filmService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Film = await _filmService.GetByIdAsync(id.Value);
            return Film == null ? NotFound() : Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();
            await _filmService.DeleteAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}
