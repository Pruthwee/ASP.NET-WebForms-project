using System.ComponentModel.DataAnnotations;
using Films.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Account
{
    /// <summary>
    /// Page model for user login.
    /// </summary>
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginModel"/> class.
        /// </summary>
        /// <param name="signInManager">The sign-in manager.</param>
        /// <param name="logger">The logger.</param>
        public LoginModel(SignInManager<User> signInManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets or sets the login input model.
        /// </summary>
        [BindProperty]
        public LoginInputModel Input { get; set; } = new LoginInputModel();

        /// <summary>
        /// Handles the GET request for the login page.
        /// </summary>
        /// <param name="returnUrl">The return URL after successful login.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            Input.ReturnUrl = returnUrl;

            return Page();
        }

        /// <summary>
        /// Handles the POST request for user login.
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
                var result = await _signInManager.PasswordSignInAsync(
                    Input.Email,
                    Input.Password,
                    Input.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User '{Email}' logged in successfully", Input.Email);
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    _logger.LogWarning("User '{Email}' requires two-factor authentication", Input.Email);
                    ModelState.AddModelError(string.Empty, "Two-factor authentication is required.");
                    return Page();
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User '{Email}' account is locked out", Input.Email);
                    ModelState.AddModelError(string.Empty, "This account has been locked out. Please try again later.");
                    return Page();
                }

                _logger.LogWarning("Invalid login attempt for user '{Email}'", Input.Email);
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login attempt for user '{Email}'", Input.Email);
                ModelState.AddModelError(string.Empty, "An error occurred during login. Please try again.");
                return Page();
            }
        }

        /// <summary>
        /// Input model for user login.
        /// </summary>
        public class LoginInputModel
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
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets a value indicating whether to remember the user.
            /// </summary>
            [Display(Name = "Remember me")]
            public bool RememberMe { get; set; }

            /// <summary>
            /// Gets or sets the return URL.
            /// </summary>
            public string ReturnUrl { get; set; } = string.Empty;
        }
    }
}
