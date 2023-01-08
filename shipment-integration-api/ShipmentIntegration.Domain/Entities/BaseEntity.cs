namespace ShipmentIntegration.Domain.Entities;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
