namespace ShipmentIntegration.Application.Models;

public class UpdateShipmentResponseVM
{
    public int Id { get; set; }
    public string ReferenceNumber { get; set; }
    public bool IsSuccess { get; set; } = true; // true : başarılı , false : başarısızs.
    public string ErrorMessage { get; set; }
}
