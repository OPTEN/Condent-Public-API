---
title: Rechnungs- und Lieferschein-XML
nav_order: 4
---

# Lieferschein-/Rechnungs-XML im PDF

{: .warning }
Die XML-Struktur befindet sich noch im Entwurfsstadium.

## Ziel

Eine empfangende Praxis soll einen Condent-Lieferschein oder eine Condent-Rechnung als PDF importieren können. Das PDF enthält ein maschinenlesbares, **Condent-eigenes XML** mit den Dokument-, Patienten-, Behandlungs- und Positionsdaten.

## PDF-Einbettung

Vorgesehen ist, das XML im PDF als PDF/A-3 einzubetten.

## Vorgeschlagene XML-Struktur

```xml
<CondentDocument xmlns="urn:condent:ch:document:1">
  <document>
    <number>LS-12345</number>
    <type>deliveryNote</type>
    <date>2026-10-08</date>
    <title>Frontzahnkrone mit Einprobe</title>
    <description>Dokumentbeschreibung</description>
    <toothColor>B1</toothColor>
    <currency>CHF</currency>
  </document>
  <patient>
    <number>12345</number>
    <condentId>C-123456</condentId>
    <salutation>Frau</salutation>
    <lastName>Muster</lastName>
    <firstName>Erika</firstName>
    <dateOfBirth>1980-01-31</dateOfBirth>
  </patient>
  <parties>
    <lab>
      <name>Dental Lab AG</name>
      <globalLocationNumber>GLN-LAB</globalLocationNumber>
      <vatIdentificationNumber>VAT-ID-LAB</vatIdentificationNumber>
      <address>
        <street>Laborstrasse 1</street>
        <postalCode>8000</postalCode>
        <city>Zürich</city>
        <countryCode>CH</countryCode>
      </address>
      <contact>
        <phone>+41 44 000 00 00</phone>
        <email>info@example.ch</email>
      </contact>
    </lab>
    <payer>
      <type>Privat</type>
      <condentId>CONDENT-PAYER-123</condentId>
      <name>Kostenträgername</name>
      <globalLocationNumber>GLN-PAYER</globalLocationNumber>
      <vatIdentificationNumber>VAT-ID-PAYER</vatIdentificationNumber>
      <address>
        <street>Kostenträgerstrasse 10</street>
        <postalCode>3000</postalCode>
        <city>Bern</city>
        <countryCode>CH</countryCode>
      </address>
    </payer>
  </parties>
  <treatment>
    <practitioner>
      <condentId>CONDENT-PRACTITIONER-123</condentId>
      <name>Behandlername</name>
    </practitioner>
    <manufacturing>Fertigung: Schweiz</manufacturing>
    <serviceDate>2026-10-01</serviceDate>
  </treatment>
  <positions>
    <position type="tariff">
      <code>TARIFF-CODE</code>
      <name>Tarifposition</name>
      <description>Tarifpositionsbeschreibung</description>
      <quantity>1</quantity>
      <unitPrice>10.50</unitPrice>
      <unitFactor>1</unitFactor>
      <amount>10.50</amount>
      <taxRate>0</taxRate>
      <additionalData>
        <warranty>false</warranty>
        <medicalTax>false</medicalTax>
        <discountPercent>10</discountPercent>
      </additionalData>
    </position>
    <position type="article">
      <code>ARTICLE-CODE</code>
      <name>Material</name>
      <description>Artikelbeschreibung</description>
      <quantity>1</quantity>
      <unitPrice>5.00</unitPrice>
      <amount>5.00</amount>
      <taxRate>0</taxRate>
      <additionalData>
        <manufacturerName>Dental Materials AG</manufacturerName>
        <orderNumber>ORD-789</orderNumber>
        <ceNumber>CE-0123</ceNumber>
        <manufacturerDescription>Keramikrohling, Typ A</manufacturerDescription>
        <composition>Zirkonoxid</composition>
        <warranty>false</warranty>
        <medicalTax>false</medicalTax>
        <lotNumbers>
          <lotNumber>
            <description>Charge Keramikrohling</description>
            <number>LOT-2026-00421</number>
          </lotNumber>
        </lotNumbers>
        <medicalDevices>
          <medicalDevice>
            <udi>00812345678905</udi>
            <productNumber>PROD-456</productNumber>
            <lotNumber>LOT-2026-00421</lotNumber>
            <serialNumber>SERIAL-98765</serialNumber>
            <expirationDate>2028-12-31</expirationDate>
            <manufacturingDate>2026-06-15</manufacturingDate>
          </medicalDevice>
        </medicalDevices>
      </additionalData>
    </position>
  </positions>
</CondentDocument>
```

### Feldregeln des Entwurfs

| Element / Bereich | Vorgeschlagene Bedeutung und Regel |
| --- | --- |
| `document/number` | Rechnungs- oder Lieferscheinnummer des exportierten Dokuments. |
| `document/type` | Dokumentart mit einem der festen Werte `invoice` oder `deliveryNote` |
| `document/date` | Belegdatum im Format `YYYY-MM-DD`. |
| `document/title`, `document/description` | Getrennte Dokumenttexte. |
| `document/toothColor` | Zahnfarbe, zum Beispiel `B1`. |
| `document/currency` | Ein ISO-4217-Währungscode, zum Beispiel `CHF` |
| `patient/number` | Patienten-ID aus der Praxis |
| `patient/condentId` | Patienten-ID von Condent |
| `patient/salutation`, `lastName`, `firstName`, `dateOfBirth` | Patientenangaben; Geburtsdatum im Format `YYYY-MM-DD`. |
| `parties/lab` | Aussteller/Labor mit Name, GLN-Nummer, MWST-Nummer, Anschrift und Kontaktangaben, soweit verfügbar. |
| `parties/payer/type` | Kostenträgerart: `Privat`, `Suva / IV / MV`, `andere Versicherung` oder `EL / Asyl / Sozial`. |
| `parties/payer` | Kostenträger mit Condent-ID, Name, GLN-Nummer, MWST-Nummer und Anschrift, soweit verfügbar. |
| `treatment/practitioner` | Behandler mit optionaler Condent-ID und Namen. |
| `treatment/manufacturing` | z. B. `Fertigung: Schweiz` |
| `treatment/serviceDate` | Leistungsdatum. Der Wert gilt für alle Positionen dieses Belegs und wird im Format `YYYY-MM-DD` ausgegeben. |
| `positions/position` | Alle Tarifpositionen und Artikel. Die Reihenfolge der Elemente entspricht der Reihenfolge der Positionen im Dokument; `type` unterscheidet die Positionsart. Gleiche Codes können mehrfach vorkommen. |
| `position/@type` | Art der Position: `tariff` für Tarifpositionen oder `article` für Artikel. |
| `position/code` | Tarif- oder Artikelcode. Codes können mehrfach vorkommen. |
| `quantity`, `unitPrice`, `amount` | Menge, Preis pro Einheit und Positionsgesamtbetrag; bei den bestehenden XML-Quelldaten ist `amount = unitPrice × quantity`. Dezimalzahlen verwenden einen Punkt als Dezimaltrennzeichen und keine Tausendertrennzeichen. |
| `unitFactor` | Optionaler effektiver numerischer Multiplikator für Positionen vom Typ `tariff` (z. B. `1`, `5.55` oder ein abweichender tariflicher Wert). Dezimalzahlen verwenden einen Punkt als Dezimaltrennzeichen und keine Tausendertrennzeichen. |
| `taxRate` | Steuersatz als Prozentzahl (z. B. `8.1` bedeutet 8,1 %); steuerfreie Positionen erhalten explizit `0`. |
| Datumsfelder | ISO-Datum im Format `YYYY-MM-DD`; gilt für Beleg-, Leistungs-, Geburts-, Herstellungs- und Ablaufdatum. |
| `additionalData` | Optionales Erweiterungselement. Das Beispiel zeigt mögliche Tarifdaten (`warranty`, `medicalTax`, `discountPercent`) und Artikeldaten (Hersteller-/Bestell-/CE-/Beschreibungs- und Zusammensetzungsangaben, Chargen sowie Medizinprodukte mit UDI, Produkt-, Chargen-/Seriennummern und Daten). |