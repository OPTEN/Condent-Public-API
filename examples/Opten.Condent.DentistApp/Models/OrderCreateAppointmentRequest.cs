namespace Opten.Condent.DentistApp.Models;

public class OrderCreateAppointmentRequest
{

	public DateTime? Date { get; set; }

	public OrderAppointmentType? Topic { get; set; }

	public string? Description { get; set; }

}
