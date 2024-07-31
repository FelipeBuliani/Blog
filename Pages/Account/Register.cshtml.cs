using Blog.Application.Services.Interfaces;
using Blog.DataTransfer.Requests;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserAppService _userAppService;

        public RegisterModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        public CreateUserRequest User { get; set; }

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

            _userAppService.CreateUser(new CreateUserRequest { UserName = User.UserName }, User.Password);

            return RedirectToPage("/Account/Login");
        }
    }
}
