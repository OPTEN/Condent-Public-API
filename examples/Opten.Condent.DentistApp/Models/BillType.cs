using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BillType
{
    Unknown,
    Order,
    Invoice,
    DeliveryBill,
    CostEstimate,
    Credit
}
