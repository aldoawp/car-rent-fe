using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentCarFrontend.Models;
using RentCarFrontend.Services;

namespace RentCarFrontend.Controllers;

public class HomeController : Controller
{
    private readonly ICar _carApi;

    public HomeController(ILogger<HomeController> logger, ICar carApi)
    {
        _carApi = carApi;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> GetAllCars(int year)
    {
        var result = await _carApi.GetAllCars(year);
        return Json(result);
    }

    public async Task<IActionResult> GetCar(int pageNumber, int pageContent, string sort, int year)
    {
        var result = await _carApi.GetCar(pageNumber, pageContent, sort, year);
        return Json(result);
    }

    public async Task<IActionResult> GetYears()
    {
        var result = await _carApi.GetYears();
        return Json(result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
