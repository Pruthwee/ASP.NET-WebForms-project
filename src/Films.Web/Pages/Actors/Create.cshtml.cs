using System.ComponentModel.DataAnnotations;
using Films.Domain.DTOs;
using Films.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Actors
{
    public class CreateModel : PageModel
    {
        private readonly IActorService _actorService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IActorService actorService, ILogger<CreateModel> logger)
        {
            _actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public ActorInputModel Actor { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var createDto = new ActorCreateDto
                {
                    Name = Actor.Name,
                    Description = Actor.Description,
                    SexId = Actor.SexId
                };

                await _actorService.CreateAsync(createDto);

                _logger.LogInformation("Actor '{Name}' created successfully", Actor.Name);
                TempData["SuccessMessage"] = $"Actor '{Actor.Name}' has been created successfully.";

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating actor '{Name}'", Actor.Name);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the actor. Please try again.");
                return Page();
            }
        }

        public class ActorInputModel
        {
            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
            [Display(Name = "Name")]
            public string Name { get; set; } = string.Empty;

            [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
            [Display(Name = "Description")]
            public string? Description { get; set; }

            [Display(Name = "Sex")]
            public int? SexId { get; set; }
        }
    }
}
