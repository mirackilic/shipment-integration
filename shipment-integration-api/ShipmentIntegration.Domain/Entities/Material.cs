namespace ShipmentIntegration.Domain.Entities;

// Malzeme
public class Material : BaseEntity
{
    // Malzeme Kodu
    public string MaterialCode { get; set; }

    // Malzeme Adı
    public string MaterialName { get; set; }

    public virtual ICollection<Shipment> Shipments { get; set; }
}
