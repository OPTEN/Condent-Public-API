using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp.Models;

public class BillingDocument
{
    [JsonPropertyName("key")]
    public Guid Key { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("createDate")]
    public DateTime CreateDate { get; set; }

    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }
}
