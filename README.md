# Sam's Warehouse

An ASP.NET Core MVC web app for a fictional warehouse store, where users can register, browse products and build shopping lists. Built as a UI assignment for my Diploma of IT (Advanced Programming) at TAFE Queensland, 2024.

## Features

- User registration, login and logout
- Browse the product catalogue
- Create, view, edit and delete shopping lists
- Add and remove products on a shopping list

## Tech

- C# / ASP.NET Core MVC (.NET 8)
- Entity Framework Core with SQL Server
- Razor views, HTML, CSS, JavaScript

## Design

- MVC structure with controllers for Users, Products and Shopping Lists
- Repository pattern with interfaces separating data access from controllers
- View models for login and shopping list screens

## Running it locally

1. Install the .NET 8 SDK and SQL Server Express
2. Check the connection string in `appsettings.json` points to your SQL Server instance
3. Open `SamsWarehouse.sln` in Visual Studio
4. Run `Update-Database` in the Package Manager Console if migrations are included, then run the project
