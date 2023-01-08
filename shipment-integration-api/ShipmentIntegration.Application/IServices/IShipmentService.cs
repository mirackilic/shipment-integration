using ShipmentIntegration.Application.Models;

namespace ShipmentIntegration.Application.IServices;

public interface IShipmentService
{
    List<GetShipmentsResponseVM> GetAll();
    Task<CreateShipmentResponseVM> CreateAsync(List<CreateShipmentRequestVM> requestVM);
    Task<int> UpdateStatusAsync(UpdateShipmentRequestVM requestVM);
}
