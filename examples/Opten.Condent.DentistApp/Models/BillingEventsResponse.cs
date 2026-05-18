using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp.Models;

public class BillingEventsResponse
{
    [JsonPropertyName("events")]
    public List<BillingEvent>? Events { get; set; }

    [JsonPropertyName("nextCursor")]
    public string? NextCursor { get; set; }

    [JsonPropertyName("hasMore")]
    public bool HasMore { get; set; }
}
