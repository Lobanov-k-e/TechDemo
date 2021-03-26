using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechDemo.Core.Infrastructure.Services;

namespace TechDemo.WebApi.Controllers
{
    public class UserController : Controller
    {
        const string Index_Action = nameof(Index);
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVm model)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(model);
                return RedirectToAction(Index_Action);
            }

            return View(model);
        }

        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(Index_Action);           
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }

            return View(UpdateUserVm.FormUser(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserVm model)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateAsync(model);
                return RedirectToAction(Index_Action);
            }

            return View(model);
        }
    }
}