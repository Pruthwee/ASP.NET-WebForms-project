using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly IFilmService _filmService;
        public List<FilmDto> Films { get; set; } = new();
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }
        public string? NameSortParam { get; set; }
        public int TotalCount => Films.Count;

        public IndexModel(IFilmService filmService)
        {
            _filmService = filmService;
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            CurrentSort = sortOrder;
            CurrentFilter = searchString;
            NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            var films = await _filmService.GetAllAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                films = films.Where(f => f.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            
            films = sortOrder switch
            {
                "name_desc" => films.OrderByDescending(f => f.Name),
                _ => films.OrderBy(f => f.Name)
            };
            
            Films = films.ToList();
        }
    }
}
