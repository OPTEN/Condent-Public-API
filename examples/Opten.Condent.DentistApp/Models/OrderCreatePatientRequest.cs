namespace Opten.Condent.DentistApp.Models;

public class OrderCreatePatientRequest
{

	public string? Reference { get; set; }

	public string? Salutation { get; set; }

	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public DateTime? Birthday { get; set; }

	public string? Email { get; set; }

	public string? PhoneNumber { get; set; }

	public OrderCreatePatientAddressRequest? Address { get; set; }

}

public class OrderCreatePatientAddressRequest
{

	public string? Address1 { get; set; }

	public string? Address2 { get; set; }

	public string? ZipCode { get; set; }

	public string? City { get; set; }

	public string? CountryCode { get; set; }

}
