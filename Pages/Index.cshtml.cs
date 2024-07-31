using Blog.Application.Services;
using Blog.Application.Services.Interfaces;
using Blog.DataTransfer.Responses;
using Blog.Domain.Entities;
using Blog.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPostAppService _postAppService;
        private readonly IUserAppService _userAppService;
        private readonly ManagerWebSocket _webSocketManager;

        public IndexModel(IPostAppService postAppService, IUserAppService userAppService, ManagerWebSocket webSocketManager)
        {
            _postAppService = postAppService;
            _userAppService = userAppService;
            _webSocketManager = webSocketManager;
        }

        public IEnumerable<PostResponse> Posts { get; set; }
        public string UserIdSession { get; set; }

        public async Task OnGetAsync()
        {
            Posts = _postAppService.GetAll();
            UserIdSession = _userAppService.GetUserId(User);
        }

        public IActionResult Create()
        {
            return RedirectToPage("./Create");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var post = _postAppService.GetById(id);

            if (post == null)
            {
                return NotFound();
            }

            _postAppService.Delete(id);
            await _webSocketManager.BroadcastMessage("Post deletado");


            return RedirectToPage();
        }
    }
}
