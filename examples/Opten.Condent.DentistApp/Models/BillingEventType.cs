using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BillingEventType
{
    Created,
    Updated,
    Deleted
}
