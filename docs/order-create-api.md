---
title: Aufträge an Condent senden
parent: Praxissoftware
nav_order: 1
---

# Aufträge an Condent senden

Mit diesen Endpunkten schickt eine Praxissoftware Patienten und Aufträge inkl. Dateien an Condent. Sie benötigen dafür einen [Praxis-Schlüssel](./index.md#praxis-schlüssel). Wie Requests signiert werden, steht in der [Einführung](./index.md#authentifizierung).

{: .new }
Änderungen: 27.02.26

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

#### Request-Parameter

| Parameter   | Pflicht     | Typ         | Kommentar |
| ----------- | ----------- | ----------- | ----------- |
| title       | Nein        | string      | Der Titel des Auftrags       |
| costUnitType   | Nein        | string        | Kostenträger des Auftrags. Mögliche Werte: `Private` (Privat), `Suva` (Suva / IV / MV), `AccidentInsurance` (Unfall / UVG), `HealthInsurance` (Krankheit / KVG), `Social` (EL / Asyl / Sozial), `StandardSupply` (Grundversorgung), `SimilarSupply` (Gleichartige Versorgung), `DifferentSupply` (Andersartige Versorgung) |
| insurances   | Nein        | string array        | Versicherungen des Patienten        |
| color | Nein        | string | Farbe, z.B. für Krone oder Schiene        |
| description | Nein        | string        | Beschreibung des Auftrags |
| scanSentVia | Nein        | string | Scan versendet via. Mögliche Werte: `DSCore`, `ThreeShape`, `ITero`, `Shining3D`, `Medit`, `StraumannAxs`, `Other` |
| trackingNumber | Nein        | string | Trackingnummer (z.B. Post) des Auftrags |
| patient | **Ja**        | object | Patientendaten |
| patient.reference | **Ja**        | string | Eindeutige Patientennummer Ihrer Praxissoftware |
| patient.salutation | Nein        | string | Anrede des Patienten. Mögliche Werte siehe unten. |
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

##### Mögliche Werte für `patient.salutation`

| Wert                | Beschreibung                  |
| ------------------- | ----------------------------- |
| `mr`                | Herr                          |
| `ms`                | Frau                          |
| `mrdr`              | Herr Dr.                      |
| `msdr`              | Frau Dr.                      |
| `mrdrdr`            | Herr Dr. Dr.                  |
| `msdrdr`            | Frau Dr. Dr.                  |
| `mrprof`            | Herr Prof.                    |
| `msprof`            | Frau Prof.                    |
| `mrprofdr`          | Herr Prof. Dr.                |
| `msprofdr`          | Frau Prof. Dr.                |
| `mrprofdrdr`        | Herr Prof. Dr. Dr.            |
| `msprofdrdr`        | Frau Prof. Dr. Dr.            |
| `mrdrmed`           | Herr Dr. med.                 |
| `msdrmed`           | Frau Dr. med.                 |
| `mrdrmeddent`       | Herr Dr. med. dent.           |
| `msdrmeddent`       | Frau Dr. med. dent.           |
| `mrdrdrmed`         | Herr Dr. Dr. med.             |
| `msdrdrmed`         | Frau Dr. Dr. med.             |
| `mrdrdrmeddent`     | Herr Dr. Dr. med. dent.       |
| `msdrdrmeddent`     | Frau Dr. Dr. med. dent.       |
| `mrprofdrmed`       | Herr Prof. Dr. med.           |
| `msprofdrmed`       | Frau Prof. Dr. med.           |
| `mrprofdrmeddent`   | Herr Prof. Dr. med. dent.     |
| `msprofdrmeddent`   | Frau Prof. Dr. med. dent.     |
| `mrprofdrdrmed`     | Herr Prof. Dr. Dr. med.       |
| `msprofdrdrmed`     | Frau Prof. Dr. Dr. med.       |
| `mrprofdrdrmeddent` | Herr Prof. Dr. Dr. med. dent. |
| `msprofdrdrmeddent` | Frau Prof. Dr. Dr. med. dent. |
| `dres`              | Dres.                         |
| `dresmed`           | Dres. med.                    |
| `dresmeddent`       | Dres. med. dent.              |

---

#### Response-Parameter

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

**Sign V2:** Der `sign` String für `X-CONDENT-SIGN` wird wie folgt erstellt:

```
apiKey
PUT
https
api-dev.condent.app
/v1/order/xxxxx/upload

timestamp
file.jpg
```

(Der Dateiname "file.jpg" ist der Body-Parameter für die Signatur)

#### Response Beispiel

```
HTTP/1.1 200 OK
```
