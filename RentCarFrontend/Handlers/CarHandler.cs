using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RentCarFrontend.Models.Output;
using RentCarFrontend.Services;

namespace RentCarFrontend.Handlers;

public class CarHandler : ICar
{
  private readonly IConfiguration _configuration;
  private readonly string baseUrl;
  private HttpClient httpClient = new HttpClient();

  public CarHandler(IConfiguration configuration) {
    _configuration = configuration;
    baseUrl = _configuration["apiEndpoint"];
  }

  public async Task<ApiResponse<IEnumerable<GetCarOutput>>> GetAllCars(int year) {
    string endpoint = $"{baseUrl}Car/GetAllCars?year={year}";
    var carOutput = new ApiResponse<IEnumerable<GetCarOutput>>();

    var response = await httpClient.GetAsync(endpoint);
    string apiResponse = await response.Content.ReadAsStringAsync();

    if (!string.IsNullOrEmpty(apiResponse))
    {
      carOutput = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetCarOutput>>>(apiResponse);
    }

    return carOutput;
  }

  public async Task<ApiResponse<IEnumerable<GetCarOutput>>> GetCar(int pageNumber, int pageContent, string sort, int year) {
    string endpoint = $"{baseUrl}Car?pageNumber={pageNumber}&pageContent={pageContent}&sort={sort}&year={year}";
    var carOutput = new ApiResponse<IEnumerable<GetCarOutput>>();

    var response = await httpClient.GetAsync(endpoint);
    string apiResponse = await response.Content.ReadAsStringAsync();

    if (!string.IsNullOrEmpty(apiResponse))
    {
      carOutput = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetCarOutput>>>(apiResponse);
    }

    return carOutput;
  }

  public async Task<ApiResponse<GetCarOutput>> GetCarDetail(string id)
  {
    string endpoint = baseUrl + "Car/" + id;
    
    var carOutput = new ApiResponse<GetCarOutput>();

    var response = await httpClient.GetAsync(endpoint);
    var apiResponse = await response.Content.ReadAsStringAsync();

    if (!string.IsNullOrEmpty(apiResponse))
    {
      carOutput = JsonConvert.DeserializeObject<ApiResponse<GetCarOutput>>(apiResponse);
    }

    return carOutput;
  }

  public async Task<ApiResponse<IEnumerable<GetYearOutput>>> GetYears()
  {
    string endpoint = baseUrl + "Car/GetYears";

    var yearOutput = new ApiResponse<IEnumerable<GetYearOutput>>();

    var response = await httpClient.GetAsync(endpoint);
    var apiResponse = await response.Content.ReadAsStringAsync();

    if (!string.IsNullOrEmpty(apiResponse))
    {
      yearOutput = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<GetYearOutput>>>(apiResponse);
    }

    return yearOutput;
  }
}
