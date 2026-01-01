# Prosty sklep – koszyk i zamówienia (ASP.NET Core)

Projekt przedstawia prostą aplikację typu sklep internetowy z:
- listą produktów,
- koszykiem,
- finalizacją zamówienia (checkout),
- zapisem zamówień do bazy danych.

Backend został zrealizowany w **ASP.NET Core Web API**, a frontend jako **minimalny HTML + JavaScript**.

---

## Funkcjonalności

### Produkty
- CRUD produktów (id, name, price)
- Walidacja ceny (price >= 0)
- Lista produktów dostępna przez API i UI

### Koszyk
- Dodawanie produktów do koszyka
- Modyfikacja ilości
- Usuwanie pozycji
- Koszyk oparty o sesję użytkownika

### Zamówienie (Checkout)
- Utworzenie zamówienia na podstawie koszyka
- Zapis do bazy danych (`Orders`, `OrderItems`)
- Snapshot ceny produktu w momencie zakupu
- Wyliczenie sumy zamówienia
- Wyczyszczenie koszyka po checkout

---

## Model danych

```sql
Products(Id, Name, Price)
Orders(Id, CreatedAt)
OrderItems(Id, OrderId, ProductId, Qty, Price)
```
 - Price w OrderItems jest snapshotem ceny z momentu zamówienia
 - Walidacja ilości: Qty > 0

---

## Technologie

 - .NET 8
 - ASP.NET Core Web API
 - Entity Framework Core
 - SQL Server
 - HTML + JavaScript (bez frameworków)

---

## Uruchomienie projektu

- Skonfiguruj połączenie do SQL Server w appsettings.json
- Uruchom aplikację:
```
dotnet run
```
- Aplikacja będzie dostępna pod adresem
```
https://localhost:XXXX
```
- Swagger:
```
https://localhost:XXXX/swagger
```
- Strona z UI:
```
https://localhost:XXXX/index.html
```
XXXX odpowiada portowi, który może być różny dla każdego urządzenia przy uruchomieniu programu.

---

## UI

Minimalny interfejs użytkownika znajduje się w:
```
wwwroot/index.html
```
Umożliwia:
- przeglądanie produktów,
- dodawanie do koszyka,
- edycję ilości,
- finalizację zamówienia.

---

## Testy API

Plik tests.rest zawiera przykładowe wywołania API:
- pobieranie produktów,
- operacje na koszyku,
- checkout.

Testy przedstawiają poprawny scenariusz „happy path”.

---

## Zrzuty Ekranu

Do projektu dołączono screenshoty prezentujące:
- operacje na produktach,
- dodawanie/usuwanie/aktualizację produktów w koszyku,
- checkout,
- testy z dodawaniem nieprawidłowych danych (np. ujemne ceny i ilości),
- funkcjonowanie UI

---

## Uwagi końcowe

- Walidacja danych realizowana jest na poziomie DTO, modelu oraz bazy danych
- Kontrolery nie powielają walidacji
- Zastosowano DTO (record) do komunikacji API
- Modele domenowe nie zawierają kolekcji (tylko referencje przez ID)
