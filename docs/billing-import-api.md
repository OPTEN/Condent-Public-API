---
title: Billing Import API
parent: Laborsoftware
nav_order: 2
---

# Billing Import API

{: .warning }
Diese API befindet sich noch in Entwicklung. Änderungen vorbehalten.

## Übersicht

Über diese API können in Ihrem System erzeugte Leistungen (z.B. Rechnungen oder Kostenvoranschläge) nach Condent zurückgespielt werden. Dabei können auch Dateien (z.B. PDF, XML usw.) übertragen werden.

Der API-Schlüssel wird **pro Labor** ausgestellt, siehe [Labor-Schlüssel](./index.md#labor-schlüssel).

Die Leistung wird einem Auftrag zugeordnet. Den `key` des Auftrags erhalten Sie aus der [Order Events API](./order-events-api.md), z.B. aus einem `OrderExported`-Event.

## Authentifizierung (Sign V2)

Siehe [hier](./index.md#authentifizierung).

**Wichtig:** Alle Endpunkte in dieser API verwenden **Sign V2** für die Authentifizierung.

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

Details und Beispiele finden Sie in der [Authentifizierungs-Dokumentation](./index.md#x-condent-sign-generieren-sign-v2).

## Leistungen importieren bzw. erstellen (Kostenvoranschlag, Rechnung usw.)

### Endpunkt

```http
POST /v2/billing/import
```

Swagger: https://api-dev.condent.app/swagger/index.html

### Request-Parameter

| Parameter   | Pflicht     | Typ         | Kommentar |
| ----------- | ----------- | ----------- | ----------- |
| key       | Ja        | string      | Interne Condent-ID des Auftrags aus der [Order Events API](./order-events-api.md)       |
| title       | Nein        | string      | Der Titel der Leistung       |
| color | Nein        | string | Farbe, z.B. für Krone oder Schiene        |
| description | Nein        | string        | Beschreibung der Leistung |
| type | Ja        | string        | Typ der Leistung, siehe [Leistungstypen](#leistungstypen) |
| date | Ja        | date        | Datum der Leistung (z.B. Rechnungsdatum) im Format ISO 8601, z.B. `2026-11-20` |

### Leistungstypen

| Wert | Beschreibung |
|------|--------------|
| `CostEstimate` | Kostenvoranschlag |
| `DeliveryBill` | Lieferschein |
| `Invoice` | Rechnung |
| `Credit` | Gutschrift |

---

### Response-Parameter

| Parameter   | Typ         | Kommentar |
| ----------- | ----------- | ----------- |
| url       | string      | URL der Leistung auf Condent       |
| uploadToken       | string      | [Token um Dateien zu hochzuladen](#dateien-hochladen)       |
| warnings       | string array     | Die Warnungen sind für Entwickler:innen gedacht. Sie weisen beispielsweise darauf hin, dass bestimmte Textfelder zu lang sind. Die Leistung kann trotz dieser Warnungen erfasst werden.      |

## Dateien hochladen

Um Dateien hochzuladen, muss zuerst eine [Leistung](#leistungen-importieren-bzw-erstellen-kostenvoranschlag-rechnung-usw) erstellt werden. Den benötigten `uploadToken` erhalten Sie in der Response des Import-Requests.

Aktuell gelten folgende Beschränkungen:
- Maximale Dateigrösse: 100 MB
- Alle Dateiformate werden akzeptiert (z.B. JPG, PNG, ZIP, PDF, STL, 3OX usw.)

### Endpunkt

```http
PUT /v2/billing/import/{uploadToken}/upload
```

Die Datei wird als `multipart/form-data` geschickt, gleich wie beim [Hochladen von Auftragsdateien](./order-create-api.md#dateien-hochladen). Für die Signatur (Sign V2) ist der Dateiname der Body.

```
apiKey
PUT
https
api-dev.condent.app
/v2/billing/import/xxxxx/upload

timestamp
rechnung.pdf
```

{: .highlight }
Der `uploadToken` im Pfad wird für die Signatur wie der ganze Pfad in **Kleinbuchstaben** geschrieben.

Sofern möglich, werden bestimmte Dateien (z.B. Bilder) serverseitig komprimiert.

Dateien müssen einzeln übertragen werden, um Timeouts zu vermeiden (z.B. bei vielen oder grossen Dateien). Wir empfehlen parallele Requests.

Ein Beispiel in C# finden Sie [hier](https://github.com/OPTEN/Condent-Public-API/blob/main/examples/Opten.Condent.DentistApp/CondentAppForm.cs).

Alternativ können mehrere Dateien gesammelt als ZIP-Datei übertragen werden.

Eine Leistung wird in Condent auch dann erstellt, wenn einzelne Dateien nicht erfolgreich übertragen wurden. Das Labor kann Dateien anschliessend manuell hochladen oder entfernen.

Das Format für Rechnungs- und Lieferschein-XML finden Sie [hier](./billing-document-xml.md).

## Fehlerantworten

Siehe [Fehlerantworten](./index.md#fehlerantworten).
