namespace ShipmentIntegration.Application.Models;

public class UpdateShipmentRequestVM
{
    public int Id { get; set; }
    public ShipmentStatusType Status { get; set; }
}
