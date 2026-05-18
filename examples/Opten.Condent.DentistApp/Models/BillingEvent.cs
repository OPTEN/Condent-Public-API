using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp.Models;

public class BillingEvent
{
    [JsonPropertyName("eventId")]
    public long EventId { get; set; }

    [JsonPropertyName("eventType")]
    public BillingEventType EventType { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("data")]
    public BillingEventData? Data { get; set; }
}
