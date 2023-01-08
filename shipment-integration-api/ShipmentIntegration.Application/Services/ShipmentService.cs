using ShipmentIntegration.Application.Helpers;
using ShipmentIntegration.Application.IServices;
using ShipmentIntegration.Application.Models;
using ShipmentIntegration.Domain.Entities;
using ShipmentIntegration.Domain.IRepositories;
using System.Text.Json;

namespace ShipmentIntegration.Application.Services;

public class ShipmentService : IShipmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public ShipmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<GetShipmentsResponseVM> GetAll()
    {
        return _unitOfWork.GetRepository<Shipment>().GetAll().
        Select(x => new GetShipmentsResponseVM
        {
            Id = x.Id,
            ReferenceNumber = x.ReferenceNumber,
            FromAddress = x.FromAddress,
            ToAddress = x.ToAddress,
            Quantity = x.Quantity,
            QuantityUnit = x.QuantityUnit,
            Weight = x.Weight,
            WeightType = x.WeightType,
            MaterialCode = x.Material.MaterialCode,
            MaterialName = x.Material.MaterialName,
            Note = x.Note
        }).ToList();
    }

    public async Task<CreateShipmentResponseVM> CreateAsync(List<CreateShipmentRequestVM> requestVM)
    {
        var materialCodes = requestVM.Select(x => x.MaterialCode).ToList();

        var materials = _unitOfWork.GetRepository<Material>().Find(x => materialCodes.Contains(x.MaterialCode)).ToList();

        var response = new CreateShipmentResponseVM();

        foreach (var item in requestVM)
        {
            var checkShipment = _unitOfWork.GetRepository<Shipment>().FindOne(x => x.ReferenceNumber == item.ReferenceNumber);

            if (checkShipment is not null)
            {
                response.ErrorMessage += $"Referenced Shipments already exist! => {item.ReferenceNumber} \n ";
                continue;
            }

            var shipment = MapShipmentHelper(item);

            shipment.Status = ShipmentStatusType.Received;

            //* Material check and add to table if not exist
            var checkMaterial = materials.FirstOrDefault(x => x.MaterialCode == item.MaterialCode);

            if (checkMaterial is not null)
                shipment.Material = checkMaterial;

            await _unitOfWork.GetRepository<Shipment>().CreateAsync(shipment);

            if (response.ReferenceNumbers is null)
                response.ReferenceNumbers = new List<string>();
            if (response.ShipmentIds is null)
                response.ShipmentIds = new List<int>();

            await _unitOfWork.SaveChangesAsync();

            response.ShipmentIds.Add(shipment.Id);
            response.ReferenceNumbers.Add(shipment.ReferenceNumber);
        }

        if (response.ShipmentIds == null || response.ShipmentIds.Count == 0)
            response.IsSuccess = false;

        return response;
    }

    public async Task<int> UpdateStatusAsync(UpdateShipmentRequestVM requestVM)
    {
        var shipment = await _unitOfWork.GetRepository<Shipment>().GetByIdAsync(requestVM.Id);

        if (shipment is null)
            throw new ApplicationException("Shipment is not exist!");

        shipment.Status = requestVM.Status;

        _unitOfWork.GetRepository<Shipment>().Update(shipment);

        var updatedShipmentId = await _unitOfWork.SaveChangesAsync();

        #region Send request to Update Shipment status at Integrated Company

        var requestModel = new UpdateShipmentStatusRequestVM
        {
            ReferenceNumber = shipment.ReferenceNumber,
            Status = shipment.Status,
            UpdatedDate = shipment.UpdatedDate
        };

        var requestBody = JsonSerializer.Serialize<UpdateShipmentStatusRequestVM>(requestModel);

        RequestHelper.SendRequestHelper(RestSharp.Method.PUT, "http://api.xx.com/statu", requestBody);

        #endregion

        return updatedShipmentId;
    }

    #region Helpers

    private Shipment MapShipmentHelper(CreateShipmentRequestVM model)
    {
        var shipment = new Shipment
        {
            FromAddress = model.FromAddress,
            ToAddress = model.ToAddress,
            ReferenceNumber = model.ReferenceNumber,
            Quantity = model.Quantity,
            QuantityUnit = model.QuantityUnit,
            Weight = model.Weight,
            WeightType = model.WeightType,
            Note = model.Note,
            Material = new Material
            {
                MaterialCode = model.MaterialCode,
                MaterialName = model.MaterialName
            }
        };

        return shipment;
    }

    #endregion
}