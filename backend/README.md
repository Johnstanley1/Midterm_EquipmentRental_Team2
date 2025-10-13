# rental-manager-sys
An Equipment Rental Management System for a construction company.

This is a Group project, and it is an Equipment Rental Management System for a construction company, developed in two parts.
- Backend
- Frontend
  
It also includes:
- ✅ ASP.NET Core Web API 
- ✅ Entity Framework Core (SQL Server or In-Memory) 
- ✅ Repository + Unit of Work Pattern 
- ✅ JWT Authentication & Role-Based Authorization 
- ✅ Client Application consuming the API


## Tech Stack 
- 🖥️ ASP.NET for API development
- 🖥️ Frontend TBD


# Prerequisites
- .NET 8 SDK or compatible version.
- IDE: Visual Studio 2022, VS Code, or any C#/.NET editor.
- Optional: Postman, curl, or Swagger UI for API testing.


## Project Structure
-

## Authentication & Functions
The system has two user profiles and allows: 
- Admins to manage equipment, customers, and rentals.
- Users (customers) to browse equipment, issue, and return items.


## Client Development
- Login Page → Authenticate users.
- Admin Dashboard → Manage equipment, customers, rentals, overdue (Admin).
- User Dashboard → Browse available equipment + manage rentals.
- Equipment Pages → CRUD for Admin, Details/Issue for User.
- Customer Pages → Manage customers (Admin) / Update profile (User).
- Rental Pages → Issue, return, extend, or cancel rentals.
- Validation & Feedback → Client + server validation; modals/toasts for messages.


## API Development
- POST /api/auth/login – Authenticate user & return JWT token with role claims.
- Full CRUD operations.
- Seed the database with one Admin and multiple Users for testing authentication and role-based access.


## How to Run
- Clone the repository.
- Restore dependencies.
- Run the application.


## How to Test
- Navigate to https://localhost:7038/swagger
- Choose between v1 and v2 tabs for the Books API.
- v1 endpoint hides Genre and PublishedYear; v2 endpoint expose them.
- Log in to get a token, user or admin.
- Click the authorize tab and input the token generated.
- Test all CRUD operations by version.


## Developers
- Johnstanley Ajagu
- Yijia Wang
