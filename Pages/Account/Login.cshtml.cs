using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Blog.Domain.Services.Interfaces;
using Blog.DataTransfer.Requests;

namespace Blog.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly BlogDbContext _context;
        private readonly IAuthService _authService;

        public LoginModel(BlogDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [BindProperty]
        public UserLogin User { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var authenticated = await _authService.Autheticate(User.UserName, User.Password);
            if (!authenticated)
            {
                ModelState.AddModelError("", "Usuário ou senha inválida");
                TempData["ErrorMessage"] = "Usuário ou senha inválida";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToPage("/Index");
        }
    }
}
