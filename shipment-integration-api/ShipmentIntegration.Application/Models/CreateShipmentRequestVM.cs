namespace ShipmentIntegration.Application.Models;

public class CreateShipmentRequestVM
{
    // Müşteri Sipariş No
    public string ReferenceNumber { get; set; }

    // Çıkış Adresi
    public string FromAddress { get; set; }

    // Varış Adresi
    public string ToAddress { get; set; }

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

    // Malzeme Kodu
    public string MaterialCode { get; set; }

    // Malzeme Adı
    public string MaterialName { get; set; }
}
