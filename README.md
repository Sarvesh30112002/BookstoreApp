📚 Bookstore Web Application – ASP.NET Core + EF Core + SQLite
==============================================================

A full-featured Bookstore management web application built using **ASP.NET Core MVC**, **Entity Framework Core**, and **SQLite**.The application allows users to manage books with complete CRUD functionality and includes bonus features such as search, pagination, validation, and an AI-powered book summary generator.

This project was developed as part of an onboarding technical assignment to demonstrate backend development skills using .NET technologies.

✨ Features
----------

### Core Features

*   Create, Read, Update and Delete books
    
*   Display all books in a clean and paginated list
    
*   View detailed information for a single book
    
*   Server-side and client-side validation
    
*   SQLite database integration using Entity Framework Core
    

### Bonus Features

*   Search books by:
    
    *   Title
        
    *   Author
        
*   Pagination using EF Core (Skip / Take)
    
*   Bootstrap based UI styling
    
*   Toast notifications for create / update / delete actions
    
*   AI-powered book summary (on demand only – backend driven)
    

### AI Feature (Additional Bonus)

*   Generates a short professional summary for a book
    
*   Summary is generated **only when user clicks a button** (no automatic calls)
    
*   Uses **Gemini API**
    
*   Optimized to avoid unnecessary API usage and billing
    
*   Backend-only implementation via a dedicated service
    

The AI logic:

*   First attempts to use known public knowledge
    
*   If information is not found, it generates a short reasonable guess based on the title and author
    

🧰 Technology Stack
-------------------

*   ASP.NET Core 9 (MVC)
    
*   Entity Framework Core
    
*   SQLite
    
*   Bootstrap 5
    
*   C#
    
*   Razor Views
    
*   Gemini API (for AI summary feature)
    

🏗️ Project Architecture
------------------------

This application follows the standard **ASP.NET Core MVC architecture**.

*   Models – domain entities
    
*   Controllers – request handling and business logic
    
*   Views – Razor views
    
*   Services – backend services (AI summary service)
    
*   Data – database context and migrations
    

📦 Database
-----------

*   Database Provider: SQLite
    
*   ORM: Entity Framework Core
    
*   Context: BookstoreContext
    
*   Table:
    
    *   Books
        

🧩 Book Model
-------------

The Book entity contains the following fields:

*   Id (int, primary key)
    
*   Title (string, required)
    
*   Author (string, required)
    
*   Genre (string, optional)
    
*   Price (decimal, required)
    

Validation is implemented using data annotations.

📌 Implemented Endpoints (BooksController)
------------------------------------------

*   Index – list of books
    
*   Details – view a single book
    
*   Create – add a new book
    
*   Edit – update an existing book
    
*   Delete – remove a book
    
*   GetAiSummary – backend endpoint to generate AI summary on demand
    

🔍 Search Feature
-----------------

Users can filter the book list by:

*   Title
    
*   Author
    

The search is case-insensitive and performed at the database level using Entity Framework Core.

📄 Pagination
-------------

Pagination is implemented using:

*   page number
    
*   page size
    
*   Skip() and Take() in EF Core
    

The book list is ordered before pagination to ensure consistent results.

✔ Validation
------------

*   Server-side validation using data annotations
    
*   Client-side validation using jQuery unobtrusive validation
    
*   Validation messages are visible in UI
    
*   Validation summary is shown for model errors
    

🎨 UI and Usability
-------------------

*   Bootstrap based layout
    
*   Responsive tables and forms
    
*   Styled navigation and buttons
    
*   Delete confirmation workflow
    
*   Toast notifications for success and error actions
    

🤖 AI Summary Feature (Gemini)
------------------------------

The AI summary is implemented as:

*   A backend service (AiSummaryService)
    
*   A backend controller endpoint
    
*   A client-side button that fetches the summary
    

The AI call is never executed during page load.

This ensures:

*   cost control
    
*   performance optimization
    
*   proper backend responsibility separation
    

▶ How to Run the Application
----------------------------

### Prerequisites

*   Visual Studio 2022
    
*   .NET 9 SDK installed
    
*   Internet connection (only required for AI summary feature)
    

### Step 1 – Clone the repository

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`git clone` 

or download the ZIP and extract it.

### Step 2 – Open the solution

Open the solution file in Visual Studio:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   BookstoreApp.sln   `

### Step 3 – Restore packages

Visual Studio restores NuGet packages automatically.

If required:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   dotnet restore   `

### Step 4 – Create database

Open **Package Manager Console** in Visual Studio and run:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   Update-Database   `

This will create the SQLite database automatically.

### Step 5 – Run the application

Click:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   Start (F5)   `

The application will open in your browser.

🔐 AI Configuration (Gemini API)
--------------------------------

The AI feature uses Gemini API.

In appsettings.json:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   "Gemini": {    "ApiKey": "YOUR_GEMINI_API_KEY"  }   `

### Important

Do NOT hardcode your API key inside code.

🚫 Important – Do NOT push API keys to GitHub
---------------------------------------------

Your API key must never be committed to source control.

🔒 Recommended Safe Setup for API Keys
--------------------------------------

### Step 1 – Use appsettings.Development.json

Create or use:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   appsettings.Development.json   `

Add:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   {    "Gemini": {      "ApiKey": "YOUR_REAL_KEY"    }  }   `

### Step 2 – Keep appsettings.json clean

In appsettings.json keep only:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   "Gemini": {    "ApiKey": ""  }   `

### Step 3 – Add appsettings.Development.json to .gitignore

Open or create .gitignore and add:

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   appsettings.Development.json   `

This prevents leaking your key.

📁 Project Structure (Simplified)

Plain textANTLR4BashCC#CSSCoffeeScriptCMakeDartDjangoDockerEJSErlangGitGoGraphQLGroovyHTMLJavaJavaScriptJSONJSXKotlinLaTeXLessLuaMakefileMarkdownMATLABMarkupObjective-CPerlPHPPowerShell.propertiesProtocol BuffersPythonRRubySass (Sass)Sass (Scss)SchemeSQLShellSwiftSVGTSXTypeScriptWebAssemblyYAMLXML`   BookstoreApp   ├── Controllers   │     └── BooksController.cs   ├── Data   │     └── BookstoreContext.cs   ├── Models   │     └── Book.cs   ├── Services   │     └── AiSummaryService.cs   ├── Views   │     └── Books   │           ├── Index.cshtml   │           ├── Details.cshtml   │           ├── Create.cshtml   │           ├── Edit.cshtml   │           └── Delete.cshtml   ├── wwwroot   ├── Program.cs   └── appsettings.json   `

📈 Evaluation Highlights
------------------------

This project demonstrates:

*   Proper use of ASP.NET Core MVC
    
*   Entity Framework Core integration
    
*   SQLite database usage
    
*   Backend validation
    
*   Clean controller design
    
*   Dependency injection
    
*   Separation of concerns
    
*   Async programming practices
    
*   Real-world backend AI integration
    

📝 Notes
--------

The application is implemented using:

**ASP.NET Core MVC with Razor Views and Entity Framework Core.**

👤 Author
---------

Sarvesh HadoleSoftware Engineer