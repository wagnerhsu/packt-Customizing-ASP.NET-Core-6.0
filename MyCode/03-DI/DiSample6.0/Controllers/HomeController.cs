﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DiSample.Models;

namespace DiSample.Controllers;

public class HomeController : Controller
{
    private readonly IService _service;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IService service)
    {
        _service = service;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var persons = _service.AllPersons();
        return View(persons);
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
