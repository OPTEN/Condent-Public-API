---
title: Einführung
nav_order: 1
---

# Einführung

REST API Endpoint:

 - Development: https://api-dev.condent.app
 - Production: https://api.condent.app

Swagger: https://api-dev.condent.app/swagger/index.html

## Welche API für wen?

| Ihre Software | APIs | Schlüssel |
| --- | --- | --- |
| [Praxissoftware](./practice-software.md) | [Aufträge an Condent senden](./order-create-api.md), [Billing Events API](./billing-events-api.md), [Rechnungs- und Lieferschein-XML](./billing-document-xml.md) | [Praxis-Schlüssel](#praxis-schlüssel) |
| [Laborsoftware](./lab-software.md) (z.B. ERP) | [Order Events API](./order-events-api.md), [Billing Import API](./billing-import-api.md) | [Labor-Schlüssel](#labor-schlüssel) |

Authentifizierung, Serverzeit und Fehlerantworten sind für alle APIs gleich und auf dieser Seite beschrieben.

## Authentifizierung

Die folgenden HTTP-Header müssen für die Authentifizierung verwendet werden:

 - `X-CONDENT-API-KEY`: API-Schlüssel der Praxis oder des Labors (siehe unten), in Ihrer Software gespeichert
 - `X-CONDENT-TIMESTAMP`: UTC Zeitstempel in Millisekunden
 - `X-CONDENT-SIGN`: Eine aus den Request-Parametern abgeleitete Signatur

Für die Signatur benötigen Sie zusätzlich einen `SecretKey`, den Sie von uns erhalten (support@condent.ch).

### Praxis-Schlüssel

Der Praxis-Schlüssel wird vom Zahnarzt in Condent erstellt und in Ihrer Praxissoftware gespeichert. Er gibt Zugriff auf die Daten dieser Praxis.

### Labor-Schlüssel

Die [Order Events API](./order-events-api.md) und die [Billing Import API](./billing-import-api.md) werden vom Labor bzw. von dessen Software (z.B. einem ERP) verwendet. Dafür wird ein API-Schlüssel **pro Labor** benötigt, nicht ein Schlüssel des Zahnarztes. Ein Labor-Schlüssel gibt nur Zugriff auf Aufträge, Events und Dateien des eigenen Labors.

Labor-Schlüssel und `SecretKey` erhalten Sie auf Anfrage bei support@condent.ch. Die Signatur funktioniert gleich wie unten beschrieben (Sign V2).

{: .new }
Änderungen: 18.05.26 - **Sign V2**

### X-CONDENT-SIGN generieren (Sign V2)

Um eine Signatur zu generieren, benötigen sie einen `SecretKey`welchen Sie von uns erhalten (support@condent.ch).

Die Signatur setzt sich wie folgt zusammen:

1. `sign` String erstellen (siehe unten)
2. Diesen `sign` String mit HMAC SHA256 und dem `SecretKey` signieren
3. Diese Signatur zu base64 encoden

#### Sign String Aufbau (Sign V2)

Der `sign` String wird wie folgt aufgebaut (alle Teile durch `\n` getrennt):

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

**Details:**
- `apiKey`: Der API-Schlüssel (identisch mit `X-CONDENT-API-KEY`)
- `method`: HTTP-Methode in **Grossbuchstaben** (GET, POST, PUT, PATCH, DELETE)
- `scheme`: Protokoll in **Kleinbuchstaben** (https, http)
- `host`: Domain in **Kleinbuchstaben** (api.condent.app)
- `path`: Pfad in **Kleinbuchstaben** (/v1/order, /v2/billing/event)
- `querystring`: Query-Parameter in **Original-Schreibweise** inkl. `?` (?limit=50 oder leer bei keinen Parametern)
- `timestamp`: Der Unix-Timestamp in Millisekunden (identisch mit `X-CONDENT-TIMESTAMP`)
- `body`: Request Body als String (leer bei GET-Requests, Dateiname bei File-Uploads)

{: .warning }
Scheme, host und path müssen in **Kleinbuchstaben** sein! Method ist in **Grossbuchstaben**. Querystring bleibt in **Original-Schreibweise**.

{: .note }
Auch **leere Strings** (z.B. bei fehlendem querystring oder body) müssen im `sign` String enthalten sein.

#### Beispiele für Sign String (Sign V2)

**POST Request mit JSON Body:**
```
POST https://api.condent.app/v1/Order
API Key: YOUR_API_KEY
Body: {"patient":{"firstName":"Max","lastName":"Muster"}}
Timestamp: 1735034400000

sign:
YOUR_API_KEY
POST
https
api.condent.app
/v1/order

1735034400000
{"patient":{"firstName":"Max","lastName":"Muster"}}
```

**GET Request ohne Body:**
```
GET https://api.condent.app/v2/Billing/Event?limit=50
API Key: YOUR_API_KEY
Timestamp: 1735034400000

sign:
YOUR_API_KEY
GET
https
api.condent.app
/v2/billing/event
?limit=50
1735034400000

```
(Beachten Sie die leere Zeile am Ende für den leeren Body)

**GET Request mit Cursor:**
```
GET https://api.condent.app/v2/Billing/Event?cursor=abc123%3D%3D&limit=50
API Key: YOUR_API_KEY
Timestamp: 1735034400000

sign:
YOUR_API_KEY
GET
https
api.condent.app
/v2/billing/event
?cursor=abc123%3D%3D&limit=50
1735034400000

```

**PUT Request mit File Upload:**
```
PUT https://api.condent.app/v1/Order/abc123/Upload
API Key: YOUR_API_KEY
Filename: scan.stl
Timestamp: 1735034400000

sign:
YOUR_API_KEY
PUT
https
api.condent.app
/v1/order/abc123/upload

1735034400000
scan.stl
```

Weitere Beispiele finden sie unter [examples](https://github.com/OPTEN/Condent-Public-API/tree/main/examples).

Wichtig ist, dass `timestamp`nicht weniger oder mehr als 15 Sekunden Unterschied zur Serverzeit von Condent hat.
Falls Ihre Software z.B. eine auf dem Windows installierte Software ist, müssen Sie berücksichtigen, dass der Benutzer die Uhrzeit auf dem Computer (fälschlicherweise) ändern kann. Sie können die aktuelle Serverzeit von Condent [abfragen](#serverzeit).

---

### X-CONDENT-SIGN generieren (Sign V1 - Legacy)

{: .warning }
Diese Methode wird nicht mehr empfohlen. Bitte verwenden Sie Sign V2 für alle neuen Implementierungen.

**Sign V1** verwendet eine einfachere Signatur-Methode:

1. Prehash aus `timestamp` + `apiKey` + `body` erstellen
2. Diesen Prehash mit HMAC SHA256 und dem `SecretKey` signieren
3. Diese Signatur zu base64 encoden

`timestamp` entspricht dem Header `X-CONDENT-TIMESTAMP`  
`apiKey` entspricht dem Header `X-CONDENT-API-KEY`  
`body` entspricht dem Request Body z.B. `"{ patient: { firstName: 'XXXXX' } }"`

---

## Serverzeit

Eine Authentifizierung ist nicht notwendig.

#### HTTP Request

`GET /v1/time`

#### Request Parameter

Keine

#### Response Parameter

| Parameter   | Typ         | Kommentar |
| ----------- | ----------- | ----------- |
| seconds | long | Condent Zeitstempel in Sekunden       |
| milliseconds | long  | Condent Zeitstempel in Millisekunden     |


#### Request Beispiel

`curl https://api-dev.condent.app/v1/time`

#### Response Beispiel

```
{
  seconds: 1677067413,
  milliseconds: 1677067413774
}
```

## Fehlerantworten

Bei einem Fehler antwortet die API mit einem HTTP-Statuscode und einer Liste von Meldungen.

```json
{
  "messages": ["Invalid API Key."]
}
```

| Statuscode | Bedeutung |
|------------|-----------|
| `400 Bad Request` | Ungültige Anfrage, z.B. fehlende Pflichtfelder oder ein `limit` ausserhalb des erlaubten Bereichs |
| `401 Unauthorized` | Authentifizierung fehlgeschlagen: ungültiger API-Schlüssel, ungültige Signatur oder `X-CONDENT-TIMESTAMP` mehr als 15 Sekunden von der [Serverzeit](#serverzeit) entfernt |
| `404 Not Found` | Die angefragte Ressource existiert nicht oder gehört nicht zu Ihrem API-Schlüssel |
| `500 Internal Server Error` | Fehler bei Condent. Wiederholen Sie die Anfrage später. |

{: .note }
Die Meldungen in `messages` sind für Entwickler:innen gedacht und können sich ändern. Werten Sie in Ihrer Software den Statuscode aus, nicht den Text.
