using malefashion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System;
using malefashion_master.Models;




var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
	.AddViewLocalization();



#region Localization

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supported = new[]
	{
		new CultureInfo("kk-KZ"),
		new CultureInfo("ru-RU"),
		new CultureInfo("en-US")

	};

	options.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
	options.SupportedCultures = supported;
	options.SupportedUICultures = supported;
});

#endregion



#region Auth
builder.Services.AddDbContext<AppIdentityDBContext>
	(options => options.UseSqlServer(
		builder.Configuration["ConnectionStrings:DefaultConnection"]));


builder.Services.AddIdentity<AppUser, IdentityRole>()
	.AddEntityFrameworkStores<AppIdentityDBContext>()
	.AddDefaultTokenProviders();
#endregion










#region LocalizeBase
//var serviceProvider = builder.Services.BuildServiceProvider();
//var languageService = serviceProvider.GetService<ILanguageService>();

//var languages = languageService.GetLanguages();
//var culture = languages.Select(x => new CultureInfo(x.Culture)).ToList();
//var defaultCulture = languages.FirstOrDefault(f => f.IsDefaultCulture);

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//	options.DefaultRequestCulture =
//	new RequestCulture(culture: defaultCulture.Culture, uiCulture: defaultCulture.Culture);

//	options.SupportedCultures = culture;
//	options.SupportedUICultures = culture;
//});

#endregion


#region log
Log.Logger = new LoggerConfiguration()
	.WriteTo.Seq("http://localhost:5341")
	.MinimumLevel.Debug()
	.CreateLogger();

builder.Host.UseSerilog();
#endregion




var app = builder.Build();


var supported = new[] { "kk-KZ", "ru-RU", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
	.SetDefaultCulture("ru-RU")
	.AddSupportedCultures(supported)
	.AddSupportedUICultures(supported);

app.UseRequestLocalization(localizationOptions);




var logger = app.Services.GetRequiredService<ILogger<Program>>();
//logger.LogInformation("Supported Cultures: {@Cultures}", localizationOptions.SupportedCultures);
logger.LogInformation("Resource Path: {Path}", builder.Configuration["Resources"]);
logger.LogInformation("Ëîêàëèçàöèÿ çàãðóæåíà: {SupportedCultures}", localizationOptions.SupportedCultures);


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
