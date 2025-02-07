using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentCarFrontend.Models.Input;
using RentCarFrontend.Services;

namespace RentCarFrontend.Controllers;

public class RentalController : Controller
{
  private readonly IRental _rentalApi;

  public RentalController(IRental rentalApi)
  {
    _rentalApi = rentalApi;
  }

  public ActionResult Index()
  {
    return View();
  }
  
  public async Task<IActionResult> GetAllRentalID()
  {
    var result = await _rentalApi.GetAllRentalID();
    return Json(result);
  }

  async public Task<ActionResult> GetRentHistories(string custID)
  {
    var response = await _rentalApi.GetRentHistories(custID);
    return Json(response);
  }

  public async Task<IActionResult> CreateRent([FromBody] CreateRentalInput request)
  {
    if (request == null)
    {
      return BadRequest("No payload");
    }
    var response = await _rentalApi.CreateRent(request);
    return Json(response);
  }
}
