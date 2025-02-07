using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentCarFrontend.Models.Input;
using RentCarFrontend.Models.Output;
using RentCarFrontend.Services;

namespace RentCarFrontend.Handlers;

public class CustomerHandler : ICustomer
{
  private readonly IConfiguration _configuration;
  private readonly string baseUrl;
  private HttpClient httpClient = new HttpClient();

  
  public CustomerHandler(IConfiguration configuration) {
    _configuration = configuration;
    baseUrl = _configuration["apiEndpoint"];
  }

  public async Task<List<string>> GetAllCustomerID()
  {
    var custIdOutput = new List<string>();

    var endpoint = baseUrl + "Customer/GetAllCustomerID";
    var response = await httpClient.GetAsync(endpoint);
    var apiResponse = await response.Content.ReadAsStringAsync();

    if (!string.IsNullOrEmpty(apiResponse))
    {
      custIdOutput = JsonConvert.DeserializeObject<List<string>>(apiResponse);
    }

    return custIdOutput;
  }

  public async Task<ApiResponse<string>> CreateCustomer(CreateCustomerInput request)
  {
    if (request == null)
    {
      return new ApiResponse<string>
            {
                StatusCode = 400,
                Payload = "Bad Request"
            };
    }

    string endpoint = baseUrl + "Customer";
    var response = await httpClient.PostAsJsonAsync(endpoint, request);
    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
  
    return apiResponse;
  }

  public async Task<ApiResponse<GetCustomerOutput>> Login(CreateLoginInput request)
  {
    string endpoint = baseUrl + "Customer/Login";
    
    var response = await httpClient.PutAsJsonAsync(endpoint, request);
    
    if (!response.IsSuccessStatusCode)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"Login failed: {errorContent}");
    }

    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<GetCustomerOutput>>();

    if (apiResponse == null)
    {
        throw new Exception("Failed to deserialize the API response.");
    }
    
    return apiResponse;
  }

  public async Task<ApiResponse<string>> Logout(CreateLogoutInput request)
  {
    string endpoint = baseUrl + "Customer/Logout";
    
    var response = await httpClient.PutAsJsonAsync(endpoint, request);
    
    if (!response.IsSuccessStatusCode)
    {
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"Logout Failed: {errorContent}");
    }

    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

    if (apiResponse == null)
    {
        throw new Exception("Failed to deserialize the API response.");
    }
    
    return apiResponse;
  }
}










  // public async Task<ApiResponse<GetCustomerOutput>> GetCustomerDetail()
  // {
  //   var customerOutput = new ApiResponse<GetCustomerOutput>();

  //   string endpoint = baseUrl + "Customer";
  //   var response = await httpClient.GetAsync(endpoint);
  //   var apiResponse = await response.Content.ReadAsStringAsync();

  //   if (!string.IsNullOrEmpty(apiResponse))
  //   {
  //     customerOutput = JsonConvert.DeserializeObject<ApiResponse<GetCustomerOutput>>(apiResponse);
  //   }

  //   return customerOutput;
  // }