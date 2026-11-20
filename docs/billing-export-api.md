---
title: Billing Export API
nav_exclude: true
---

# Billing Export API

{: .warning }
Die Billing Export API wird nicht weitergeführt. Bitte verwenden Sie stattdessen die folgenden APIs.

| Bisher | Neu |
| --- | --- |
| Exportierte Aufträge abfragen (`GET /v2/billing/export-events`) | [Order Events API](./order-events-api.md) mit dem Event-Typ `OrderExported` (Filter `types=OrderExported`) |
| Leistungen importieren (`POST /api/billing/import`) und Dateien hochladen | [Billing Import API](./billing-import-api.md) |
