using DownNotifier.Application.CustomExceptions.TargetApp;
using DownNotifier.Application.Features.TargetAppFeatures.Command;
using DownNotifier.Application.Features.TargetAppFeatures.Query;
using DownNotifier.Application.Utilities.HealthCheck;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DownNotifier.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<DashboardController> logger;

        public DashboardController(IMediator mediator, ILogger<DashboardController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var targetApps = await mediator.Send(new GetTargetAppsForUserRequest(userId));
            return View(targetApps);
        }


        public IActionResult CreateTargetApp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTargetApp(CreateTargetAppCommandRequest form)
        {
            try
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
                form.UserId = userId;
                var response = await mediator.Send(form);
                return RedirectToAction(nameof(Index));
            }
            catch (TargetAppAlreadyExistsException ex)
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


        public async Task<IActionResult> UpdateTargetApp(int id)
        {
            try
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
                var response = await mediator.Send(new GetTargetAppByIdForUserRequest(userId, id));
                if (response == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTargetApp(UpdateTargetAppCommandRequest form)
        {
            try
            {
                var response = await mediator.Send(form);
                return RedirectToAction(nameof(Index));
            }
            catch (TargetAppAlreadyExistsException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (TargetAppNotFoundException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                ViewBag.ErrorMessage = "An error occured";
            }
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CheckApplication([FromBody] string url)
        {
            try
            {
                bool status = await HealthChecker.CheckURL(url);
                return Json(new { serviceState = status, state = true });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                return Json(new { state = false });
            }
        }


        [HttpDelete]
        public async Task<JsonResult> DeleteApplication([FromBody] int id)
        {
            try
            {
                int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
                await mediator.Send(new DeleteTargetAppCommandRequest(id, userId));
                return Json(new { state = true });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
                return Json(new { state = false });
            }
        }
    }
}
