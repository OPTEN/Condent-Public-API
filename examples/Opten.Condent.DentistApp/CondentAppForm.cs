using Opten.Condent.DentistApp.Models;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Opten.Condent.DentistApp
{
	public partial class CondentAppForm : Form
	{

		// Contact Condent Software AG for your SecretKey.
		// Please treat this pair of keys as passwords and keep them safe!
		private const string SECRET_KEY = "YOUR_SECRET_KEY";

		private readonly List<string> _tempFileNames = new();

		public CondentAppForm()
		{
			InitializeComponent();

			dtpOrderAppointmentTime.Format = DateTimePickerFormat.Custom;
			dtpOrderAppointmentTime.CustomFormat = "HH:mm";
			dtpOrderAppointmentTime.ShowUpDown = true;
		}

		private async void btnPatientSend_Click(object sender, EventArgs e)
		{
			// disable to prevent double clicks (you might want to show a loading indicator)
			btnPatientSend.Enabled = false;

			var data = BuildPatient();

			var options = GetDefaultOptions();
			options.WriteIndented = true;

			tbJsonOutput.Text = JsonSerializer.Serialize(data, options);

			await SendAsync(data);

			// after everything is done enable the button again
			btnPatientSend.Enabled = true;
		}

		private async void btnOrderSend_Click(object sender, EventArgs e)
		{
			// disable to prevent double clicks (you might want to show a loading indicator)
			btnOrderSend.Enabled = false;

			var data = BuildOrder();

			var options = GetDefaultOptions();
			options.WriteIndented = true;

			tbJsonOutput2.Text = JsonSerializer.Serialize(data, options);

			await SendAsync(data);

			// after everything is done enable the button again
			btnOrderSend.Enabled = true;
		}

		private async Task SendAsync(OrderCreateRequest data)
		{
			try
			{
				// This is just for demo purpose. It's up to you how you want to handle HttpClient, JSON parsing, etc.
				using var httpClient = new HttpClient
				{
					BaseAddress = new Uri(tbSettingsApiUrl.Text.Trim())
				};

				using var request = new HttpRequestMessage(HttpMethod.Post, "v1/order");

				var body = JsonSerializer.Serialize(data, GetDefaultOptions());

				request.Content = new StringContent(body, Encoding.UTF8, "application/json");

				await AddAuthorizationAsync(request, body);

				using var response = await httpClient.SendAsync(request);

				response.EnsureSuccessStatusCode();

				var stream = await response.Content.ReadAsStringAsync();
				var order = JsonSerializer.Deserialize<OrderCreateResponse>(stream, GetDefaultOptions());

				if (string.IsNullOrWhiteSpace(order?.Url))
				{
					// This is an edge case and will probably never happen.
					// You can show a message to the user that the user should try again.
					MessageBox.Show("Es ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut.");
					return;
				}

				// after we got a valid order we can upload the files (if there are any).
				if (_tempFileNames.Any())
				{
					await TryUploadFilesAsync(httpClient, order.UploadToken!);
				}

				// You might need to handle this differently
				Process.Start(new ProcessStartInfo { FileName = order.Url, UseShellExecute = true });
			}
			catch
			{
				// You should show a message to the user that the user should try again.
				// You also might want to look into to returned errors.
				// Possible errors are:
				// - Reference, FirstName or LastName is missing
				// - Wrong API Key or API Key deleted by user
				// - Timestamp not synced
				// - api.condent.app is currently not reachable

				MessageBox.Show("Condent ist momentan nicht erreichbar. Bitte versuchen Sie es später erneut. Falls der Fehler weiterhin auftritt, kontaktieren Sie uns.");
			}
		}

		private async Task TryUploadFilesAsync(HttpClient httpClient, string token)
		{
			try
			{
				var tasks = new List<Task>();

				foreach (var fileName in _tempFileNames)
				{
					// prepare tasks to upload them in parallel (line 154)
					tasks.Add(Task.Run(async () =>
					{
						using var form = new MultipartFormDataContent();

						using var fileStream = File.OpenRead(fileName);
						using var binaryReader = new BinaryReader(fileStream);

						var fileBytes = binaryReader.ReadBytes((int)fileStream.Length);

						form.Add(new ByteArrayContent(fileBytes), "file", fileName);

						using var request = new HttpRequestMessage(HttpMethod.Put, $"v1/order/{token}/upload");

						request.Content = form;

						var stringToSign = fileName;

						await AddAuthorizationAsync(request, stringToSign);

						await httpClient.SendAsync(request);
					}));
				}

				// upload all files in parallel
				await Task.WhenAll(tasks);
			}
			catch
			{
				// We don't care if the files are uploaded because on Condent they are able to upload them manually.
				// But you could add some logging here to investigate why the upload didn't work.
			}

			_tempFileNames.Clear();
			tbFileName.Text = null;
		}

		private async Task AddAuthorizationAsync(HttpRequestMessage request, string stringToSign)
		{
			var apiKey = tbApiKey.Text.Trim();

			// you can also use DateTimeOffset.UtcNow (but be aware that an user can change their clock)
			var timestamp = await GetCondentServerTime();
			//timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

			var secretKey = Encoding.UTF8.GetBytes(SECRET_KEY);
			using var encryptor = new HMACSHA256(secretKey);

			var bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes(timestamp + apiKey + stringToSign));
			var sign = Convert.ToBase64String(bytes);

			request.Headers.Add("X-CONDENT-API-KEY", apiKey);
			request.Headers.Add("X-CONDENT-TIMESTAMP", timestamp.ToString(CultureInfo.InvariantCulture));
			request.Headers.Add("X-CONDENT-SIGN", sign);
		}

		private async Task<long> GetCondentServerTime()
		{
			// This is just for demo purpose. It's up to you how you want to handle HttpClient, JSON parsing, etc.
			using var httpClient = new HttpClient
			{
				BaseAddress = new Uri(tbSettingsApiUrl.Text.Trim())
			};

			using var request = new HttpRequestMessage(HttpMethod.Get, "v1/time");

			var response = await httpClient.SendAsync(request);

			response.EnsureSuccessStatusCode();

			var stream = await response.Content.ReadAsStringAsync();
			var serverTime = JsonSerializer.Deserialize<TimeResponse>(stream, GetDefaultOptions());

			return serverTime!.Milliseconds;
		}

		private void btnCopyToClipboard_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbJsonOutput.Text) == false)
			{
				Clipboard.SetText(tbJsonOutput.Text);
			}
		}

		private void btnCopyToClipboard2_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbJsonOutput2.Text) == false)
			{
				Clipboard.SetText(tbJsonOutput2.Text);
			}
		}

		private OrderCreateRequest BuildPatient()
		{
			var salutation = cbPatientSalutation.SelectedItem?.ToString()?.Trim() == "Frau"
				? "ms"
				: "mr";
			var countryCode = cbAdressCountry.SelectedItem?.ToString()?.Trim() == "Schweiz"
				? "CH"
				: null;

			var insurances = tbPatientInsurance.Text
				?.Trim()
				?.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries)
				?.Select(x => x.Trim()) ?? Array.Empty<string>();

			var costUnitType = cbPatientCostUnitType.SelectedItem?.ToString()?.Trim() switch
			{
				"Privat" => OrderCostUnitType.Private,
				"SUVA / IV / MV" => OrderCostUnitType.Suva,
				"Unfallkrankenkasse" => OrderCostUnitType.AccidentInsurance,
				"Krankenkasse" => OrderCostUnitType.HealthInsurance,
				"EL / Asyl / Sozial" => OrderCostUnitType.Social,
				_ => (OrderCostUnitType?)null
			};

			return new OrderCreateRequest
			{
				CostUnitType = costUnitType,
				Insurances = insurances,
				Patient = new OrderCreatePatientRequest
				{
					Reference = tbPatientReference.Text?.Trim(),
					Salutation = salutation,
					FirstName = tbPatientFirstName.Text?.Trim(),
					LastName = tbPatientLastName.Text?.Trim(),
					Birthday = dtpPatientBirthdate.Value.ToUniversalTime(),
					Email = tbPatientEmail.Text?.Trim(),
					PhoneNumber = tbPatientPhoneNumber.Text?.Trim(),
					Address = new OrderCreatePatientAddressRequest
					{
						Address1 = tbAddress1.Text?.Trim(),
						Address2 = tbAddress2.Text?.Trim(),
						ZipCode = tbAddressZipCode.Text?.Trim(),
						City = tbAddressCity.Text?.Trim(),
						CountryCode = countryCode
					}
				}
			};
		}

		private OrderCreateRequest BuildOrder()
		{
			var request = BuildPatient();

			var appointmentDate = dtpOrderAppointment.Value.Date.Add(dtpOrderAppointmentTime.Value.TimeOfDay);

			request.Appointments = new List<OrderCreateAppointmentRequest>
			{
				new OrderCreateAppointmentRequest
				{
					Date = appointmentDate.ToUniversalTime()
				}
			};

			if (cbOrderAttachment.Checked)
			{
				request.Attachments = new List<OrderCreateAttachmentRequest>
				{
					new OrderCreateAttachmentRequest
					{
						Description = "Bissnahme"
					}
				};
			}

			var scanSentVia = cbOrderScanSentVia.SelectedItem?.ToString()?.Trim() switch
			{
				"Sirona" => OrderScanSentViaType.Sirona,
				"3 Shape" => OrderScanSentViaType.ThreeShape,
				_ => (OrderScanSentViaType?)null
			};

			request.Title = tbOrderTitle.Text?.Trim();
			request.Color = tbOrderColor.Text?.Trim();
			request.Description = tbOrderDescription.Text?.Trim();
			request.Color = tbOrderColor.Text?.Trim();
			request.ScanSentVia = scanSentVia;
			request.TrackingNumber = tbOrderTrackingNumber.Text?.Trim();

			return request;
		}

		private static JsonSerializerOptions GetDefaultOptions()
		{
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			options.Converters.Add(new JsonStringEnumConverter());

			return options;
		}

		private void btnFileUpoad_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					_tempFileNames.AddRange(openFileDialog1.FileNames);
					tbFileName.Text = $"{openFileDialog1.FileNames.Length} Dateien ausgewählt";
				}
				catch (SecurityException ex)
				{
					MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
					$"Details:\n\n{ex.StackTrace}");
				}
			}
		}

	}
}