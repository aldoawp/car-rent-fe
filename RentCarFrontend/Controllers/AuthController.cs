using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentCarFrontend.Models.Input;
using RentCarFrontend.Services;

namespace RentCarFrontend.Controllers;

public class AuthController : Controller
{
  private readonly ICustomer _customerApi;

  public AuthController(ICustomer customerApi)
  {
    _customerApi = customerApi;
  }

  public IActionResult Login()
  {
      return View();
  }

  public IActionResult Register()
  {
      return View();
  }

  public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerInput request)
  {
    var result = await _customerApi.CreateCustomer(request);
    return Json(result);
  }

  public async Task<IActionResult> GetAllCustomerID()
  {
    var result = await _customerApi.GetAllCustomerID();
    return Json(result);
  }

  public async Task<IActionResult> UserLogin([FromBody] CreateLoginInput request)
  {
    var result = await _customerApi.Login(request);
    return Json(result);
  }

  public async Task<IActionResult> UserLogout([FromBody] CreateLogoutInput request)
  {
    var result = await _customerApi.Logout(request);
    return Json(result);
  }
}
