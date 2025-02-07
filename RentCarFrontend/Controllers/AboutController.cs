using System;
using Microsoft.AspNetCore.Mvc;

namespace RentCarFrontend.Controllers;

public class AboutController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}
