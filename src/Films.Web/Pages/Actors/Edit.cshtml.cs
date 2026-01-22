using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Films.Web.Pages.Actors
{
    public class EditModel : PageModel
    {
        private readonly IActorService _actorService;
        
        [BindProperty]
        public ActorInputModel Actor { get; set; } = new();

        public EditModel(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            var actor = await _actorService.GetByIdAsync(id.Value);
            if (actor == null) return NotFound();
            
            Actor = new ActorInputModel
            {
                Id = actor.Id,
                Name = actor.Name,
                Description = actor.Description,
                SexId = actor.SexId
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var updateDto = new ActorUpdateDto
            {
                Name = Actor.Name,
                Description = Actor.Description,
                SexId = Actor.SexId,
                IsActive = true
            };
            
            await _actorService.UpdateAsync(Actor.Id, updateDto);
            return RedirectToPage("./Index");
        }

        public class ActorInputModel
        {
            public int Id { get; set; }
            
            [Required]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;
            
            [StringLength(500)]
            public string? Description { get; set; }
            
            public int? SexId { get; set; }
        }
    }
}
