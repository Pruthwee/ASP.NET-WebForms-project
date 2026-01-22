using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Directors
{
    public class IndexModel : PageModel
    {
        private readonly IDirectedByService _directedByService;
        public List<DirectedByDto> Directors { get; set; } = new();
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }
        public string? NameSortParam { get; set; }

        public IndexModel(IDirectedByService directedByService)
        {
            _directedByService = directedByService;
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            CurrentSort = sortOrder;
            CurrentFilter = searchString;
            NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            var directors = await _directedByService.GetAllAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                directors = directors.Where(d => d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            
            directors = sortOrder switch
            {
                "name_desc" => directors.OrderByDescending(d => d.Name),
                _ => directors.OrderBy(d => d.Name)
            };
            
            Directors = directors.ToList();
        }
    }
}
