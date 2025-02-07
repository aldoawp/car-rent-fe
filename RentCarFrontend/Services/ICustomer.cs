using System;
using Microsoft.AspNetCore.Mvc;
using RentCarFrontend.Models.Input;
using RentCarFrontend.Models.Output;

namespace RentCarFrontend.Services;

public interface ICustomer
{
  Task<List<string>> GetAllCustomerID();
  Task<ApiResponse<string>> CreateCustomer(CreateCustomerInput request);
  Task<ApiResponse<GetCustomerOutput>> Login(CreateLoginInput request);
  Task<ApiResponse<string>> Logout(CreateLogoutInput request);
}
