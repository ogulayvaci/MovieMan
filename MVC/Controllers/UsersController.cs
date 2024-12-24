using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class UsersController : MvcController
    {
        // Service injections:
        private readonly IService<user, UserModel> _userService;
        private readonly IService<role, RoleModel> _roleService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public UsersController(
			IService<user, UserModel> userService
            , IService<role, RoleModel> roleService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _userService = userService;
            _roleService = roleService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Users
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // Way 1:
                //var result = _userService.Query().Where(u => u.Record.UserName == user.UserName && u.Record.Password == user.Password && u.Record.IsActive).FirstOrDefault();
                // Way 2:
                var model = _userService.Query().FirstOrDefault(u => u.Record.username == user.username && u.Record.password == user.password && u.Record.isactive);
                if (model is not null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, model.username),
                        new Claim(ClaimTypes.Role, model.role),
                        new Claim("id", model.Record.id.ToString())
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); // CookieAuthenticationDefaults.AuthenticationScheme = cookie name
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
	}
}
