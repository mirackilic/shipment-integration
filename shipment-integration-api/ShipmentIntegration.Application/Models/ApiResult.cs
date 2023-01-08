namespace ShipmentIntegration.Application.Models;

public class CreateShipmentResponseVM
{
    public List<int> ShipmentIds { get; set; }
    public List<string> ReferenceNumbers { get; set; }
    public bool IsSuccess { get; set; } = true; // true : başarılı , false : başarısızs.
    public string ErrorMessage { get; set; }
    public object Data { get; set; }
}
