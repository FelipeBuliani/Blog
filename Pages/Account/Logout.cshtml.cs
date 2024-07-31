using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog.Application.Services.Interfaces;

namespace Blog.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthAppService _authAppService;

        public LogoutModel(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        public async Task<IActionResult> OnGet()
        {
            _authAppService.SignOut();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            

            return RedirectToPage("/Account/Login");
        }
    }
}