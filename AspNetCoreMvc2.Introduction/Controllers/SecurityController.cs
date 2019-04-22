using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Identity;
using AspNetCoreMvc2.Introduction.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc2.Introduction.Controllers
{
	public class SecurityController : Controller
	{
		private UserManager<AppIdentityUser> _userManager;
		private SignInManager<AppIdentityUser> _signInManager;

		public SecurityController(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(loginViewModel);
			}

			var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
			if (user != null)
			{
				if (!await _userManager.IsEmailConfirmedAsync(user))
				{
					ModelState.AddModelError(string.Empty, "Confirm your email please");
					return View(loginViewModel);
				}
			}

			var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

			if (result.Succeeded)
			{
				return RedirectToAction("Index", "Student");
			}

			ModelState.AddModelError(string.Empty, "Login Failed");
			return View(loginViewModel);
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "School");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(registerViewModel);
			}

			var user = new AppIdentityUser
			{
				UserName = registerViewModel.UserName,
				Email = registerViewModel.Email,
				Age = registerViewModel.Age
			};

			var result = await _userManager.CreateAsync(user, registerViewModel.Password);

			if (result.Succeeded)
			{
				var confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(user);
				var callBackUrl = Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode });

				//Send Email

				return RedirectToAction("Index", "Student");
			}

			return View(registerViewModel);
		}

		public async Task<IActionResult> ConfirmEmail(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return RedirectToAction("Index", "Student");
			}
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				throw new ApplicationException("Unable to find the user");
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);
			if (result.Succeeded)
			{
				return View("ConfirmEmail");
			}

			return RedirectToAction("Index", "Student");
		}
	}
}