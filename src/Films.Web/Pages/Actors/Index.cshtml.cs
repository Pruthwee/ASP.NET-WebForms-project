using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Actors
{
    public class IndexModel : PageModel
    {
        private readonly IActorService _actorService;
        public List<ActorDto> Actors { get; set; } = new();
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }
        public string? NameSortParam { get; set; }

        public IndexModel(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            CurrentSort = sortOrder;
            CurrentFilter = searchString;
            NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            var actors = await _actorService.GetAllAsync();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                actors = actors.Where(a => a.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            
            actors = sortOrder switch
            {
                "name_desc" => actors.OrderByDescending(a => a.Name),
                _ => actors.OrderBy(a => a.Name)
            };
            
            Actors = actors.ToList();
        }
    }
}
