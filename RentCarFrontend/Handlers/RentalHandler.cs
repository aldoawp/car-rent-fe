using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentCarFrontend.Models.Input;
using RentCarFrontend.Models.Output;
using RentCarFrontend.Services;

namespace RentCarFrontend.Handlers;

public class RentalHandler : IRental
{
  private readonly IConfiguration _configuration;
  private readonly string baseUrl;
  private readonly HttpClient httpClient = new HttpClient();

  public RentalHandler(IConfiguration configuration) {
    _configuration = configuration;
    baseUrl = _configuration["apiEndpoint"];
  }

  public async Task<List<string>> GetAllRentalID()
  {
    var rentIdOutput = new List<string>();

    var endpoint = baseUrl + "Rental/GetAllRentalID";
    var response = await httpClient.GetAsync(endpoint);
    var apiResponse = await response.Content.ReadAsStringAsync();

    if (!string.IsNullOrEmpty(apiResponse))
    {
      rentIdOutput = JsonConvert.DeserializeObject<List<string>>(apiResponse);
    }

    return rentIdOutput;
  }

  async public Task<ApiResponse<IEnumerable<GetRentalOutput>>> GetRentHistories(string custID)
  {
    string endpoint = baseUrl + "Rental/" + custID;
    
    try 
    {
      var response = await httpClient.GetAsync(endpoint);
      if(!response.IsSuccessStatusCode)
      {
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Request failed with status code {response.StatusCode}. Details: {errorContent}");
      }

      var result = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<GetRentalOutput>>>();
      if (result == null)
      {
        throw new InvalidOperationException("Failed to deserialize the response content.");
      }
      return result;
    }
    catch (HttpRequestException httpReqEx)
    {
      throw new Exception("Network or server error occurred while fetching rental histories: " + httpReqEx.Message, httpReqEx);
    }
    catch (InvalidOperationException invalidOpsEx)
    {
      throw new Exception("Invalid operation error while deserializing response: " + invalidOpsEx, invalidOpsEx);
    }
    catch (Exception ex)
    {
      throw new Exception("An unexpected error occurred while fetching rental histories: " + ex.Message, ex);
    }
  }

  async public Task<ApiResponse<string>> CreateRent([FromBody] CreateRentalInput request)
  {
    // Check if the request is correct or not
    if (request == null)
    {
        return new ApiResponse<string>
        {
            StatusCode = 400,
            RequestMethod = "POST",
            Payload = "{\"message\": \"Bad Request\"}"
        };
    }

    string endpoint = baseUrl + "Rental";

    try
    {
        var response = await httpClient.PostAsJsonAsync(endpoint, request);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
            return apiResponse;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new ApiResponse<string>
            {
                StatusCode = (int)response.StatusCode,
                RequestMethod = "POST",
                Payload = $"{{\"error\": \"{errorContent}\"}}"
            };
        }
    }
    catch (Exception ex)
    {
        return new ApiResponse<string>
        {
            StatusCode = 500,
            RequestMethod = "POST",
            Payload = $"{{\"error\": \"{ex.Message}\"}}"
        };
    }
  }

}
