# RaceDayAPI — PROG6212 Part 2

## 1. Project Overview

RaceDayAPI is an ASP.NET Core Web API developed for the RaceDay event management system. It provides API endpoints for managing sporting events, event categories, participant enrolments, results, event course details, user profiles, and authentication.

The system is designed to support running, walking, and cycling events. It distinguishes between Participants and Organisers through role-based access controls.

## 2. Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- Swagger / OpenAPI
- Session-based authentication
- Password hashing
- Git and GitHub

## 3. Main Features

### Authentication
- User registration
- Login and logout
- Password hashing and verification
- Session-based authentication
- Participant and Organiser role checks
- Duplicate email detection

### Event Management
- Retrieve events
- Create, update, and delete events
- Manage event categories
- Manage event course details and routes

### Participant Management
- View and update user profiles
- Manage participant enrolments
- Retrieve and manage event results

### API Documentation
Swagger provides an interactive interface for viewing and testing the available API endpoints.

## 4. Project Structure

- `Controllers/` — API endpoints and request handling
- `Data/` — Database context and data access
- `DTOs/` — Data transfer objects
- `Filters/` — Session authentication and authorisation
- `Migrations/` — Entity Framework Core database migrations, if configured
- `Models/` — Application data models
- `Services/` — Supporting services, including password handling
- `Properties/` — Project launch settings
- `Program.cs` — Application configuration and middleware
- `RaceDayAPI.csproj` — Project dependencies and build configuration

## 5. Prerequisites

Before running the application, ensure that you have:

- Visual Studio with ASP.NET and web development support
- A compatible .NET SDK matching the project file
- Microsoft SQL Server
- SQL Server Management Studio (SSMS), or another SQL Server management tool

## 6. Configuration

Configure the SQL Server connection string named `RaceDayConnection` in your local application settings.

Example configuration:

    {
      "ConnectionStrings": {
        "RaceDayConnection": "Server=YOUR_SERVER;Database=RaceDayPart2DB;Trusted_Connection=True;TrustServerCertificate=True;"
      }
    }

Replace `YOUR_SERVER` and the database settings with your own SQL Server configuration.

Do not commit database passwords, API keys, or other secrets to GitHub.

## 7. How to Run the Project

1. Clone or download this repository.
2. Open `RaceDayAPI.sln` in Visual Studio if the solution file is included, or open `RaceDayAPI.csproj`.
3. Configure the `RaceDayConnection` connection string.
4. Ensure that the database exists and that the required schema is available. Apply the project's migrations if appropriate.
5. Restore NuGet packages.
6. Build the solution.
7. Run the application using HTTPS.
8. Open the Swagger interface at the HTTPS application address followed by `/swagger` when running in the Development environment.

## 8. API Endpoints

The following routes are provided by the corresponding controllers:

| Controller | Base route |
|---|---|
| Authentication | `/api/Auth` |
| Events | `/api/Events` |
| Event categories | `/api/EventCategories` |
| Enrolments | `/api/Enrolments` |
| Results | `/api/Results` |
| Event course details | `/api/EventCourseDetails` |
| Profiles | `/api/Profiles` |

Use Swagger to inspect the individual HTTP methods, route parameters, request bodies, and responses.

## 9. Authentication and Authorisation

Authentication uses ASP.NET Core session middleware. After a successful login, the application stores the user's ID and role in the session.

Public registration creates Participant accounts. Organiser-only operations require an authenticated session with the Organiser role.

The application should be accessed over HTTPS so that secure session cookies can be used.

## 10. ASP.NET Core Features Demonstrated

### Dependency Injection
The application registers services through the ASP.NET Core dependency injection container. Controllers receive their required dependencies through constructor injection.

### Middleware and Session Management
The application uses middleware for HTTPS redirection, routing, session management, and authorisation. Sessions are used to maintain authentication state between API requests.

### Entity Framework Core
Entity Framework Core provides database access through `RaceDayDbContext` and supports asynchronous database operations.

### Swagger / OpenAPI
Swagger documents the API endpoints and provides an interactive interface for testing requests and responses.

## 11. Testing

API endpoints should be tested through Swagger or an appropriate API testing tool.

Testing should cover:

- Successful and unsuccessful registration
- Login with valid and invalid credentials
- Logout and access to protected endpoints
- Role-based access restrictions
- Event and category operations
- Participant enrolments and results
- Profile access restrictions
- Database persistence and validation

Record the actual test results and include evidence required by the assignment brief. Do not mark tests as passed until they have been executed.

## 12. Continuous Integration

If GitHub Actions is configured for this repository, its workflow can be used to build the application and run automated tests.

Add the workflow status badge and describe the verified workflow here once the CI pipeline has been configured and run successfully.

## 13. Demonstration Video

**YouTube demonstration:** Add the link to the unlisted demonstration video here once it has been recorded and uploaded.

The demonstration should show the running application, relevant API endpoints, authentication, and the required testing evidence.

## 14. Repository

**GitHub repository:** https://github.com/YOUR-GITHUB-USERNAME/RaceDayAPI1

Replace `YOUR-GITHUB-USERNAME` with your actual GitHub username.

## 15. Conclusion

RaceDayAPI demonstrates the development of an API-based event management system using ASP.NET Core, C#, Entity Framework Core, and SQL Server. It provides a foundation for managing sporting events, participants, enrolments, results, and authentication through structured API endpoints.
