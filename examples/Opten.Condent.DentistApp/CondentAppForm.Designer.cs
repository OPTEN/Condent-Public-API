namespace Opten.Condent.DentistApp
{
	partial class CondentAppForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tpSettings = new TabPage();
			btnSettingsSave = new Button();
			lblUrl = new Label();
			tbSettingsApiUrl = new TextBox();
			tbApiKey = new TextBox();
			lblApiKey = new Label();
			tpOrder = new TabPage();
			btnFileUpoad = new Button();
			tbFileName = new TextBox();
			lblFileNamen = new Label();
			cbOrderAttachment = new CheckBox();
			lblOrderAttachment = new Label();
			tbJsonOutput2 = new TextBox();
			btnCopyToClipboard2 = new Button();
			dtpOrderAppointmentTime = new DateTimePicker();
			dtpOrderAppointment = new DateTimePicker();
			lblOrderAppointment = new Label();
			cbOrderScanSentVia = new ComboBox();
			label8 = new Label();
			tbOrderColor = new TextBox();
			tbOrderTrackingNumber = new TextBox();
			tbOrderDescription = new TextBox();
			tbOrderTitle = new TextBox();
			label7 = new Label();
			label4 = new Label();
			label1 = new Label();
			label2 = new Label();
			lblOrderTitle = new Label();
			btnOrderSend = new Button();
			label6 = new Label();
			tpPatient = new TabPage();
			tbPatientInsurance = new TextBox();
			cbPatientCostUnitType = new ComboBox();
			lblPatientInsurance = new Label();
			lblPatientCostUnitType = new Label();
			cbAdressCountry = new ComboBox();
			tbAddress2 = new TextBox();
			tbAddressZipCode = new TextBox();
			tbAddress1 = new TextBox();
			tbAddressCity = new TextBox();
			tbPatientEmail = new TextBox();
			tbPatientPhoneNumber = new TextBox();
			tbPatientFirstName = new TextBox();
			tbPatientReference = new TextBox();
			tbJsonOutput = new TextBox();
			tbPatientLastName = new TextBox();
			label12 = new Label();
			label14 = new Label();
			label15 = new Label();
			label13 = new Label();
			label16 = new Label();
			lblPatientEmail = new Label();
			cbPatientSalutation = new ComboBox();
			lblPatientPhonenumber = new Label();
			lblPatientFirstName = new Label();
			lblPatientlReference = new Label();
			btnCopyToClipboard = new Button();
			dtpPatientBirthdate = new DateTimePicker();
			lblPatientLastName = new Label();
			btnPatientSend = new Button();
			lblPatientBirthdate = new Label();
			lblPatientSalutation = new Label();
			tcDefault = new TabControl();
			openFileDialog1 = new OpenFileDialog();
			tpSettings.SuspendLayout();
			tpOrder.SuspendLayout();
			tpPatient.SuspendLayout();
			tcDefault.SuspendLayout();
			SuspendLayout();
			// 
			// tpSettings
			// 
			tpSettings.Controls.Add(btnSettingsSave);
			tpSettings.Controls.Add(lblUrl);
			tpSettings.Controls.Add(tbSettingsApiUrl);
			tpSettings.Controls.Add(tbApiKey);
			tpSettings.Controls.Add(lblApiKey);
			tpSettings.Location = new Point(4, 24);
			tpSettings.Name = "tpSettings";
			tpSettings.Padding = new Padding(3);
			tpSettings.Size = new Size(1131, 661);
			tpSettings.TabIndex = 2;
			tpSettings.Text = "Einstellungen";
			tpSettings.UseVisualStyleBackColor = true;
			// 
			// btnSettingsSave
			// 
			btnSettingsSave.Location = new Point(142, 95);
			btnSettingsSave.Name = "btnSettingsSave";
			btnSettingsSave.Size = new Size(200, 50);
			btnSettingsSave.TabIndex = 4;
			btnSettingsSave.Text = "Speichern";
			btnSettingsSave.UseVisualStyleBackColor = true;
			// 
			// lblUrl
			// 
			lblUrl.AutoSize = true;
			lblUrl.Location = new Point(23, 72);
			lblUrl.Name = "lblUrl";
			lblUrl.Size = new Size(28, 15);
			lblUrl.TabIndex = 3;
			lblUrl.Text = "URL";
			// 
			// tbSettingsApiUrl
			// 
			tbSettingsApiUrl.Location = new Point(142, 66);
			tbSettingsApiUrl.Name = "tbSettingsApiUrl";
			tbSettingsApiUrl.Size = new Size(516, 23);
			tbSettingsApiUrl.TabIndex = 2;
			tbSettingsApiUrl.Text = "https://api-dev.condent.app";
			// 
			// tbApiKey
			// 
			tbApiKey.Location = new Point(142, 37);
			tbApiKey.Name = "tbApiKey";
			tbApiKey.Size = new Size(516, 23);
			tbApiKey.TabIndex = 0;
			tbApiKey.Text = "GKDqbaxYyZMw7m34ui";
			// 
			// lblApiKey
			// 
			lblApiKey.AutoSize = true;
			lblApiKey.Location = new Point(23, 43);
			lblApiKey.Name = "lblApiKey";
			lblApiKey.Size = new Size(47, 15);
			lblApiKey.TabIndex = 1;
			lblApiKey.Text = "API Key";
			// 
			// tpOrder
			// 
			tpOrder.Controls.Add(btnFileUpoad);
			tpOrder.Controls.Add(tbFileName);
			tpOrder.Controls.Add(lblFileNamen);
			tpOrder.Controls.Add(cbOrderAttachment);
			tpOrder.Controls.Add(lblOrderAttachment);
			tpOrder.Controls.Add(tbJsonOutput2);
			tpOrder.Controls.Add(btnCopyToClipboard2);
			tpOrder.Controls.Add(dtpOrderAppointmentTime);
			tpOrder.Controls.Add(dtpOrderAppointment);
			tpOrder.Controls.Add(lblOrderAppointment);
			tpOrder.Controls.Add(cbOrderScanSentVia);
			tpOrder.Controls.Add(label8);
			tpOrder.Controls.Add(tbOrderColor);
			tpOrder.Controls.Add(tbOrderTrackingNumber);
			tpOrder.Controls.Add(tbOrderDescription);
			tpOrder.Controls.Add(tbOrderTitle);
			tpOrder.Controls.Add(label7);
			tpOrder.Controls.Add(label4);
			tpOrder.Controls.Add(label1);
			tpOrder.Controls.Add(label2);
			tpOrder.Controls.Add(lblOrderTitle);
			tpOrder.Controls.Add(btnOrderSend);
			tpOrder.Controls.Add(label6);
			tpOrder.Location = new Point(4, 24);
			tpOrder.Name = "tpOrder";
			tpOrder.Padding = new Padding(3);
			tpOrder.Size = new Size(1131, 661);
			tpOrder.TabIndex = 1;
			tpOrder.Text = "Auftrag";
			tpOrder.UseVisualStyleBackColor = true;
			// 
			// btnFileUpoad
			// 
			btnFileUpoad.BackColor = Color.White;
			btnFileUpoad.Location = new Point(360, 497);
			btnFileUpoad.Name = "btnFileUpoad";
			btnFileUpoad.Size = new Size(125, 23);
			btnFileUpoad.TabIndex = 85;
			btnFileUpoad.Text = "Dateien auswählen";
			btnFileUpoad.UseVisualStyleBackColor = false;
			btnFileUpoad.Click += btnFileUpoad_Click;
			// 
			// tbFileName
			// 
			tbFileName.Enabled = false;
			tbFileName.Location = new Point(136, 497);
			tbFileName.Name = "tbFileName";
			tbFileName.Size = new Size(200, 23);
			tbFileName.TabIndex = 84;
			// 
			// lblFileNamen
			// 
			lblFileNamen.AutoSize = true;
			lblFileNamen.Location = new Point(23, 500);
			lblFileNamen.Name = "lblFileNamen";
			lblFileNamen.Size = new Size(47, 15);
			lblFileNamen.TabIndex = 83;
			lblFileNamen.Text = "Dateien";
			// 
			// cbOrderAttachment
			// 
			cbOrderAttachment.AutoSize = true;
			cbOrderAttachment.Location = new Point(136, 430);
			cbOrderAttachment.Name = "cbOrderAttachment";
			cbOrderAttachment.Size = new Size(15, 14);
			cbOrderAttachment.TabIndex = 82;
			cbOrderAttachment.UseVisualStyleBackColor = true;
			// 
			// lblOrderAttachment
			// 
			lblOrderAttachment.AutoSize = true;
			lblOrderAttachment.Location = new Point(23, 430);
			lblOrderAttachment.Name = "lblOrderAttachment";
			lblOrderAttachment.Size = new Size(64, 15);
			lblOrderAttachment.TabIndex = 81;
			lblOrderAttachment.Text = "Bissnahme";
			// 
			// tbJsonOutput2
			// 
			tbJsonOutput2.Location = new Point(682, 40);
			tbJsonOutput2.Multiline = true;
			tbJsonOutput2.Name = "tbJsonOutput2";
			tbJsonOutput2.ReadOnly = true;
			tbJsonOutput2.Size = new Size(412, 554);
			tbJsonOutput2.TabIndex = 78;
			// 
			// btnCopyToClipboard2
			// 
			btnCopyToClipboard2.Location = new Point(987, 616);
			btnCopyToClipboard2.Name = "btnCopyToClipboard2";
			btnCopyToClipboard2.Size = new Size(107, 28);
			btnCopyToClipboard2.TabIndex = 79;
			btnCopyToClipboard2.Text = "Zwischenablage";
			btnCopyToClipboard2.UseVisualStyleBackColor = true;
			btnCopyToClipboard2.Click += btnCopyToClipboard2_Click;
			// 
			// dtpOrderAppointmentTime
			// 
			dtpOrderAppointmentTime.Format = DateTimePickerFormat.Time;
			dtpOrderAppointmentTime.Location = new Point(342, 392);
			dtpOrderAppointmentTime.Name = "dtpOrderAppointmentTime";
			dtpOrderAppointmentTime.Size = new Size(75, 23);
			dtpOrderAppointmentTime.TabIndex = 77;
			// 
			// dtpOrderAppointment
			// 
			dtpOrderAppointment.Location = new Point(136, 392);
			dtpOrderAppointment.Name = "dtpOrderAppointment";
			dtpOrderAppointment.Size = new Size(200, 23);
			dtpOrderAppointment.TabIndex = 76;
			// 
			// lblOrderAppointment
			// 
			lblOrderAppointment.AutoSize = true;
			lblOrderAppointment.Location = new Point(23, 398);
			lblOrderAppointment.Name = "lblOrderAppointment";
			lblOrderAppointment.Size = new Size(43, 15);
			lblOrderAppointment.TabIndex = 75;
			lblOrderAppointment.Text = "Termin";
			// 
			// cbOrderScanSentVia
			// 
			cbOrderScanSentVia.FormattingEnabled = true;
			cbOrderScanSentVia.Items.AddRange(new object[] { "Sirona", "3 Shape" });
			cbOrderScanSentVia.Location = new Point(136, 250);
			cbOrderScanSentVia.Name = "cbOrderScanSentVia";
			cbOrderScanSentVia.Size = new Size(200, 23);
			cbOrderScanSentVia.TabIndex = 74;
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(136, 311);
			label8.Name = "label8";
			label8.Size = new Size(195, 15);
			label8.TabIndex = 44;
			label8.Text = "übernimmt Daten von Tab \"Patient\"";
			// 
			// tbOrderColor
			// 
			tbOrderColor.Location = new Point(136, 91);
			tbOrderColor.Name = "tbOrderColor";
			tbOrderColor.Size = new Size(200, 23);
			tbOrderColor.TabIndex = 43;
			// 
			// tbOrderTrackingNumber
			// 
			tbOrderTrackingNumber.Location = new Point(136, 279);
			tbOrderTrackingNumber.Name = "tbOrderTrackingNumber";
			tbOrderTrackingNumber.Size = new Size(200, 23);
			tbOrderTrackingNumber.TabIndex = 39;
			// 
			// tbOrderDescription
			// 
			tbOrderDescription.Location = new Point(136, 120);
			tbOrderDescription.Multiline = true;
			tbOrderDescription.Name = "tbOrderDescription";
			tbOrderDescription.Size = new Size(200, 124);
			tbOrderDescription.TabIndex = 37;
			// 
			// tbOrderTitle
			// 
			tbOrderTitle.Location = new Point(136, 37);
			tbOrderTitle.Name = "tbOrderTitle";
			tbOrderTitle.Size = new Size(200, 23);
			tbOrderTitle.TabIndex = 30;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(23, 311);
			label7.Name = "label7";
			label7.Size = new Size(44, 15);
			label7.TabIndex = 42;
			label7.Text = "Patient";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(23, 282);
			label4.Name = "label4";
			label4.Size = new Size(97, 15);
			label4.TabIndex = 40;
			label4.Text = "Trackingnummer";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(23, 123);
			label1.Name = "label1";
			label1.Size = new Size(79, 15);
			label1.TabIndex = 38;
			label1.Text = "Beschreibung";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(23, 253);
			label2.Name = "label2";
			label2.Size = new Size(50, 15);
			label2.TabIndex = 35;
			label2.Text = "Scan via";
			// 
			// lblOrderTitle
			// 
			lblOrderTitle.AutoSize = true;
			lblOrderTitle.Location = new Point(23, 43);
			lblOrderTitle.Name = "lblOrderTitle";
			lblOrderTitle.Size = new Size(29, 15);
			lblOrderTitle.TabIndex = 31;
			lblOrderTitle.Text = "Titel";
			// 
			// btnOrderSend
			// 
			btnOrderSend.BackColor = Color.White;
			btnOrderSend.Location = new Point(136, 594);
			btnOrderSend.Name = "btnOrderSend";
			btnOrderSend.Size = new Size(200, 50);
			btnOrderSend.TabIndex = 26;
			btnOrderSend.Text = "An Condent übermitteln";
			btnOrderSend.UseVisualStyleBackColor = false;
			btnOrderSend.Click += btnOrderSend_Click;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(23, 94);
			label6.Name = "label6";
			label6.Size = new Size(36, 15);
			label6.TabIndex = 25;
			label6.Text = "Farbe";
			// 
			// tpPatient
			// 
			tpPatient.Controls.Add(tbPatientInsurance);
			tpPatient.Controls.Add(cbPatientCostUnitType);
			tpPatient.Controls.Add(lblPatientInsurance);
			tpPatient.Controls.Add(lblPatientCostUnitType);
			tpPatient.Controls.Add(cbAdressCountry);
			tpPatient.Controls.Add(tbAddress2);
			tpPatient.Controls.Add(tbAddressZipCode);
			tpPatient.Controls.Add(tbAddress1);
			tpPatient.Controls.Add(tbAddressCity);
			tpPatient.Controls.Add(tbPatientEmail);
			tpPatient.Controls.Add(tbPatientPhoneNumber);
			tpPatient.Controls.Add(tbPatientFirstName);
			tpPatient.Controls.Add(tbPatientReference);
			tpPatient.Controls.Add(tbJsonOutput);
			tpPatient.Controls.Add(tbPatientLastName);
			tpPatient.Controls.Add(label12);
			tpPatient.Controls.Add(label14);
			tpPatient.Controls.Add(label15);
			tpPatient.Controls.Add(label13);
			tpPatient.Controls.Add(label16);
			tpPatient.Controls.Add(lblPatientEmail);
			tpPatient.Controls.Add(cbPatientSalutation);
			tpPatient.Controls.Add(lblPatientPhonenumber);
			tpPatient.Controls.Add(lblPatientFirstName);
			tpPatient.Controls.Add(lblPatientlReference);
			tpPatient.Controls.Add(btnCopyToClipboard);
			tpPatient.Controls.Add(dtpPatientBirthdate);
			tpPatient.Controls.Add(lblPatientLastName);
			tpPatient.Controls.Add(btnPatientSend);
			tpPatient.Controls.Add(lblPatientBirthdate);
			tpPatient.Controls.Add(lblPatientSalutation);
			tpPatient.Location = new Point(4, 24);
			tpPatient.Name = "tpPatient";
			tpPatient.Padding = new Padding(3);
			tpPatient.Size = new Size(1131, 661);
			tpPatient.TabIndex = 0;
			tpPatient.Text = "Patient";
			tpPatient.UseVisualStyleBackColor = true;
			// 
			// tbPatientInsurance
			// 
			tbPatientInsurance.Location = new Point(136, 505);
			tbPatientInsurance.Name = "tbPatientInsurance";
			tbPatientInsurance.Size = new Size(200, 23);
			tbPatientInsurance.TabIndex = 75;
			// 
			// cbPatientCostUnitType
			// 
			cbPatientCostUnitType.FormattingEnabled = true;
			cbPatientCostUnitType.Items.AddRange(new object[] { "Privat", "SUVA / IV / MV", "Krankenkasse", "EL / Asyl / Sozial" });
			cbPatientCostUnitType.Location = new Point(136, 476);
			cbPatientCostUnitType.Name = "cbPatientCostUnitType";
			cbPatientCostUnitType.Size = new Size(200, 23);
			cbPatientCostUnitType.TabIndex = 77;
			// 
			// lblPatientInsurance
			// 
			lblPatientInsurance.AutoSize = true;
			lblPatientInsurance.Location = new Point(16, 508);
			lblPatientInsurance.Name = "lblPatientInsurance";
			lblPatientInsurance.Size = new Size(75, 15);
			lblPatientInsurance.TabIndex = 76;
			lblPatientInsurance.Text = "Versicherung";
			// 
			// lblPatientCostUnitType
			// 
			lblPatientCostUnitType.AutoSize = true;
			lblPatientCostUnitType.Location = new Point(16, 479);
			lblPatientCostUnitType.Name = "lblPatientCostUnitType";
			lblPatientCostUnitType.Size = new Size(70, 15);
			lblPatientCostUnitType.TabIndex = 74;
			lblPatientCostUnitType.Text = "Kostenstelle";
			// 
			// cbAdressCountry
			// 
			cbAdressCountry.FormattingEnabled = true;
			cbAdressCountry.Items.AddRange(new object[] { "Schweiz" });
			cbAdressCountry.Location = new Point(136, 396);
			cbAdressCountry.Name = "cbAdressCountry";
			cbAdressCountry.Size = new Size(200, 23);
			cbAdressCountry.TabIndex = 73;
			// 
			// tbAddress2
			// 
			tbAddress2.Location = new Point(136, 309);
			tbAddress2.Name = "tbAddress2";
			tbAddress2.Size = new Size(200, 23);
			tbAddress2.TabIndex = 72;
			// 
			// tbAddressZipCode
			// 
			tbAddressZipCode.Location = new Point(136, 338);
			tbAddressZipCode.Name = "tbAddressZipCode";
			tbAddressZipCode.Size = new Size(200, 23);
			tbAddressZipCode.TabIndex = 70;
			// 
			// tbAddress1
			// 
			tbAddress1.Location = new Point(136, 280);
			tbAddress1.Name = "tbAddress1";
			tbAddress1.Size = new Size(200, 23);
			tbAddress1.TabIndex = 68;
			// 
			// tbAddressCity
			// 
			tbAddressCity.Location = new Point(136, 367);
			tbAddressCity.Name = "tbAddressCity";
			tbAddressCity.Size = new Size(200, 23);
			tbAddressCity.TabIndex = 66;
			// 
			// tbPatientEmail
			// 
			tbPatientEmail.Location = new Point(136, 182);
			tbPatientEmail.Name = "tbPatientEmail";
			tbPatientEmail.Size = new Size(200, 23);
			tbPatientEmail.TabIndex = 22;
			// 
			// tbPatientPhoneNumber
			// 
			tbPatientPhoneNumber.Location = new Point(136, 211);
			tbPatientPhoneNumber.Name = "tbPatientPhoneNumber";
			tbPatientPhoneNumber.Size = new Size(200, 23);
			tbPatientPhoneNumber.TabIndex = 19;
			// 
			// tbPatientFirstName
			// 
			tbPatientFirstName.Location = new Point(136, 95);
			tbPatientFirstName.Name = "tbPatientFirstName";
			tbPatientFirstName.Size = new Size(200, 23);
			tbPatientFirstName.TabIndex = 17;
			tbPatientFirstName.Text = "Elias";
			// 
			// tbPatientReference
			// 
			tbPatientReference.Location = new Point(136, 37);
			tbPatientReference.Name = "tbPatientReference";
			tbPatientReference.Size = new Size(200, 23);
			tbPatientReference.TabIndex = 15;
			tbPatientReference.Text = "1234567";
			// 
			// tbJsonOutput
			// 
			tbJsonOutput.Location = new Point(682, 40);
			tbJsonOutput.Multiline = true;
			tbJsonOutput.Name = "tbJsonOutput";
			tbJsonOutput.ReadOnly = true;
			tbJsonOutput.Size = new Size(412, 554);
			tbJsonOutput.TabIndex = 13;
			// 
			// tbPatientLastName
			// 
			tbPatientLastName.Location = new Point(136, 124);
			tbPatientLastName.Name = "tbPatientLastName";
			tbPatientLastName.Size = new Size(200, 23);
			tbPatientLastName.TabIndex = 10;
			tbPatientLastName.Text = "Bühler";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new Point(23, 341);
			label12.Name = "label12";
			label12.Size = new Size(27, 15);
			label12.TabIndex = 71;
			label12.Text = "PLZ";
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Location = new Point(23, 370);
			label14.Name = "label14";
			label14.Size = new Size(24, 15);
			label14.TabIndex = 67;
			label14.Text = "Ort";
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new Point(23, 399);
			label15.Name = "label15";
			label15.Size = new Size(33, 15);
			label15.TabIndex = 65;
			label15.Text = "Land";
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new Point(23, 286);
			label13.Name = "label13";
			label13.Size = new Size(70, 15);
			label13.TabIndex = 69;
			label13.Text = "Strasse / Nr.";
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new Point(23, 312);
			label16.Name = "label16";
			label16.Size = new Size(74, 15);
			label16.TabIndex = 64;
			label16.Text = "Adresszusatz";
			// 
			// lblPatientEmail
			// 
			lblPatientEmail.AutoSize = true;
			lblPatientEmail.Location = new Point(23, 185);
			lblPatientEmail.Name = "lblPatientEmail";
			lblPatientEmail.Size = new Size(41, 15);
			lblPatientEmail.TabIndex = 23;
			lblPatientEmail.Text = "E-Mail";
			// 
			// cbPatientSalutation
			// 
			cbPatientSalutation.FormattingEnabled = true;
			cbPatientSalutation.Items.AddRange(new object[] { "Frau", "Herr" });
			cbPatientSalutation.Location = new Point(136, 66);
			cbPatientSalutation.Name = "cbPatientSalutation";
			cbPatientSalutation.Size = new Size(200, 23);
			cbPatientSalutation.TabIndex = 21;
			// 
			// lblPatientPhonenumber
			// 
			lblPatientPhonenumber.AutoSize = true;
			lblPatientPhonenumber.Location = new Point(23, 214);
			lblPatientPhonenumber.Name = "lblPatientPhonenumber";
			lblPatientPhonenumber.Size = new Size(91, 15);
			lblPatientPhonenumber.TabIndex = 20;
			lblPatientPhonenumber.Text = "Telefonnummer";
			// 
			// lblPatientFirstName
			// 
			lblPatientFirstName.AutoSize = true;
			lblPatientFirstName.Location = new Point(23, 101);
			lblPatientFirstName.Name = "lblPatientFirstName";
			lblPatientFirstName.Size = new Size(54, 15);
			lblPatientFirstName.TabIndex = 18;
			lblPatientFirstName.Text = "Vorname";
			// 
			// lblPatientlReference
			// 
			lblPatientlReference.AutoSize = true;
			lblPatientlReference.Location = new Point(23, 43);
			lblPatientlReference.Name = "lblPatientlReference";
			lblPatientlReference.Size = new Size(63, 15);
			lblPatientlReference.TabIndex = 16;
			lblPatientlReference.Text = "Patient Nr.";
			// 
			// btnCopyToClipboard
			// 
			btnCopyToClipboard.Location = new Point(987, 616);
			btnCopyToClipboard.Name = "btnCopyToClipboard";
			btnCopyToClipboard.Size = new Size(107, 28);
			btnCopyToClipboard.TabIndex = 14;
			btnCopyToClipboard.Text = "Zwischenablage";
			btnCopyToClipboard.UseVisualStyleBackColor = true;
			btnCopyToClipboard.Click += btnCopyToClipboard_Click;
			// 
			// dtpPatientBirthdate
			// 
			dtpPatientBirthdate.Location = new Point(136, 153);
			dtpPatientBirthdate.Name = "dtpPatientBirthdate";
			dtpPatientBirthdate.Size = new Size(200, 23);
			dtpPatientBirthdate.TabIndex = 12;
			// 
			// lblPatientLastName
			// 
			lblPatientLastName.AutoSize = true;
			lblPatientLastName.Location = new Point(23, 127);
			lblPatientLastName.Name = "lblPatientLastName";
			lblPatientLastName.Size = new Size(65, 15);
			lblPatientLastName.TabIndex = 11;
			lblPatientLastName.Text = "Nachname";
			// 
			// btnPatientSend
			// 
			btnPatientSend.BackColor = Color.White;
			btnPatientSend.Location = new Point(136, 594);
			btnPatientSend.Name = "btnPatientSend";
			btnPatientSend.Size = new Size(200, 50);
			btnPatientSend.TabIndex = 9;
			btnPatientSend.Text = "An Condent übermitteln";
			btnPatientSend.UseVisualStyleBackColor = false;
			btnPatientSend.Click += btnPatientSend_Click;
			// 
			// lblPatientBirthdate
			// 
			lblPatientBirthdate.AutoSize = true;
			lblPatientBirthdate.Location = new Point(23, 159);
			lblPatientBirthdate.Name = "lblPatientBirthdate";
			lblPatientBirthdate.Size = new Size(83, 15);
			lblPatientBirthdate.TabIndex = 8;
			lblPatientBirthdate.Text = "Geburtsdatum";
			// 
			// lblPatientSalutation
			// 
			lblPatientSalutation.AutoSize = true;
			lblPatientSalutation.Location = new Point(23, 72);
			lblPatientSalutation.Name = "lblPatientSalutation";
			lblPatientSalutation.Size = new Size(45, 15);
			lblPatientSalutation.TabIndex = 6;
			lblPatientSalutation.Text = "Anrede";
			// 
			// tcDefault
			// 
			tcDefault.Controls.Add(tpPatient);
			tcDefault.Controls.Add(tpOrder);
			tcDefault.Controls.Add(tpSettings);
			tcDefault.Dock = DockStyle.Fill;
			tcDefault.Location = new Point(0, 0);
			tcDefault.Name = "tcDefault";
			tcDefault.SelectedIndex = 0;
			tcDefault.Size = new Size(1139, 689);
			tcDefault.TabIndex = 1;
			// 
			// openFileDialog1
			// 
			openFileDialog1.FileName = "openFileDialog1";
			openFileDialog1.Multiselect = true;
			// 
			// CondentAppForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1139, 689);
			Controls.Add(tcDefault);
			Name = "CondentAppForm";
			Text = "Condent API Testing App";
			tpSettings.ResumeLayout(false);
			tpSettings.PerformLayout();
			tpOrder.ResumeLayout(false);
			tpOrder.PerformLayout();
			tpPatient.ResumeLayout(false);
			tpPatient.PerformLayout();
			tcDefault.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TabPage tpSettings;
		private Button btnSettingsSave;
		private Label lblUrl;
		private TextBox tbSettingsApiUrl;
		private TextBox tbApiKey;
		private Label lblApiKey;
		private TabPage tpOrder;
		private Label label8;
		private TextBox tbOrderColor;
		private TextBox tbOrderTrackingNumber;
		private TextBox tbOrderDescription;
		private TextBox tbOrderTitle;
		private Label label7;
		private Label label4;
		private Label label1;
		private Label label2;
		private Label lblOrderTitle;
		private Button btnOrderSend;
		private Label label6;
		private TabPage tpPatient;
		private ComboBox cbAdressCountry;
		private TextBox tbAddress2;
		private TextBox tbAddressZipCode;
		private TextBox tbAddress1;
		private TextBox tbAddressCity;
		private TextBox tbPatientEmail;
		private TextBox tbPatientPhoneNumber;
		private TextBox tbPatientFirstName;
		private TextBox tbPatientReference;
		private TextBox tbJsonOutput;
		private TextBox tbPatientLastName;
		private Label label12;
		private Label label14;
		private Label label15;
		private Label label13;
		private Label label16;
		private Label lblPatientEmail;
		private ComboBox cbPatientSalutation;
		private Label lblPatientPhonenumber;
		private Label lblPatientFirstName;
		private Label lblPatientlReference;
		private Button btnCopyToClipboard;
		private DateTimePicker dtpPatientBirthdate;
		private Label lblPatientLastName;
		private Button btnPatientSend;
		private Label lblPatientBirthdate;
		private Label lblPatientSalutation;
		private TabControl tcDefault;
		private TextBox tbPatientInsurance;
		private ComboBox cbPatientCostUnitType;
		private Label lblPatientInsurance;
		private Label lblPatientCostUnitType;
		private ComboBox cbOrderScanSentVia;
		private DateTimePicker dtpOrderAppointment;
		private Label lblOrderAppointment;
		private DateTimePicker dtpOrderAppointmentTime;
		private TextBox tbJsonOutput2;
		private Button btnCopyToClipboard2;
		private CheckBox cbOrderAttachment;
		private Label lblOrderAttachment;
		private Button btnFileUpoad;
		private TextBox tbFileName;
		private Label lblFileNamen;
		private OpenFileDialog openFileDialog1;
	}
}