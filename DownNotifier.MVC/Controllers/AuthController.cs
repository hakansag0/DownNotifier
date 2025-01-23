using DownNotifier.Application.CustomExceptions.User;
using DownNotifier.Application.Features.UserFeatures.Command;
using DownNotifier.Application.Features.UserFeatures.Query;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DownNotifier.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<AuthController> logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/Dashboard");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequest form)
        {
            try
            {
                var response = await mediator.Send(form);

                List<Claim> claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier,response.Id.ToString()),
                        new Claim(ClaimTypes.Email,response.Email),
                        new Claim(ClaimTypes.Name,response.Name),
                         new Claim(ClaimTypes.Role,"User"),
                    };
                ;

                var identity = new ClaimsIdentity(claims,
                  CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties()
                {
                    IsPersistent = true,
                });
                return Redirect("/Dashboard");
            }
            catch (LoginUserNotFoundException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                ViewBag.ErrorMessage = "An error occured.";
            }
            return View();
        }


        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect("/Dashboard");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommandRequest form)
        {
            try
            {
                await mediator.Send(form);
                return RedirectToAction(nameof(Login));
            }
            catch (RegisterUserExistsException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                ViewBag.ErrorMessage = "An error occured.";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
