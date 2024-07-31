using Blog.Application.Services.Interfaces;
using Blog.DataTransfer.Requests;
using Blog.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IPostAppService _postAppService;
        private readonly IUserAppService _userAppService;
        private readonly ManagerWebSocket _webSocketManager;

        public CreateModel(IPostAppService postAppService, IUserAppService userAppService, ManagerWebSocket webSocketManager)
        {
            _postAppService = postAppService;
            _userAppService = userAppService;
            _webSocketManager = webSocketManager;
        }

        [BindProperty]
        public CreatePostRequest Post { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Post.UserId = _userAppService.GetUserId(User);
                _postAppService.Create(Post);
                await _webSocketManager.BroadcastMessage("Novo post adicionado");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao adicionar post";
                ModelState.AddModelError("", "An error occurred while creating the post.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
