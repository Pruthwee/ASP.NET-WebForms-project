using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Films.Web.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly IFilmService _filmService;
        
        [BindProperty]
        public FilmInputModel Film { get; set; } = new();

        public CreateModel(IFilmService filmService)
        {
            _filmService = filmService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var createDto = new FilmCreateDto
            {
                Name = Film.Name,
                Description = Film.Description,
                Year = Film.Year,
                Country = Film.Country
            };
            
            await _filmService.CreateAsync(createDto);
            return RedirectToPage("./Index");
        }

        public class FilmInputModel
        {
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
