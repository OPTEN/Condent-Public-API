# Condent Public API

Dieses Repository enthält die öffentliche Dokumentation und Beispiele zur Integration der **Condent API**.

## Dokumentationsseite

📖 **https://opten.github.io/Condent-Public-API/**

## Dokumentation

| Dokument | Beschreibung |
| --- | --- |
| [Einführung](docs/index.md) | Welche API für wen, Authentifizierung, Serverzeit und Fehlerantworten |
| **Praxissoftware** | |
| [Aufträge an Condent senden](docs/order-create-api.md) | Patienten und Aufträge erstellen, Dateien hochladen |
| [Billing Events API](docs/billing-events-api.md) | Eventbasierte Schnittstelle zur Abfrage von Änderungen an Leistungen |
| [Rechnungs- und Lieferschein-XML](docs/billing-document-xml.md) | XML-Format für Rechnungen und Lieferscheine im PDF |
| **Laborsoftware** | |
| [Order Events API](docs/order-events-api.md) | Eventbasierte Schnittstelle zur Übernahme von Aufträgen und Folgeereignissen (inkl. zur Abrechnung exportierter Aufträge) |
| [Billing Import API](docs/billing-import-api.md) | Leistungen (Kostenvoranschlag, Rechnung usw.) inkl. Dateien nach Condent zurückspielen |

## Beispiele

Unter [examples/](examples/) finden Sie eine C#-Beispielapplikation, die die wichtigsten API-Aufrufe demonstriert.

## API Endpoints

| Umgebung | URL |
| --- | --- |
| Development | https://api-dev.condent.app |
| Production | https://api.condent.app |

## Support

Bei Fragen oder Problemen wenden Sie sich an: support@condent.ch
