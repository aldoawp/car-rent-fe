using System;
using RentCarFrontend.Models.Input;
using RentCarFrontend.Models.Output;

namespace RentCarFrontend.Services;

public interface IRental
{
  Task<List<string>> GetAllRentalID();
  Task<ApiResponse<IEnumerable<GetRentalOutput>>> GetRentHistories(string custID);
  Task<ApiResponse<string>> CreateRent(CreateRentalInput request);
}
