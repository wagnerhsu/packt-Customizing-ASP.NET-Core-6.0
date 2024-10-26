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


    public HomeController(
        IOptions<AppSettings> options, IOptions<RemoteTcpServerSettings> remoteTcpServerSettings, ILogger<HomeController> logger)
    {
        _options = options.Value;
        this._logger = logger;
        _remoteTcpServerSettings = remoteTcpServerSettings.Value;

    }

    public IActionResult Index()
    {
        ViewData["Message"] = _options.Bar;
        _remoteTcpServerSettings.Dump(nameof(RemoteTcpServerSettings));
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
