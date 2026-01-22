using Films.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Films.Web.Pages.Account
{
    /// <summary>
    /// Page model for user logout.
    /// </summary>
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutModel"/> class.
        /// </summary>
        /// <param name="signInManager">The sign-in manager.</param>
        /// <param name="logger">The logger.</param>
        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles the POST request for user logout.
        /// </summary>
        /// <param name="returnUrl">The return URL after logout.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            try
            {
                var userName = User.Identity?.Name ?? "Unknown";
                await _signInManager.SignOutAsync();
                
                _logger.LogInformation("User '{UserName}' logged out", userName);

                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return RedirectToPage("/Index");
            }
        }

        /// <summary>
        /// Handles the GET request for logout (redirects to POST).
        /// </summary>
        /// <param name="returnUrl">The return URL after logout.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
        {
            // Perform logout on GET as well for convenience
            return await OnPostAsync(returnUrl);
        }
    }
}
