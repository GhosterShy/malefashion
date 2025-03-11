using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using malefashion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Identity.UI.Services;
using malefashion_master.Models;



namespace malefashion.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;




	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	


	}

	public async Task<IActionResult> Index()
	{
		//AppUser appUser = new AppUser();
		//appUser.UserName = "Shyngys";
		//appUser.Email = "shyngys@gmail.com";

		//var result = await _userManager.CreateAsync(appUser, "@Shyngys_2005");

		//if (result.Succeeded)
		//{

		//}
		Console.WriteLine($"CurrentCulture: {CultureInfo.CurrentCulture}");
		Console.WriteLine($"CurrentUICulture: {CultureInfo.CurrentUICulture}");




		_logger.LogCritical("Test of Critical Logging");
		_logger.LogError("Test of Error Logging");
		_logger.LogInformation("Test of Infomation Logging");
		_logger.LogDebug("^Test of Debug Logging");
		return View();
	}






	public IActionResult Contacts()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Contacts(ContactModel contact)
	{

		_logger.LogInformation("Оправка сообщений пользователя {name} на адрес {email}", contact.Name, contact.Email);

		if (string.IsNullOrWhiteSpace(contact.Name))
		{
			ModelState.AddModelError("name", "Введите свое имя!");
		}
		if (!ModelState.IsValid)
		{
			_logger.LogWarning("Заполните поле!");
			return View();
		}
		else
		{
			var emailSender = new EmailSender();
			bool success = emailSender.SendMessage(contact.Email, contact.Message, contact.Name);
			if (success)
			{
				TempData["SuccessMessage"] = "Сообщение успешно отправлено!";
			}
			else
			{
				TempData["ErrorMessage"] = "Отправка сообщений безуспешно!.";
			}

			return RedirectToAction("Contacts");
		}

	}





	[HttpPost]
	public JsonResult ChangeCulture(string culture)
	{
		Response.Cookies.Append(
			CookieRequestCultureProvider.DefaultCookieName,
			CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
			new CookieOptions { Expires = DateTime.Now.AddMonths(1) });

		return Json(culture);
	}




	public IActionResult DebugCulture()
	{
		var culture = CultureInfo.CurrentCulture.Name;
		var uiCulture = CultureInfo.CurrentUICulture.Name;
		return Content($"CurrentCulture: {culture}, CurrentUICulture: {uiCulture}");
	}




	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}






}