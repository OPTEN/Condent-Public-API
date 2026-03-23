namespace Opten.Condent.DentistApp.Models;

public class OrderCreateRequest
{

	public string? Title { get; set; }

	public OrderCostUnitType? CostUnitType { get; set; }

	public IEnumerable<string>? Insurances { get; set; }

	public string? Color { get; set; }

	public string? Description { get; set; }

	public OrderScanSentViaType? ScanSentVia { get; set; }

	public string? TrackingNumber { get; set; }

	public OrderCreatePatientRequest? Patient { get; set; }

	public IEnumerable<OrderCreateAppointmentRequest>? Appointments { get; set; }

	public IEnumerable<OrderCreateAttachmentRequest>? Attachments { get; set; }

	//[JsonIgnore]
	//public List<IFormFile> Uploads { get; set; } = new();

}


internal class SwaggerCreateRequest
{

	public OrderCreateRequest? Request { get; set; }

}
