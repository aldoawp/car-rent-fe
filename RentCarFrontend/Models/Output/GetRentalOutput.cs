using System;

namespace RentCarFrontend.Models.Output;

public class GetRentalOutput
{
  public DateTime RentalDate { get; set; }
  public DateTime ReturnDate { get; set; }
  public decimal TotalPrice { get; set; }
  public bool PaymentStatus { get; set; }
  public string CarID { get; set; }
}
