using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Films.Web.Pages.Directors
{
    public class CreateModel : PageModel
    {
        private readonly IDirectedByService _directedByService;
        
        [BindProperty]
        public DirectorInputModel Director { get; set; } = new();

        public CreateModel(IDirectedByService directedByService)
        {
            _directedByService = directedByService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var createDto = new DirectedByCreateDto
            {
                Name = Director.Name,
                Description = Director.Description
            };
            
            await _directedByService.CreateAsync(createDto);
            return RedirectToPage("./Index");
        }

        public class DirectorInputModel
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;
            
            [StringLength(500)]
            public string? Description { get; set; }
        }
    }
}
