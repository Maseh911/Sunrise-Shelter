// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace SunriseShelter.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<SunriseShelterUser> _signInManager;
        private readonly UserManager<SunriseShelterUser> _userManager;
        private readonly IUserStore<SunriseShelterUser> _userStore;
        private readonly IUserEmailStore<SunriseShelterUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<SunriseShelterUser> userManager,
            IUserStore<SunriseShelterUser> userStore,
            SignInManager<SunriseShelterUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(25)]
            [Display(Name = "First Name")]
            [NoNumbersOrSymbols]
            public string FirstName { get; set; }

            [Required]
            [StringLength(25)]
            [Display(Name = "Last Name")]
            [NoNumbersOrSymbols]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [NewZealandPhone]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [StringLength(50)]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Marital Status")]
            [StringLength(15)]
            [NoSpacesOrNumbersOrSymbols]
            public string MartialStatus { get; set; }

            [Required]
            [Display(Name = "Address")]
            [StringLength(25)]
            [NoSymbols]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Country of Origin")]
            [StringLength(25)]
            [NoNumbersOrSymbols]
            public string BirthPlace { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                // Set all user properties
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.DateOfBirth = Input.DateOfBirth;
                user.PhoneNumber = Input.PhoneNumber;
                user.MartialStatus = Input.MartialStatus;
                user.Address = Input.Address;
                user.BirthPlace = Input.BirthPlace;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Add user to Parent role
                    await _userManager.AddToRoleAsync(user, "Parent");

                    // Sign in and redirect
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private SunriseShelterUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<SunriseShelterUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(SunriseShelterUser)}'. " +
                    $"Ensure that '{nameof(SunriseShelterUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<SunriseShelterUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<SunriseShelterUser>)_userStore;
        }
    }
}