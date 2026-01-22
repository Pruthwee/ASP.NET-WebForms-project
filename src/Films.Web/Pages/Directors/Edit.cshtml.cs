using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Films.Web.Pages.Directors
{
    public class EditModel : PageModel
    {
        private readonly IDirectedByService _directedByService;
        
        [BindProperty]
        public DirectorInputModel Director { get; set; } = new();

        public EditModel(IDirectedByService directedByService)
        {
            _directedByService = directedByService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            var director = await _directedByService.GetByIdAsync(id.Value);
            if (director == null) return NotFound();
            
            Director = new DirectorInputModel
            {
                Id = director.Id,
                Name = director.Name,
                Description = director.Description
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var updateDto = new DirectedByUpdateDto
            {
                Name = Director.Name,
                Description = Director.Description,
                IsActive = true
            };
            
            await _directedByService.UpdateAsync(Director.Id, updateDto);
            return RedirectToPage("./Index");
        }

        public class DirectorInputModel
        {
            public int Id { get; set; }
            
            [Required]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;
            
            [StringLength(500)]
            public string? Description { get; set; }
        }
    }
}
