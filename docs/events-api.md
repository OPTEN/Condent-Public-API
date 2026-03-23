---
title: Events API
nav_order: 2
---

{: .warning }
> ⚠️ **Work in Progress**: Diese API befindet sich noch in Entwicklung. Änderungen vorbehalten.

## Übersicht

Die neue eventbasierte API ermöglicht es, Änderungen an Leistungen (z. B. Lieferschein, Kostenvoranschlag, Rechnung usw.) effizient abzufragen. Anstatt regelmässig alle Daten vollständig zu synchronisieren, können nur die Änderungen seit der letzten Abfrage geladen werden.

Dokumente wie **PDF** oder **XML** (Suva/Sumex1, VDDS) werden ebenfalls mitgeliefert.

## Authentifizierung

Siehe [hier](./index.md#authentifizierung).

## Konzept

Das Event-System arbeitet mit einem **Cursor-Prinzip**:

1. **Erster Aufruf**  
   Wenn kein `cursor` übergeben wird, liefert die API die erste Ergebnisseite gemäss dem definierten `limit`. So kann die initiale Verarbeitung oder Synchronisation gestartet werden.

2. **Folgende Aufrufe**  
   Mit dem `nextCursor` aus der vorherigen Antwort erhalten Sie die jeweils nächsten neuen Events ab der zuletzt verarbeiteten Position.

`nextCursor` muss vom Client gespeichert und bei der nächsten Anfrage wieder mitgesendet werden.

Der Cursor kann als **Base64-codierter String** zurückgegeben werden. In diesem Fall können Zeichen wie `=` enthalten sein. Da solche Zeichen in Query-Parametern speziell behandelt werden, sollte der Cursor **URL-encoded** übertragen werden (z. B. `=` → `%3D`).

Beispiel:

```http
GET /v2/billing/event?cursor=eyJwb3MiOjEyMzQ1fQ%3D%3D
```

Viele HTTP-Clients übernehmen dieses Encoding automatisch. Wird die URL jedoch manuell zusammengesetzt, muss der `cursor` korrekt encoded werden.

## Endpunkte

```http
GET /v2/billing/event
```

Swagger: https://api-dev.condent.app/swagger/index.html

## Parameter

| Parameter | Typ | Pflicht | Beschreibung |
|-----------|-----|---------|--------------|
| `cursor` | string | Nein | Zuletzt empfangener `nextCursor`. Ohne diesen Parameter werden die ersten verfügbaren Events gemäss `limit` zurückgegeben. |
| `limit` | int | Nein | Maximale Anzahl Events pro Anfrage (Standard: 50, Maximum: 200) |

## Antwort-Struktur

```json
{
  "events": [
    {
      "eventId": 67890,
      "eventType": "Created",
      "timestamp": "2026-01-23T11:00:00Z",
      "data": {
        "key": "770e8400-e29b-41d4-a716-446655440000",
        "patientId": 1001,
        "patientReference": "1234567",
        "reference": "2025-01-16-0011",
        "title": "Implantatkrone Seitenzahn Ti Base",
        "type": "DeliveryBill",
        "status": "Submitted",
        "currencySymbol": "CHF",
        "totalPrice": 3500.00,
        "patientUrl": "https://my.condent.ch/redirect/patient/456",
        "url": "https://my.condent.ch/redirect/bill/770e8400-e29b-41d4-a716-446655440000",
        "date": "2026-01-20T00:00:00Z",
        "createDate": "2026-01-20T09:00:00Z",
        "updateDate": "2026-01-23T11:00:00Z",
        "documents": [
          {
            "key": "880e8400-e29b-41d4-a716-446655440001",
            "name": "LS-2026-001.pdf",
            "mimeType": "application/pdf",
            "url": "https://api.condent.ch/v2/document/880e8400-e29b-41d4-a716-446655440001",
            "createDate": "2026-01-20T09:00:00Z",
            "updateDate": "2026-01-20T09:00:00Z"
          }
        ]
      }
    }
  ],
  "nextCursor": "abc123==",
  "hasMore": false
}
```

| Property | Beschreibung |
|-----|--------------|
| `patientId` | Interne Condent-ID |
| `patientReference` | Pat Nr. z.B. aus einem Zaz-System |

## Event-Typen

| Typ | Beschreibung |
|-----|--------------|
| `Created` | Neue Leistung erstellt |
| `Updated` | Bestehende Leistung aktualisiert |
| `Deleted` | Bestehende Leistung gelöscht |

## Verwendungsbeispiel

### Initiale Synchronisation

```bash
# Erste Seite an Events abrufen (ohne cursor)
GET /v2/billing/event?limit=50

# Antwort:
# { "events": [...], "nextCursor": "abc123==", "hasMore": false }
```

### Weitere Events abrufen

```bash
# Nächste Events ab der zuletzt erhaltenen Position abrufen
GET /v2/billing/event?cursor=abc123%3D%3D&limit=50

# Antwort:
# { "events": [...], "nextCursor": "def456==", "hasMore": false }
```

### Pagination bei vielen Events

```bash
# Bei hasMore=true weitere Events abrufen
GET /v2/billing/event?cursor=def456%3D%3D&limit=100

# Wiederholen, bis hasMore=false ist
```

## Empfohlenes Polling-Intervall

- **Minimum**: 30 Sekunden
- **Empfohlen**: 1 bis 5 Minuten

## Hinweise

- Bei `Deleted`-Events kann `data` leer sein (`null`)
- Speichern Sie den `nextCursor` persistent, um nach einem Neustart an der letzten Position fortfahren zu können
- Die `documents`-Liste kann leer sein, wenn keine Dokumente vorhanden sind
- Falls der `cursor` Base64-codiert ist, muss er beim Übergeben als Query-Parameter korrekt URL-encoded werden
