using AcuCall.Core.Interfaces;
using AcuCall.Web.Hubs;
using AcuCall.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace AcuCall.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;        
        private readonly IMapper _mapper;
        private readonly IHubContext<UserHub> _userHub;

        public UsersController(IUserService userService, IMapper mapper, IHubContext<UserHub> userHub)
        {
            _userService = userService;
            _mapper = mapper;
            _userHub = userHub;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View((await _userService.GetAllUsersAsync()).Select(_mapper.Map<User>).ToList());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                int userId = await _userService.AddUserAsync(_mapper.Map<Core.Objects.User>(user));
                if (userId > 0)
                {
                    user.UserId = userId;
                    await _userHub.Clients.All.SendAsync("newUser", user);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(user);
        }


        // GET: Users/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var user = _mapper.Map<User>(await _userService.GetUserByIdAsync(id));
            return View(user);
        }

        // POST: Users/Edit        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                bool successful = await _userService.UpdateUserAsync(_mapper.Map<Core.Objects.User>(user));
                if (successful)
                {
                    await _userHub.Clients.All.SendAsync("updateUser", user);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool successful = await _userService.DeleteUserAsync(id);
            if (successful)
            {
                await _userHub.Clients.All.SendAsync("removedUser", id);
                return new StatusCodeResult(200);
            }

            return new BadRequestObjectResult(new { message = "NotFound" });
        }
    }
}
