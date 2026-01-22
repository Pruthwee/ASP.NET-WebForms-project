using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Films.Web.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly IFilmService _filmService;
        
        [BindProperty]
        public FilmInputModel Film { get; set; } = new();

        public EditModel(IFilmService filmService)
        {
            _filmService = filmService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            var film = await _filmService.GetByIdAsync(id.Value);
            if (film == null) return NotFound();
            
            Film = new FilmInputModel
            {
                Id = film.Id,
                Name = film.Name,
                Description = film.Description,
                Year = film.Year,
                Country = film.Country
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var updateDto = new FilmUpdateDto
            {
                Name = Film.Name,
                Description = Film.Description,
                Year = Film.Year,
                Country = Film.Country,
                IsActive = true
            };
            
            await _filmService.UpdateAsync(Film.Id, updateDto);
            return RedirectToPage("./Index");
        }

        public class FilmInputModel
        {
            public int Id { get; set; }
            
            [Required]
            [StringLength(200)]
            public string Name { get; set; } = string.Empty;
            
            [StringLength(1000)]
            public string? Description { get; set; }
            
            public int? Year { get; set; }
            
            [StringLength(100)]
            public string? Country { get; set; }
        }
    }
}
