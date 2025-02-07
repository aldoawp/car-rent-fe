using System;

namespace RentCarFrontend.Models.Output;

public class ApiResponse<T>
{
  public int StatusCode { get; set; }
  public string RequestMethod { get; set; }
  public T Payload { get; set; }
}
