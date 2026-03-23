namespace Opten.Condent.DentistApp.Models;

public class OrderCreateResponse
{

	public string? Url { get; set; }

	public string? UploadToken { get; set; }

	public IEnumerable<string>? Warnings { get; set; }

}
