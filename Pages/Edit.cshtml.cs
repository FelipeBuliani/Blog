using Blog.Application.Services.Interfaces;
using Blog.DataTransfer.Requests;
using Blog.DataTransfer.Responses;
using Blog.Domain.Entities;
using Blog.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class EditModel : PageModel
    {
        private readonly IPostAppService _postAppService;
        private readonly IUserAppService _userAppService;
        private readonly ManagerWebSocket _webSocketManager;

        public EditModel(IPostAppService postAppService, IUserAppService userAppService, ManagerWebSocket webSocketManager)
        {
            _postAppService = postAppService;
            _userAppService = userAppService;
            _webSocketManager = webSocketManager;
        }

        [BindProperty]
        public PostResponse Post { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;
            Post = _postAppService.GetById(id);
            if (Post == null)
            {
                return NotFound();
            }

            if (Post.UserId != _userAppService.GetUserId(User))
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var editPost = new EditPostRequest
                {
                    Id = Id,
                    Title = Post.Title,
                    Content = Post.Content
                };
                editPost.UserId = _userAppService.GetUserId(User);
                _postAppService.Update(editPost);
                await _webSocketManager.BroadcastMessage("Post editado");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Erro ao editar post";
                ModelState.AddModelError("", "An error occurred while updating the post.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
