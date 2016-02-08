using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace MvcPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISignInService _signService;

        public AccountController(IUserService userService, ISignInService signService)
        {
            _userService = userService;
            _signService = signService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Roles = new List<RoleEntity>() { new RoleEntity() { Id = 3 } }
                };

                user = _userService.CreateUser(user);
                if (user != null)
                {
                    _signService.IdentitySignin(user);
                    return RedirectToAction("Index", "Home",null);
                }

                ViewBag.Error = "This user already exists";
                return View("Register");
            }
            ViewBag.Error = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)); ;
            return View("Register");

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(UserLoginModel model)
        {
            UserEntity user = _userService.ValidateUser(model.Name, model.Password);
            if (user != null)
            {
                _signService.IdentitySignin(user, model.RememberMe);
                return RedirectToAction("Index", "Home");
            }
            TempData.Add("Error", "Wrong username or password");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            _signService.IdentitySignout();
            return RedirectToAction("Index", "Home",null);
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int id)
        {
            var user = _userService.GetUserEntity(id);
            PopulateRoles(user);
            return View(user.ToUserViewModel());
        }
        private void PopulateRoles(UserEntity user)
        {
            var allRoles = _userService.GetAllRoles();
            var userRoles = new HashSet<int>(user.Roles.Select(x => x.Id));
            var viewModel = new List<RoleViewModel>();
            foreach (var role in allRoles)
            {
                viewModel.Add(new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsAssigned = userRoles.Contains(role.Id)
                });
            }
            ViewBag.Roles = viewModel;
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(UserViewModel user, string[] selectedRoles)
        {

            UpdateUserRoles(user, selectedRoles);
            _userService.UpdateUser(user.ToBllUser());
            if (!((ClaimsIdentity)User.Identity).Claims
                    .Any(x => x.Type == ClaimTypes.Role &&
                    x.Value == "Admin"))
            {
                _signService.IdentitySignout();
                _signService.IdentitySignin(user.ToBllUser());
            }
            return RedirectToAction("Index", "Home",null);
        }
        private void UpdateUserRoles(UserViewModel user, string[] selectedRoles)
        {
            if (selectedRoles == null)
            {
                user.Roles = new List<RoleViewModel>();
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);
            var userRoles = new HashSet<int>
                (user.Roles.Select(x => x.Id));
            foreach (var role in _userService.GetAllRoles())
            {
                if (selectedRolesHS.Contains(role.Id.ToString()))
                {
                    if (!userRoles.Contains(role.Id))
                    {
                        user.Roles.Add(role.ToRoleViewModel());
                    }
                }
                else
                {
                    if (userRoles.Contains(role.Id))
                    {
                        user.Roles.Remove(role.ToRoleViewModel());
                    }
                }
            }
        }
        /*
        [ActionName("Index")]
        public ActionResult GetAllUsers()
        {
            return View(service.GetAllUserEntities().Select(user => user.ToUserViewModel()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            service.CreateUser(userViewModel.ToBllUser());
            return RedirectToAction("Index");
        }
        */
        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            UserEntity user = _userService.GetUserEntity(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToUserViewModel());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserEntity user)
        {
            _userService.DeleteUser(user);
            return RedirectToAction("Index", "Home", null);
        }

    }
}