using System;
using RentCarFrontend.Models.Output;

namespace RentCarFrontend.Services;

public interface ICar
{
  Task<ApiResponse<IEnumerable<GetCarOutput>>> GetAllCars(int year);
  Task<ApiResponse<IEnumerable<GetCarOutput>>> GetCar(int pageNumber, int pageContent, string sort, int year);
  Task<ApiResponse<GetCarOutput>> GetCarDetail(string id);
  Task<ApiResponse<IEnumerable<GetYearOutput>>> GetYears();
}
