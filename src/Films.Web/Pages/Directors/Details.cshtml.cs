using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Directors
{
    public class DetailsModel : PageModel
    {
        private readonly IDirectedByService _directedByService;
        public DirectedByDto? Director { get; set; }

        public DetailsModel(IDirectedByService directedByService)
        {
            _directedByService = directedByService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            Director = await _directedByService.GetByIdAsync(id.Value);
            return Director == null ? NotFound() : Page();
        }
    }
}
