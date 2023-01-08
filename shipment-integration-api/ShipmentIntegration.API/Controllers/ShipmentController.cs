using Microsoft.AspNetCore.Mvc;
using ShipmentIntegration.Application.IServices;
using ShipmentIntegration.Application.Models;

namespace ShipmentIntegration.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShipmentController : ControllerBase
{
    private readonly IShipmentService _shipmentService;

    public ShipmentController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var result = _shipmentService.GetAll();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(List<CreateShipmentRequestVM> requestVM)
    {
        var result = await _shipmentService.CreateAsync(requestVM);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateShipmentRequestVM requestVM)
    {
        if (requestVM.Id <= 0)
            throw new ApplicationException("Id is required!");

        var result = await _shipmentService.UpdateStatusAsync(requestVM);

        return Ok(result);
    }
}
