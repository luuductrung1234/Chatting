using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Identity.Infrastructure.Data;
using Identity.API.Services;
using Identity.API.Models.AccountViewModels;
using Microsoft.AspNetCore.Authentication;

namespace Identity.API.Controllers
{
   public class AccountController : Controller
   {
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly ILoginService<ApplicationUser> _loginService;
      private readonly ILogger<AccountController> _logger;
      private readonly IConfiguration _configuration;

      public AccountController(
         UserManager<ApplicationUser> userManager,
         ILoginService<ApplicationUser> loginService,
         ILogger<AccountController> logger,
         IConfiguration configuration)
      {
         _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
         _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
         _logger = logger ?? throw new ArgumentNullException(nameof(logger));
         _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
      }

      /// <summary>
      /// Show login page
      /// </summary>
      [HttpGet]
      public async Task<IActionResult> Login(string returnUrl = null)
      {
         var vm = await BuildLoginViewModelAsync(returnUrl);

         ViewData["ReturnUrl"] = returnUrl;

         return View();
      }

      /// <summary>
      /// Handle postback from username/password name
      /// </summary>
      [HttpPost]
      public async Task<IActionResult> Login(LoginViewModel model)
      {
         if (ModelState.IsValid)
         {
            var user = await _loginService.FindByUserName(model.Email);

            if (await _loginService.ValidateCredentials(user, model.Password))
            {
               var tokenLifetime = _configuration.GetValue("TokenLifeTimeMinutes", 120);

               var props = new AuthenticationProperties()
               {
                  ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifetime),
                  AllowRefresh = true,
                  RedirectUri = model.ReturnUrl
               };

               if (model.RememberMe)
               {
                  var permanentTokenLifetime = _configuration.GetValue("PermanentTokenLifetimeDays", 365);

                  props.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(permanentTokenLifetime);
                  props.IsPersistent = true;
               }

               await _loginService.SignInAsync(user, props);

               return Redirect("~/");
            }

            ModelState.AddModelError("", "Invalid username or password.");
         }

         // something when wrong, show forn with errors
         var vm = await BuildLoginViewModelAsync(model);

         ViewData["ReturnUrl"] = model.ReturnUrl;

         return View(vm);
      }

      #region Helper Methods

      private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
      {
         return await Task.FromResult(new LoginViewModel()
         {
            ReturnUrl = returnUrl,
            Email = ""
         });
      }

      private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginViewModel model)
      {
         var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
         vm.Email = model.Email;
         vm.RememberMe = model.RememberMe;
         return vm;
      }

      #endregion
   }
}