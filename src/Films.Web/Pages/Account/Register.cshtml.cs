using System.ComponentModel.DataAnnotations;
using Films.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Account
{
    /// <summary>
    /// Page model for user registration.
    /// </summary>
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterModel"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign-in manager.</param>
        /// <param name="logger">The logger.</param>
        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets or sets the registration input model.
        /// </summary>
        [BindProperty]
        public RegisterInputModel Input { get; set; } = new RegisterInputModel();

        /// <summary>
        /// Handles the GET request for the registration page.
        /// </summary>
        /// <param name="returnUrl">The return URL after successful registration.</param>
        /// <returns>The page result.</returns>
        public IActionResult OnGet(string? returnUrl = null)
        {
            Input.ReturnUrl = returnUrl ?? Url.Content("~/");
            return Page();
        }

        /// <summary>
        /// Handles the POST request for user registration.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            string returnUrl = Input.ReturnUrl ?? Url.Content("~/");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User '{Email}' created a new account with password", Input.Email);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    _logger.LogInformation("User '{Email}' logged in after registration", Input.Email);

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for user '{Email}'", Input.Email);
                ModelState.AddModelError(string.Empty, "An error occurred during registration. Please try again.");
                return Page();
            }
        }

        /// <summary>
        /// Input model for user registration.
        /// </summary>
        public class RegisterInputModel
        {
            /// <summary>
            /// Gets or sets the email address.
            /// </summary>
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            [Required(ErrorMessage = "Password is required")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the password confirmation.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the return URL.
            /// </summary>
            public string ReturnUrl { get; set; } = string.Empty;
        }
    }
}
