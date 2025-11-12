# rental-manager-sys
An Equipment Rental Management System for a construction company.

This was a group project designed to manage the equipment rental lifecycle — from issuing to returning items — with role-based access control for Admins and Users.
It consists of two main parts:
- Backend
- Frontend

Features:
- ✅ ASP.NET Core Web API
- ✅ Entity Framework Core (SQL Server or In-Memory)
- ✅ Repository + Unit of Work Pattern
- ✅ Google OAuth2 Authentication
- ✅ Role-Based Authorization (Admin / User)
- ✅ Angular Frontend integrated with secure API endpoints


## Tech Stack
| Layer        | Technology                                       |
|--------------|--------------------------------------------------|
| **Backend**  | ASP.NET Core Web API, C#, EF Core                |
| **Frontend** | Angular, TypeScript, Bootstrap                   |
| **Database** | SQL Server / InMemory for demo                   |
| **Auth**     | Google OAuth2 (via OpenID Connect)               |
| **Tools**    | Visual Studio 2022, VS Code, Postman, Swagger UI |


## Prerequisites
- .NET 8 SDK or compatible version.
- IDE: Visual Studio 2022, VS Code, or any C#/.NET editor.
- Optional: Postman, curl, or Swagger UI for API testing.


## Project Setup Instructions
- First clone the Repository
- Configure Google OAuth credentials as environment variables or in the appsettings.json
####
    "Authentication": {
      "Google": {
        "ClientId": "YOUR_GOOGLE_CLIENT_ID",
        "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET"
      }
    }
####
- Run the backend "dotnet run" and test the endpoints in swagger
- Navigate to the frontend and install dependencies: npm install
- Run the frontend: ng serve


## Authentication, Functions & API Development
- After successful Google login, the user profile and role are stored in localStorage.
- The backend maintains session cookies for authenticated API access (withCredentials: true).
- Logout clears the local session and redirects to the backend logout endpoint.
- Full CRUD operations.
- Seed the database with one Admin and multiple Users for testing authentication and role-based access.

### The system has two user profiles and allows:
- Admins to manage equipment, customers, and rentals.
- Users (customers) to browse equipment, issue, and return rentals.


## Client Development
- Login Page → Authenticate users.
- Admin Dashboard → Manage equipment, customers, rentals, overdue (Admin).
- User Dashboard → Browse available equipment + manage rentals.
- Equipment Pages → CRUD for Admin, Details/Issue for User.
- Customer Pages → Manage customers (Admin) / Update profile (User).
- Rental Pages → Issue, return, extend, or cancel rentals.
- Validation & Feedback → Client + server validation; modals/toasts for messages.


## How to Run
- Clone the repository.
- Restore dependencies.
- Run the application.


## How to Test
- Navigate to http://localhost:5027/swagger/index.html
- Authenticate as Admin or User.
- Use the Authorize button to test secured endpoints.
- Explore CRUD operations for:
  - Equipment
  - Customers
  - Rentals


## Developers
- Johnstanley Ajagu
- Yijia Wang
