---
title: Billing Export API
nav_order: 3
---

{: .warning }
Diese API befindet sich noch in Entwicklung. Änderungen vorbehalten.

## Übersicht

Über diese [eventbasierte API](#exportierte-aufträge-abfragen) können vom Labor markierte Aufträge aus Condent abgefragt werden. Dadurch können in Ihrem System beispielsweise Kostenvoranschläge, Rechnungen oder andere Leistungen erstellt werden.

Zusätzlich können in Ihrem System erzeugte Leistungen (z.B. Rechnungen oder Kostenvoranschläge) wieder nach Condent [zurückgespielt](#leistungen-importieren-bzw-erstellen-kostenvoranschlag-rechnung-usw) werden. Dabei können auch Dateien (z.B. PDF, XML usw.) übertragen werden.

## Authentifizierung

Siehe [hier](./index.md#authentifizierung).

## Konzept

Das Event-System arbeitet mit einem **Cursor-Prinzip**, siehe [Konzeptbeschreibung](./billing-events-api.md#konzept).

## Exportierte Aufträge abfragen

### Endpunkt

```http
GET /v2/billing/export-events
```

### Parameter

| Parameter | Typ | Pflicht | Beschreibung |
|-----------|-----|---------|--------------|
| `cursor` | string | Nein | Zuletzt empfangener `nextCursor`. Ohne diesen Parameter werden die ersten verfügbaren Events gemäss `limit` zurückgegeben. |
| `limit` | int | Nein | Maximale Anzahl Events pro Anfrage (Standard: 50, Maximum: 200). |

### Antwortstruktur

```json
{
  "events": [
    {
      "eventId": 67890,
      "timestamp": "2026-01-23T11:00:00Z",
      "data": {
        "key": "770e8400-e29b-41d4-a716-446655440000",
        "customer": {
            "id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
            "reference": "1234567"
        },
        "patient": {
            "id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
            "reference": "1234567",
            "salutation": null,
            "firstName": "John",
            "lastName": "Doe",
            "birthday": "1990-01-01",
            "streetName": "Zürichstrasse",
            "buildingNumber": "123",
            "city": "Zürich",
            "zipCode": "8000",
            "countryCode": "CH",
            "phoneNumber": null,
            "email": "john.doe@condent.ch"
        },
        "customerEmployee": {
            "salutation": null,
            "firstName": "John",
            "lastName": "Doe"
        },
        "title": "OK/UK Drahtklammerprothesen",
        "description": "OK Drahtklammerimmediatsprothese zum Ersatz von 16,14,22,24-26",
        "color": "B1",
        "type": "DeliveryBill",
        "url": "https://my.condent.ch/redirect/billing-export/770e8400-e29b-41d4-a716-446655440000",
        "patientUrl": "https://my.condent.ch/redirect/patient/456",
        "createDate": "2026-01-20T09:00:00Z"
      }
    }
  ],
  "nextCursor": "abc123==",
  "hasMore": false
}
```

| Property | Beschreibung |
|-----|--------------|
| `key` | Interne Condent-ID um dem Auftrag dann eine Leistung o.Ä. zuzuweisen |
| `customer.id` | Interne Condent-ID des Kunden |
| `customer.reference` | Code z.B. aus einem ERP-System |
| `patient.id` | Interne Condent-ID des Patienten |
| `patient.reference` | Pat Nr. z.B. aus einem ERP-System |
| `customerEmployee` | Behandler (kann leer sein) |
| `type` | Typ der erwarteten Leistung (z.B. Kostenvoranschlag oder Rechnung; optional) |

## Leistungen importieren bzw. erstellen (Kostenvoranschlag, Rechnung usw.)

### Endpunkt

```http
POST /api/billing/import
```

### Request-Parameter

| Parameter   | Pflicht     | Typ         | Kommentar |
| ----------- | ----------- | ----------- | ----------- |
| key       | Ja        | string      | Interne Condent-ID aus `/v2/billing/export-events`       |
| title       | Nein        | string      | Der Titel der Leistung       |
| color | Nein        | string | Farbe, z.B. für Krone oder Schiene        |
| description | Nein        | string        | Beschreibung der Leistung |
| type | Ja        | string        | Typ der Leistung (z.B. Kostenvoranschlag oder Rechnung) |
| date | Ja        | string        | Datum der Leistung (z.B. Rechnungsdatum) |

---

### Response-Parameter

| Parameter   | Typ         | Kommentar |
| ----------- | ----------- | ----------- |
| url       | string      | URL der Leistung auf Condent       |
| uploadToken       | string      | [Token um Dateien zu hochzuladen](#dateien-hochladen)       |
| warnings       | string array     | Die Warnungen sind für Entwickler:innen gedacht. Sie weisen beispielsweise darauf hin, dass bestimmte Textfelder zu lang sind. Die Leistung kann trotz dieser Warnungen erfasst werden.      |

### Dateien hochladen

Um Dateien hochzuladen, muss zuerst eine [Leistung](#leistungen-importieren-bzw-erstellen-kostenvoranschlag-rechnung-usw) erstellt werden. Den benötigten `uploadToken` erhalten Sie in der Response des Import-Requests.

Aktuell gelten folgende Beschränkungen:
- Maximale Dateigrösse: 100 MB
- Alle Dateiformate werden akzeptiert (z.B. JPG, PNG, ZIP, PDF, STL, 3OX usw.)

Sofern möglich, werden bestimmte Dateien (z.B. Bilder) serverseitig komprimiert.

Dateien müssen einzeln übertragen werden, um Timeouts zu vermeiden (z.B. bei vielen oder grossen Dateien). Wir empfehlen parallele Requests.

Eein Beispiel in C# finden Sie [hier](https://github.com/OPTEN/Condent-Public-API/blob/main/examples/Opten.Condent.DentistApp/CondentAppForm.cs).

Alternativ können mehrere Dateien gesammelt als ZIP-Datei übertragen werden.

Eine Leistung wird in Condent auch dann erstellt, wenn einzelne Dateien nicht erfolgreich übertragen wurden. Das Labor kann Dateien anschliessend manuell hochladen oder entfernen.

Details zu Request und Response finden Sie [hier](./index.md#dateien-hochladen).