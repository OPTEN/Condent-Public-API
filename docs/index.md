---
title: API-Anbindung
nav_order: 1
---

# Einleitung

REST API Endpoint:

 - Development: https://api-dev.condent.app
 - Production: https://api.condent.app

## Authentifizierung

Die folgenden HTTP-Header müssen für die Authentifizierung verwendet werden:

 - `X-CONDENT-API-KEY`: API Schlüssel, welcher vom Zahnarzt erstellt und in Ihrer Software gespeichert wird
 - `X-CONDENT-TIMESTAMP`: UTC Zeitstempel in Millisekunden
 - `X-CONDENT-SIGN`: Eine aus den Request-Parametern abgeleitete Signatur

### X-CONDENT-SIGN generieren

Um eine Signatur zu generieren, benötigen sie einen `SecretKey`welchen Sie von uns erhalten (support@condent.ch).

Die Signatur setzt sich wie folgt zusammen:

1. Prehash aus`timestamp` + `apiKey` + `body` erstellen
2. Diesen Prehash mit HMAC SHA256 und dem `SecretKey` signieren
3. Diese Signatur zu base64 encoden

`timestamp` entspricht dem Header `X-CONDENT-TIMESTAMP`
`apiKey` entspricht dem Header `X-CONDENT-API-KEY`
`body` entspricht dem Request Body z.B. `"{ patient: { firstName: 'XXXXX' } }"`

Beispiele finden sie unter [examples](https://github.com/OPTEN/Condent-Public-API/tree/main/examples).

Wichtig ist, dass `timestamp`nicht weniger oder mehr als 15 Sekunden Unterschied zur Serverzeit von Condent hat.
Falls Ihre Software z.B. eine auf dem Windows installierte Software ist, müssen Sie berücksichtigen, dass der Benutzer die Uhrzeit auf dem Computer (fälschlicherweise) ändern kann. Sie können die aktuelle Serverzeit von Condent [abfragen](#serverzeit).

---

> &nbsp;
> ⚠️ **Änderungen: 27.02.26**
> &nbsp;

## Änderungen am Adressmodell

Aufgrund regulatorischer Anpassungen in der Schweiz wird das Adressmodell auf eine strukturierte Form umgestellt.

### Änderungen

- `patient.address.address1`  
  Wird als **deprecated** markiert.  
  Bitte stattdessen die neuen strukturierten Felder verwenden:
  - `patient.address.streetName`
  - `patient.address.buildingNumber`
- `patient.address.address2`  
  Wird als **obsolet** markiert und in einer zukünftigen API-Version entfernt.  
  Dieses Feld sollte nicht mehr verwendet werden.

### Grund

Diese Anpassung erfolgt im Kontext einer regulatorischen Änderung der Swiss Payment Standards.  
Für Zahlungsüberweisungen sowie die Generierung von QR-Rechnungen wird künftig eine strukturierte Empfängeradresse benötigt.

---

## Patient mit oder ohne Auftragsdaten an Condent schicken

Eine vollständige Dokumentation der API finden Sie hier: https://api-dev.condent.app/swagger/index.html

#### HTTP Request

`POST /v1/order`

#### Request Parameter

| Parameter   | Pflicht     | Typ         | Kommentar |
| ----------- | ----------- | ----------- | ----------- |
| title       | Nein        | string      | Der Titel des Auftrags       |
| costUnitType   | Nein        | string        | Kostenträger des Auftrags       |
| insurances   | Nein        | string array        | Versicherungen des Patienten        |
| color | Nein        | string | Farbe z.B. für die Krone oder Schiene        |
| description | Nein        | string        | Beschreibung des Auftrags |
| scanSentVia | Nein        | string | Scan versendet via |
| trackingNumber | Nein        | string | Trackingnummer (z.B. Post) des Auftrags |
| patient | **Ja**        | object | Patientendaten |
| patient.reference | **Ja**        | string | Eindeutige Patientennummer Ihrer Praxissoftware |
| patient.firstName | **Ja**        | string | Vorname des Patienten |
| patient.lastName | **Ja**        | string | Nachname des Patienten |
| patient.birthday | Nein        | date | Geburtstag des Patienten in UTC (z.B. 1991-01-01T00:00:00Z) |
| patient.email | Nein        | string | E-Mail-Adresse des Patienten |
| patient.phoneNumber | Nein        | string | Telefonnummer des Patienten. Format: +(CountryCode)(NationalNumber) (z.B. +41761234455) |
| patient.address | Nein        | object | Adresse des Patienten |
| patient.address.address1 | Nein | string | ⚠️ Deprecated – bitte streetName + buildingNumber verwenden |
| patient.address.address2 | Nein | string | ❌ Obsolet – wird entfernt |
| patient.address.streetName | Nein | string | Strassenname |
| patient.address.buildingNumber | Nein | string | Hausnummer |
| patient.address.zipCode | Nein        | string | PLZ |
| patient.address.city | Nein        | string | Ort |
| patient.address.countryCode | Nein        | string | Land in [Alpha-2 code](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2). Condent unterstützt nicht alle Länder. Falls ein Land gesendet wird, welches wir nicht führen, kann der Patient/Auftrag trotzdem erfasst werden. |
| appointments | Nein | array | Termine |
| appointments.date | Nein | date | Datum und Zeit des Termins in UTC (z.B. 2023-02-22T11:43:00Z) |
| appointments.description | Nein | string | Beschreibung des Termins |
| attachments | Nein | array | Beilagen |
| attachments.upperJaw | Nein | bool | OK |
| attachments.lowerJaw | Nein | bool | UK |
| attachments.description | Nein | string | Beschreibung der Beilage |

---

#### Response Parameter

| Parameter   | Typ         | Kommentar |
| ----------- | ----------- | ----------- |
| url       | string      | URL für die Erfassung des Auftrags auf Condent       |
| uploadToken       | string      | [Token um Dateien zu hochzuladen](#dateien-hochladen)       |
| warnings       | string array     | Die Warnungen sind für die Entwickler:innen gedacht. Diese Warnungen weisen z.B. darauf hin, dass die gesendete Telefonnummer im falschen Format ist (z.B. wurde 076 123 44 55 statt +41761234455 gesendet). Der Patient resp. Auftrag kann trotz Warnungen erfasst werden.      |

#### Request Beispiel

```
POST  /v1/order  HTTP/1.1  
Host:  api-dev.condent.app  
X-CONDENT-SIGN:  XXXXX  
X-CONDENT-API-KEY:  XXXXX  
X-CONDENT-TIMESTAMP:  1672211928338
Content-Type:  application/json

{
   patient: {
      reference: 'XXXXX'
      firstName: 'XXXXX'
      lastName: 'XXXXX'
   }
}
```

`X-CONDENT-SIGN` wird aus dem geschickten JSON generiert.

#### Response Beispiel

```
{
  url: 'https://my-dev.condent.app/xyz',
  uploadToken: 'xyz'
  warnings: []
}
```

## Dateien hochladen

Sie müssen zuerst eine [Order](#patient-mit-oder-ohne-auftragsdaten-an-condent-schicken) erstellen, um Dateien hochzuladen. Den `UploadToken` erhalten Sie im Response der Order.

Aktuell gelten folgende Beschränkungen:
- Grösse bis zu maximal 100 MB
- Jegliche Dateiformate werden akzeptiert (JPG, PNG, ZIP, PDF, STL, 3OX, ZIP, ...)

(Sofern möglich, werden wir Dateien wie z.B. Bilder komprimieren.)

Dateien müssen einzeln geschickt werden, dies um Timeouts zu verhindern (wenn z.B. sehr viele, sehr grosse Dateien geschickt werden). Wir empfehlen, dass die Requests parallel geschickt werden (ein Beispiel in C# finden Sie [hier](https://github.com/OPTEN/Condent-Public-API/blob/main/examples/Opten.Condent.DentistApp/CondentAppForm.cs)).

Wenn es sehr viele Dateien sind, kann auch ein ZIP-File geschickt werden.

Grundsätzlich kann der Auftrag auf Condent erfasst werden, auch wenn einzelne Dateien durch einen Fehler nicht übermittelt werden konnten. Der Zahnarzt hat die Möglichkeit, die Dateien auch manuell auf Condent hochzuladen oder zu löschen.

#### HTTP Request

`PUT /v1/order/{UploadToken}/upload`

#### Request Body

Sie müssen die Datei als multipart/form-data schicken.

#### Request Beispiel

```
PUT  /v1/order/XXXXX/upload  HTTP/1.1  
Host:  api-dev.condent.app  
X-CONDENT-SIGN:  XXXXX  
X-CONDENT-API-KEY:  XXXXX  
X-CONDENT-TIMESTAMP:  1672211928338
Content-Type:  multipart/form-data; boundary=------------------------d74496d66958873e

--------------------------d74496d66958873e
Content-Disposition: form-data; name="file.jpg"; filename="file.jpg"
Content-Type: image/jpeg
```

`X-CONDENT-SIGN` wird aus dem geschickten Dateinamen (file.jpg) generiert.

#### Response Beispiel

```
HTTP/1.1 200 OK
```

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