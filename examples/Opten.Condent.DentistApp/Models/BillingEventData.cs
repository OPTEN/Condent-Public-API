using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp.Models;

public class BillingEventData
{
    [JsonPropertyName("key")]
    public Guid Key { get; set; }

    [JsonPropertyName("patientId")]
    public int PatientId { get; set; }

    [JsonPropertyName("patientReference")]
    public string? PatientReference { get; set; }

    [JsonPropertyName("reference")]
    public string? Reference { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("type")]
    public BillType Type { get; set; }

    [JsonPropertyName("currencySymbol")]
    public string? CurrencySymbol { get; set; }

    [JsonPropertyName("totalPrice")]
    public double TotalPrice { get; set; }

    [JsonPropertyName("patientUrl")]
    public string? PatientUrl { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("createDate")]
    public DateTime CreateDate { get; set; }

    [JsonPropertyName("updateDate")]
    public DateTime UpdateDate { get; set; }

    [JsonPropertyName("documents")]
    public List<BillingDocument>? Documents { get; set; }
}
