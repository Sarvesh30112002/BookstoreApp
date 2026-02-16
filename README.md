# BookstoreApp
# 📚 Bookstore Web Application – ASP.NET Core MVC + EF Core + SQLite

A full-stack Bookstore management web application built using **ASP.NET Core MVC**, **Entity Framework Core**, and **SQLite**.  
This project was developed as part of an onboarding assignment to demonstrate backend development, clean architecture, data access, validation, pagination, search and modern backend integration with an AI service.

---

## 🎯 Project Objective

Develop a basic web application for managing a bookstore that allows users to perform:

- Create
- Read
- Update
- Delete (CRUD)

operations on a list of books using ASP.NET Core and Entity Framework Core.

---

## ✨ Key Features

### ✔ Core Features

- Create new books
- View all books
- View detailed information of a book
- Edit existing books
- Delete books
- SQLite database with Entity Framework Core
- Clean MVC architecture

---

### ✔ Validation

- Server-side validation using Data Annotations
- Client-side validation using jQuery unobtrusive validation
- Inline validation messages
- Validation summary support

---

### ✔ Bonus Features

- 🔍 Search books by:
  - Title
  - Author
- 📄 Pagination using:
  - Page number
  - Page size
  - EF Core `Skip()` and `Take()`
- 🎨 UI styling using Bootstrap
- 🔔 Toast notifications for:
  - Create
  - Update
  - Delete

---

### ✔ Advanced Bonus – AI Book Summary (Backend Feature)

- AI summary is generated only when the user clicks a button
- Backend endpoint is used (no direct call from view)
- Gemini API is used
- Cost-optimized (no automatic calls on page load)
- Clean service abstraction
- Fallback logic:
  - If known public information is available → factual summary
  - Otherwise → guessed description based on title and author

---

## 🧰 Technology Stack

- ASP.NET Core 9 (MVC)
- C#
- Entity Framework Core
- SQLite
- Razor Views
- Bootstrap 5
- Gemini API (AI summary feature)

---

## 🏗 Architecture

The application follows standard **ASP.NET Core MVC architecture**.

- Models → Domain models
- Controllers → Request handling and business logic
- Views → Razor views
- Services → AI integration logic
- Data → EF Core DbContext

---

## 🧩 Book Model

```text
Id     : int (Primary Key)
Title  : string (Required)
Author : string (Required)
Genre  : string (Optional)
Price  : decimal (Required)
```

🗃 Database

Provider: SQLite

ORM: Entity Framework Core

Context: BookstoreContext

Table:

Books

🧭 BooksController – Implemented Actions

Index (GET)

Details (GET)

Create (GET)

Create (POST)

Edit (GET)

Edit (POST)

Delete (GET)

Delete (POST)

GetAiSummary (GET – backend API endpoint)

🔍 Search

Books can be filtered using:

Title

Author

Search is case-insensitive and handled through Entity Framework Core.

📄 Pagination

Page number

Page size

Skip() / Take()

Stable ordering before pagination

✔ Validation

Client side:

jQuery unobtrusive validation

Server side:

ModelState validation

Field-level and summary validation messages are shown

🎨 UI & UX

Bootstrap layout

Styled forms and tables

Responsive pages

Confirmation workflow for delete

Toast notifications

Consistent layout and navigation

🤖 AI Summary Feature (Gemini)

Implemented as a backend service (AiSummaryService)

Triggered only when the user clicks Generate AI summary

Uses Gemini API

API call is never performed during page load

Clean error handling and fallback message

▶ How to Run the Application
✅ Prerequisites

Visual Studio 2022

.NET 9 SDK

Internet connection (only required for AI feature)

Step 1 – Clone the repository
git clone <your-repository-url>

Step 2 – Open the solution

Open:

BookstoreApp.sln


in Visual Studio 2022.

Step 3 – Restore packages

Visual Studio will restore automatically.

Or manually:

dotnet restore

Step 4 – Configure database

Open Package Manager Console:

Tools → NuGet Package Manager → Package Manager Console


Run:

Update-Database


This creates the SQLite database.

Step 5 – Configure Gemini API

Create a file:

appsettings.Development.json


Add:

{
  "Gemini": {
    "ApiKey": "YOUR_GEMINI_API_KEY"
  }
}


In appsettings.json keep:

"Gemini": {
  "ApiKey": ""
}

Step 6 – Run the application

Press:

F5


or click Start in Visual Studio.

🔐 Important Security Note

Do NOT commit API keys.

🔒 Recommended Secret Handling

Use:

appsettings.Development.json


and add it to .gitignore.

appsettings.Development.json


must never be committed.

🧪 Testing the AI Feature

Open a book’s details page

Click Generate AI summary

The backend endpoint is called

The result is displayed

📂 Simplified Project Structure
BookstoreApp
 ├── Controllers
 │    └── BooksController.cs
 ├── Data
 │    └── BookstoreContext.cs
 ├── Models
 │    └── Book.cs
 ├── Services
 │    └── AiSummaryService.cs
 ├── Views
 │    └── Books
 │         ├── Index.cshtml
 │         ├── Details.cshtml
 │         ├── Create.cshtml
 │         ├── Edit.cshtml
 │         └── Delete.cshtml
 ├── wwwroot
 ├── Program.cs
 └── appsettings.json

👤 Author

Sarvesh Hadole
Software Engineer
