using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.API.Models.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
   public class AccountController : Controller
   {
      public AccountController()
      {

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