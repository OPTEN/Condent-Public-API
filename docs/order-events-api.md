---
title: Order Events API
parent: Laborsoftware
nav_order: 1
---

# Order Events API

{: .warning }
Diese API befindet sich noch in Entwicklung. Änderungen vorbehalten.

## Übersicht

Über die Order Events API kann das System eines Labors (z.B. ein ERP) Aufträge aus Condent automatisch übernehmen und weiterbearbeiten. Die Daten fliessen **von Condent in die Laborsoftware**.

Die Laborsoftware fragt einen Event-Feed ab und erhält:

| Ereignis in Condent | Event-Typ | Was die Laborsoftware erhält |
|---------------------|-----------|-----------------------|
| Ein neuer Auftrag wird erfasst | `OrderCreated` | Den Auftrag mit allen Auftragsdaten und einer Liste der Dateien |
| Der Kunde löscht den Auftrag (innerhalb der ersten Stunde möglich) | `OrderDeleted` | Eine Löschmeldung zum bereits übernommenen Auftrag |
| Der Kunde lädt weitere Dateien hoch | `MediaAdded` | Die neuen Dateien, dem bestehenden Auftrag zugeordnet |
| Der Kunde schreibt einen Kommentar | `CommentAdded` | Kommentartext, Verfasser und Zeitpunkt |
| Der Kunde stellt eine Terminänderungsanfrage | `AppointmentChangeRequested` | Die Anfrage mit dem gewünschten Termin. Dies ist **keine** bestätigte Terminänderung. |
| Auftragsdaten werden geändert (z.B. Beschreibung, Farbe, Termine, Status) | `OrderUpdated` | Den vollständigen, aktuellen Auftrag |
| Das Labor exportiert den Auftrag zur Abrechnung | `OrderExported` | Den vollständigen Auftrag |

Nicht alle Event-Typen müssen verarbeitet werden. Mit dem Parameter [`types`](#parameter) liefert die API nur die benötigten Events. Ein Abrechnungssystem fragt z.B. nur `OrderExported` ab.

### Auftragsarten

Der Feed enthält diese Arten von Aufträgen. Die Art steht im Feld `type` des Auftrags.

| Wert | Beschreibung |
|------|--------------|
| `Order` | Auftrag |
| `Production` | Produktionsauftrag |
| `Quote` | Offerte |

Notizen, Dokumente und Leistungen (Lieferscheine, Rechnungen usw.) sind nicht Teil dieses Feeds. Änderungen an Leistungen liefert die [Billing Events API](./billing-events-api.md).

Dateien (Scans, Bilder, PDFs) sind nicht in den Events eingebettet. Jedes Event listet die Dateien mit einer Download-URL auf; die Laborsoftware lädt sie separat herunter.

{: .note }
Leistungen wie Kostenvoranschläge, Rechnungen oder Lieferscheine werden über die [Billing Import API](./billing-import-api.md) nach Condent zurückgespielt.

## Authentifizierung (Sign V2)

Siehe [hier](./index.md#authentifizierung).

**Wichtig:** Alle Endpunkte in dieser API verwenden **Sign V2** für die Authentifizierung.

Der API-Schlüssel für diese API wird **pro Labor** ausgestellt, siehe [Labor-Schlüssel](./index.md#labor-schlüssel). Ein Schlüssel gibt nur Zugriff auf Aufträge, Events und Dateien des eigenen Labors.

Der `sign` String wird nach folgendem Schema aufgebaut (durch `\n` getrennt):

```
apiKey
method (uppercase)
scheme (lowercase)
host (lowercase)
path (lowercase)
querystring (original case)
timestamp
body
```

{: .highlight }
Scheme, host und path müssen in **Kleinbuchstaben** sein! Method ist in **Grossbuchstaben**. Querystring bleibt in **Original-Schreibweise**.

{: .note }
Auch **leere Strings** (z.B. bei fehlendem querystring oder body) müssen im `sign` String enthalten sein.

Details und Beispiele stehen in der [Authentifizierungs-Dokumentation](./index.md#x-condent-sign-generieren-sign-v2).

## Konzept

Der Feed arbeitet mit demselben **Cursor-Prinzip** wie die [Billing Events API](./billing-events-api.md#konzept):

1. **Erster Aufruf**  
   Ohne `cursor` liefert die API die erste Ergebnisseite gemäss `limit`.

2. **Folgende Aufrufe**  
   Mit dem `nextCursor` aus der vorherigen Antwort liefert die API die nächsten Events ab der zuletzt verarbeiteten Position.

Den `nextCursor` persistent speichern, und zwar erst, nachdem die Events dieser Seite verarbeitet sind. Schlägt die Verarbeitung fehl, wird die API erneut mit dem vorherigen Cursor aufgerufen und liefert dieselben Events nochmals; es geht nichts verloren.

Ist der Cursor Base64-codiert, kann er Zeichen wie `=` enthalten. Er muss **URL-encoded** übertragen werden (z.B. `=` → `%3D`).

### Auftrags- und Event-IDs

- `key` (GUID) identifiziert einen Auftrag in Condent und ändert sich nie. Damit lässt sich jedes Event dem richtigen Auftrag in der Laborsoftware zuordnen.
- `eventId` identifiziert ein Event. Dasselbe Event kann mehrfach geliefert werden (z.B. nach einem erneuten Abruf mit einem älteren Cursor). Jede `eventId` nur einmal verarbeiten.

{: .highlight }
Einen Auftrag in der Laborsoftware **nur einmal pro `key`** erstellen. Trifft ein `OrderCreated`-Event für einen bereits bekannten `key` ein, wird es ignoriert.

## Events abfragen

### Endpunkt

```http
GET /v2/order/event
```

Swagger: https://api-dev.condent.app/swagger/index.html

### Parameter

| Parameter | Typ | Pflicht | Beschreibung |
|-----------|-----|---------|--------------|
| `cursor` | string | Nein | Zuletzt empfangener `nextCursor`. Ohne diesen Parameter werden die ersten verfügbaren Events gemäss `limit` zurückgegeben. |
| `limit` | int | Nein | Maximale Anzahl Events pro Anfrage (Standard: 50, Maximum: 200). |
| `types` | string | Nein | Kommagetrennte Liste von [Event-Typen](#event-typen), z.B. `OrderExported` oder `OrderCreated,OrderDeleted`. Ohne diesen Parameter werden alle Event-Typen geliefert. |

Der Cursor ist unabhängig vom Filter. `types` kann jederzeit geändert werden, der bisherige Cursor bleibt gültig; ab dieser Position liefert die API dann die Events gemäss dem neuen Filter.

### Antwortstruktur

Jedes Event hat denselben Rahmen. Der Inhalt von `data` hängt vom `eventType` ab.

```json
{
  "events": [
    {
      "eventId": 67890,
      "eventType": "OrderCreated",
      "timestamp": "2026-11-20T09:15:00Z",
      "orderKey": "770e8400-e29b-41d4-a716-446655440000",
      "data": { }
    }
  ],
  "nextCursor": "abc123==",
  "hasMore": false
}
```

| Property | Beschreibung |
|----------|--------------|
| `eventId` | Eindeutige ID des Events |
| `eventType` | Typ des Events, siehe [Event-Typen](#event-typen) |
| `timestamp` | Zeitpunkt der Aktion in Condent (UTC) |
| `orderKey` | `key` des Auftrags, zu dem das Event gehört |
| `data` | Inhalt je nach Event-Typ |
| `nextCursor` | Cursor für die nächste Anfrage |
| `hasMore` | `true`, wenn sofort weitere Events verfügbar sind |

## Event-Typen

| Typ | Beschreibung |
|-----|--------------|
| `OrderCreated` | Ein neuer Auftrag wurde erfasst |
| `OrderDeleted` | Der Auftrag wurde in Condent gelöscht |
| `MediaAdded` | Zu einem bestehenden Auftrag wurden Dateien hinzugefügt |
| `CommentAdded` | Zum Auftrag wurde ein Kommentar geschrieben |
| `AppointmentChangeRequested` | Der Kunde wünscht einen anderen Termin |
| `OrderUpdated` | Auftragsdaten wurden geändert |
| `OrderExported` | Das Labor hat den Auftrag zur Abrechnung exportiert |

### OrderCreated

Enthält den vollständigen Auftrag. Die Felder entsprechen den Auftragsfeldern von [`POST /v1/order`](./order-create-api.md#request-parameter).

```json
{
  "eventId": 67890,
  "eventType": "OrderCreated",
  "timestamp": "2026-11-20T09:15:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "key": "770e8400-e29b-41d4-a716-446655440000",
    "type": "Order",
    "title": "Implantatkrone 46 Ti Base",
    "description": "Verschraubt, Zirkon",
    "status": "New",
    "color": "A2",
    "costUnitType": "Private",
    "insurances": [],
    "scanSentVia": "ThreeShape",
    "trackingNumber": null,
    "customer": {
      "id": 2001,
      "reference": "1234567"
    },
    "customerEmployee": {
      "salutation": "mrdrmeddent",
      "firstName": "Peter",
      "lastName": "Muster"
    },
    "patient": {
      "id": 1001,
      "reference": "1234567",
      "salutation": "ms",
      "firstName": "Jane",
      "lastName": "Doe",
      "birthday": "1990-01-01",
      "streetName": "Zürichstrasse",
      "buildingNumber": "123",
      "city": "Zürich",
      "zipCode": "8000",
      "countryCode": "CH",
      "phoneNumber": null,
      "email": null
    },
    "appointments": [
      {
        "key": "990e8400-e29b-41d4-a716-446655440002",
        "date": "2026-11-27T08:00:00Z",
        "description": "Einprobe"
      }
    ],
    "attachments": [
      {
        "upperJaw": true,
        "lowerJaw": false,
        "description": "Bissregistrat"
      }
    ],
    "files": [
      {
        "key": "880e8400-e29b-41d4-a716-446655440001",
        "name": "scan-ok.stl",
        "mimeType": "model/stl",
        "size": 18450231,
        "url": "https://api.condent.app/v2/order/770e8400-e29b-41d4-a716-446655440000/file/880e8400-e29b-41d4-a716-446655440001",
        "createDate": "2026-11-20T09:14:00Z"
      }
    ],
    "url": "https://my.condent.ch/redirect/order/770e8400-e29b-41d4-a716-446655440000",
    "patientUrl": "https://my.condent.ch/redirect/patient/456",
    "createDate": "2026-11-20T09:15:00Z"
  }
}
```

| Property | Beschreibung |
|----------|--------------|
| `key` | Interne Condent-ID des Auftrags (GUID), identisch mit `orderKey` |
| `type` | Art des Auftrags, siehe [Auftragsarten](#auftragsarten) |
| `status` | Status des Auftrags in Condent, z.B. `New`, `Processing`, `Delivered`. Änderungen werden mit `OrderUpdated` gemeldet. |
| `customer.id` | Interne Condent-ID des Kunden (Praxis, Zahl) |
| `customer.reference` | Kundennummer des Labors, z.B. aus der Laborsoftware. Kann leer sein, wenn sie in Condent nicht gepflegt ist. |
| `customerEmployee` | Behandler, der den Auftrag erfasst hat (kann leer sein) |
| `patient.id` | Interne Condent-ID des Patienten (Zahl, wie `patientId` in der [Billing Events API](./billing-events-api.md#antwortstruktur)) |
| `patient.reference` | Pat Nr. aus der Praxissoftware (kann leer sein) |
| `appointments` | Termine des Auftrags (z.B. Einprobe, Lieferung). Der `key` wird bei `AppointmentChangeRequested` verwendet. |
| `files` | Dateien zum Zeitpunkt der Erfassung. Weitere Dateien folgen mit `MediaAdded`. |
| `url` | Link zum Auftrag in Condent |

Mögliche Werte für `salutation`: siehe [`POST /v1/order`](./order-create-api.md#mögliche-werte-für-patientsalutation).

### OrderDeleted

Der Kunde kann einen Auftrag innerhalb der ersten Stunde nach der Erfassung löschen. Auch das Labor kann Aufträge löschen. Je nachdem, wie weit der Auftrag bereits bearbeitet wurde, wird er in der Laborsoftware gelöscht oder als gelöscht markiert.

```json
{
  "eventId": 67891,
  "eventType": "OrderDeleted",
  "timestamp": "2026-11-20T09:40:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "deletedBy": {
      "type": "Customer",
      "firstName": "Peter",
      "lastName": "Muster"
    }
  }
}
```

| Property | Beschreibung |
|----------|--------------|
| `deletedBy.type` | `Customer` (Praxis) oder `Lab` (Labor) |

Nach `OrderDeleted` werden für diesen Auftrag keine weiteren Events gesendet, und seine Dateien können nicht mehr heruntergeladen werden.

### MediaAdded

Wird gesendet, wenn der Kunde zu einem bestehenden Auftrag weitere Dateien hochlädt. Ein Event enthält alle Dateien eines Uploads.

```json
{
  "eventId": 67892,
  "eventType": "MediaAdded",
  "timestamp": "2026-11-20T10:05:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "addedBy": {
      "type": "Customer",
      "firstName": "Peter",
      "lastName": "Muster"
    },
    "files": [
      {
        "key": "880e8400-e29b-41d4-a716-446655440003",
        "name": "foto-farbe.jpg",
        "mimeType": "image/jpeg",
        "size": 1203344,
        "url": "https://api.condent.app/v2/order/770e8400-e29b-41d4-a716-446655440000/file/880e8400-e29b-41d4-a716-446655440003",
        "createDate": "2026-11-20T10:05:00Z"
      }
    ]
  }
}
```

### CommentAdded

```json
{
  "eventId": 67893,
  "eventType": "CommentAdded",
  "timestamp": "2026-11-20T10:30:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "key": "aa0e8400-e29b-41d4-a716-446655440004",
    "message": "Bitte Kontaktpunkt zu 45 prüfen.",
    "isWorkInstruction": false,
    "author": {
      "type": "Customer",
      "firstName": "Peter",
      "lastName": "Muster"
    },
    "createDate": "2026-11-20T10:30:00Z"
  }
}
```

| Property | Beschreibung |
|----------|--------------|
| `key` | Interne Condent-ID des Kommentars |
| `isWorkInstruction` | `true`, wenn der Kommentar als Arbeitsanweisung markiert ist |
| `author.type` | `Customer` (Praxis) oder `Lab` (Labor) |

### AppointmentChangeRequested

Der Kunde wünscht einen anderen Termin. Das Labor muss die Anfrage weiterhin in Condent annehmen oder ablehnen.

{: .highlight }
Dies ist eine **Anfrage**, keine bestätigte Änderung. Der vereinbarte Termin in der Laborsoftware darf nicht überschrieben werden.

```json
{
  "eventId": 67894,
  "eventType": "AppointmentChangeRequested",
  "timestamp": "2026-11-20T11:00:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "appointmentKey": "990e8400-e29b-41d4-a716-446655440002",
    "currentDate": "2026-11-27T08:00:00Z",
    "requestedDate": "2026-11-25T08:00:00Z",
    "currentDescription": "Einprobe",
    "requestedDescription": "Einprobe",
    "status": "Pending",
    "requestedBy": {
      "type": "Customer",
      "firstName": "Peter",
      "lastName": "Muster"
    }
  }
}
```

| Property | Beschreibung |
|----------|--------------|
| `appointmentKey` | `key` des Termins aus `OrderCreated` |
| `currentDate` | Aktuell vereinbarter Termin |
| `requestedDate` | Vom Kunden gewünschter Termin |
| `status` | Bei diesem Event immer `Pending` |

### OrderUpdated

Wird gesendet, wenn sich Daten des Auftrags ändern, z.B.:

- Titel, Beschreibung, Farbe oder weitere Auftragsfelder
- Termine (direkt geändert, hinzugefügt oder entfernt, auch wenn das Labor eine Terminänderungsanfrage annimmt oder ablehnt)
- Status des Auftrags

`data` enthält den vollständigen, aktuellen Auftrag im selben Format wie bei [`OrderCreated`](#ordercreated). Die Laborsoftware übernimmt die Felder, die sich bei ihr ändern dürfen. Ein Event wird nur gesendet, wenn sich tatsächlich etwas geändert hat; mehrere Änderungen kurz nacheinander können in einem Event zusammengefasst werden.

```json
{
  "eventId": 67896,
  "eventType": "OrderUpdated",
  "timestamp": "2026-11-21T08:10:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "key": "770e8400-e29b-41d4-a716-446655440000",
    "title": "Implantatkrone 46 Ti Base",
    "color": "A3",
    "...": "weitere Felder wie bei OrderCreated"
  }
}
```

{: .note }
Neue Dateien und Kommentare lösen kein `OrderUpdated` aus, dafür gibt es `MediaAdded` und `CommentAdded`.

### OrderExported

Das Labor hat den Auftrag in Condent zur Abrechnung markiert. Die Laborsoftware kann daraus z.B. einen Kostenvoranschlag oder eine Rechnung erstellen und diese über die [Billing Import API](./billing-import-api.md) zurückspielen.

Exportiert werden immer Aufträge, nie Leistungen. `data` enthält den Auftrag im selben Format wie bei [`OrderCreated`](#ordercreated):

```json
{
  "eventId": 67895,
  "eventType": "OrderExported",
  "timestamp": "2026-12-04T16:20:00Z",
  "orderKey": "770e8400-e29b-41d4-a716-446655440000",
  "data": {
    "key": "770e8400-e29b-41d4-a716-446655440000",
    "title": "Implantatkrone 46 Ti Base",
    "...": "weitere Felder wie bei OrderCreated"
  }
}
```

Ein Auftrag kann mehrmals exportiert werden. Jeder Export erzeugt ein neues `OrderExported`-Event.

## Auftrag abfragen

Liefert den aktuellen Stand eines Auftrags im selben Format wie `data` bei `OrderCreated`. Damit lässt sich ein einzelner Auftrag neu synchronisieren, z.B. nach einem Fehler in der Laborsoftware.

### Endpunkt

```http
GET /v2/order/{key}
```

| Antwort | Bedeutung |
|---------|-----------|
| `200 OK` | Auftrag gefunden |
| `404 Not Found` | Der Auftrag existiert nicht, wurde gelöscht oder gehört zu einem anderen Labor |

## Dateien herunterladen

Dafür wird die `url` einer Datei aus den Events verwendet.

```http
GET /v2/order/{key}/file/{fileKey}
```

| Parameter | Beschreibung |
|-----------|--------------|
| `key` | `key` des Auftrags |
| `fileKey` | `key` der Datei aus `files` |

Der Request muss wie alle anderen signiert werden. Die Antwort ist die Datei mit ihrem `Content-Type`.

| Antwort | Bedeutung |
|---------|-----------|
| `200 OK` | Datei |
| `404 Not Found` | Die Datei oder der Auftrag existiert nicht, wurde gelöscht oder gehört zu einem anderen Labor |

## Verwendungsbeispiel

### Initiale Synchronisation

```bash
# Erste Seite an Events abrufen (ohne cursor)
GET /v2/order/event?limit=50

# Antwort:
# { "events": [...], "nextCursor": "abc123==", "hasMore": false }
```

### Verarbeitungsablauf

```text
1. GET /v2/order/event?cursor=<gespeicherter Cursor>
2. Für jedes Event, in der erhaltenen Reihenfolge:
     - überspringen, wenn die eventId bereits verarbeitet wurde
     - verarbeiten (Auftrag erstellen, Dateien herunterladen, Kommentar ergänzen, ...)
3. nextCursor speichern
4. Bei hasMore = true sofort weiter bei 1, sonst das Polling-Intervall abwarten
```

## Fehlerantworten

Siehe [Fehlerantworten](./index.md#fehlerantworten).

## Empfohlenes Polling-Intervall

- **Minimum**: 30 Sekunden
- **Empfohlen**: 1 bis 5 Minuten

## Hinweise

- Die Events eines Labors werden in der Reihenfolge geliefert, in der sie entstanden sind, und sind in dieser Reihenfolge zu verarbeiten.
- Events werden 90 Tage aufbewahrt. Der Feed sollte mindestens einmal täglich abgefragt werden, damit keine Events verloren gehen.
- Den `nextCursor` persistent speichern, um nach einem Neustart an der letzten Position fortfahren zu können.
- Geliefert werden nur Events ab Aktivierung der Anbindung für das Labor. Bestehende Aufträge werden nicht übertragen.
- Bei `OrderDeleted`, `MediaAdded`, `CommentAdded` und `AppointmentChangeRequested` zeigt `deletedBy.type`, `addedBy.type`, `author.type` bzw. `requestedBy.type`, ob die Praxis (`Customer`) oder das Labor (`Lab`) das Event ausgelöst hat. Nicht zu verwechseln mit `type` des Auftrags (`Order`, `Production`, `Quote`).
