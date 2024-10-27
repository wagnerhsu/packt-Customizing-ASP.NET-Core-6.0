using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConfigureSample.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Dumpify;

namespace ConfigureSample.Controllers;

public class HomeController : Controller
{
    private readonly AppSettings _options;
    private readonly RemoteTcpServerSettings _remoteTcpServerSettings;
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(
        IOptions<AppSettings> options,
        IOptions<RemoteTcpServerSettings> remoteTcpServerSettings,
        ILogger<HomeController> logger,
        IConfiguration configuration)
    {
        _options = options.Value;
        _logger = logger;
        _configuration = configuration;

        _remoteTcpServerSettings = remoteTcpServerSettings.Value;

    }

    public IActionResult Index()
    {
        ViewData["Message"] = _options.Bar;
        _remoteTcpServerSettings.Dump(nameof(RemoteTcpServerSettings));
        _configuration.GetValue<AppSettings>("AppSettings").Dump("GetValue<AppSettings>");
        _configuration.GetValue<int>("AppSettings:Foo").Dump("AppSettings:Foo");
        _configuration.GetSection("AppSettings").Get<AppSettings>().Dump(nameof(AppSettings));
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
