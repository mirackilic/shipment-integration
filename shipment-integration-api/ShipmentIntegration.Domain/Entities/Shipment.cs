using ShipmentIntegration.Domain.Entities;

//Sipariş
public class Shipment : BaseEntity
{
    // Müşteri Sipariş No
    public string ReferenceNumber { get; set; }

    // Çıkış Adresi
    public string FromAddress { get; set; }

    // Varış Adresi
    public string ToAddress { get; set; }

    // Siparişin Durumu
    public ShipmentStatusType Status { get; set; }

    // Miktar
    public int Quantity { get; set; }

    // Miktar Birim
    public QuantityUnit QuantityUnit { get; set; }

    // Ağırlık
    public int Weight { get; set; }

    // Ağırlık Birim
    public WeightType WeightType { get; set; }

    // Not
    public string Note { get; set; }
    public int MaterialId { get; set; }


    public virtual Material Material { get; set; }
}