namespace ShipmentIntegration.Application.Models;

public class UpdateShipmentStatusRequestVM
{
    // Müşteri Sipariş No
    public string ReferenceNumber { get; set; }

    // Statü
    public ShipmentStatusType Status { get; set; }

    // Değişim Tarihi
    public DateTime UpdatedDate { get; set; }
}
