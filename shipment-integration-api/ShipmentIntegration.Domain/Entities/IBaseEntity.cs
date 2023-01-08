using System;

namespace ShipmentIntegration.Domain.Entities;

public interface IBaseEntity
{
    int Id { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime UpdatedDate { get; set; }
}
