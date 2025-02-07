using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using RentCarFrontend.Models.Output;
using RentCarFrontend.Services;

namespace RentCarFrontend.Controllers;

public class CarController : Controller
{
  private readonly ICar _carApi;

  public CarController(ICar carApi)
  {
    _carApi = carApi;
  }

  public async Task<IActionResult> Index(string carID, string pickUpDate, string returnDate)
  {
    var result = await _carApi.GetCarDetail(carID);
    
    ViewData["CarName"] = result.Payload.Name;
    ViewData["CarModel"] = result.Payload.Model;
    ViewData["CarYear"] = result.Payload.Year;
    ViewData["CarLicensePlate"] = result.Payload.LicensePlate;
    ViewData["CarNumberOfCarSeats"] = result.Payload.NumberOfCarSeats;
    ViewData["CarTransmission"] = result.Payload.Transmission;
    ViewData["CarPricePerDay"] = string.Format("{0:C}", result.Payload.PricePerDay);
    ViewData["CarStatus"] = result.Payload.Status;
    
    string dateFormat = "yyyy-MM-dd";
    DateTime cPickUpDate = DateTime.ParseExact(pickUpDate, dateFormat, CultureInfo.InvariantCulture);
    DateTime cReturnDate = DateTime.ParseExact(returnDate, dateFormat, CultureInfo.InvariantCulture);
    string formattedPickUpDate = cPickUpDate.ToString("dd MMMM yyyy");
    string formattedReturnDate = cReturnDate.ToString("dd MMMM yyyy");
    var dateDiff = cReturnDate - cPickUpDate;
    var totalPrice = result.Payload.PricePerDay * dateDiff.Days;

    ViewData["pickUpDate"] = formattedPickUpDate;
    ViewData["returnDate"] = formattedReturnDate;
    ViewData["totalPrice"] = string.Format("{0:C}", totalPrice);
    
    return View();
  }

  public async Task<IActionResult> GetCarDetail(string carID)
  {
    var result = await _carApi.GetCarDetail(carID);
    return Json(result);
  }
}
