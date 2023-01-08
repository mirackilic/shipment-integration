namespace ShipmentIntegration.API.Models;

public class ApiResult
{
    public int ShipmentId { get; set; }
    public string ReferenceNumber { get; set; }
    public bool IsSuccess { get; set; } // true : başarılı , false : başarısızs.
    public string ErrorMessage { get; set; }
    public object Data { get; set; }
}
