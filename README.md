# RaceDay API

## 1. Project Overview

RaceDay API is an ASP.NET Core Web API developed to support a running, walking, and cycling event management system. It provides API endpoints for managing event information, event categories, enrolments, results, user authentication, and participant profiles.

The project is designed to provide a backend foundation for a future RaceDay application.

## 2. Technologies Used

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- Microsoft SQL Server
- Swagger / OpenAPI
- xUnit
- Git and GitHub
- GitHub Actions

## 3. Main Features

- REST API architecture
- Event management endpoints
- Event category management
- Participant enrolment management
- Results management
- Authentication-related functionality
- Participant profile management
- Database access using Entity Framework Core
- API documentation through Swagger
- Automated build and test workflow using GitHub Actions

## 4. Project Structure

The solution contains the main API project and an automated testing project.

```text
RaceDayAPI/
├── Controllers/
├── Data/
├── DTOs/
├── Filters/
├── Migrations/
├── Models/
├── Services/
├── Properties/
├── Program.cs
├── appsettings.json
├── RaceDayAPI.csproj
├── RaceDayAPI.Tests/
│   ├── RaceDayAPI.Tests.csproj
│   └── UnitTest1.cs
└── .github/
    └── workflows/
        └── dotnet.yml
```

*Note: The exact folder layout may differ depending on how the solution is organised locally.*

## 5. Requirements

To run the project locally, you need:

- Visual Studio with ASP.NET and web development tools
- The .NET 10 SDK
- Microsoft SQL Server
- A suitable SQL Server connection string

## 6. Configuration

The API uses a SQL Server connection string named `RaceDayConnection`.

Configure the connection string in your local configuration file. For example:

```json
{
  "ConnectionStrings": {
    "RaceDayConnection": "Server=YOUR_SERVER;Database=RaceDayDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER` and the database name with the values for your environment.

Do not commit passwords, private credentials, or other secrets to a public repository.

## 7. Running the API

1. Clone the repository:

   ```bash
   git clone https://github.com/Kamogelo47/RaceDayAPI1.git
   ```

2. Open the solution in Visual Studio.

3. Restore the NuGet packages.

4. Configure the SQL Server connection string.

5. Build the solution.

6. Run the `RaceDayAPI` project.

7. Open the Swagger URL displayed by the application to explore the available API endpoints.

## 8. Running the Automated Tests

The solution includes a test project named `RaceDayAPI.Tests`.

To run the tests from the terminal, navigate to the folder containing the solution file and execute:

```bash
dotnet restore
dotnet build
dotnet test
```

The tests use xUnit and ASP.NET Core's testing infrastructure to verify API behaviour.

## 9. Continuous Integration

The repository includes a GitHub Actions workflow located at:

`.github/workflows/dotnet.yml`

The workflow is configured to restore dependencies, build the solution, and run automated tests when changes are pushed to the `master` branch or a pull request targets that branch.

Workflow results can be viewed under the **Actions** tab of the GitHub repository.

## 10. Repository

GitHub repository:

https://github.com/Kamogelo47/RaceDayAPI1

## 11. Future Improvements

Potential future enhancements include:

- Improved authentication and authorisation
- Additional validation and error handling
- More automated integration tests
- Improved API documentation
- Expanded event and participant management functionality
- Deployment to a cloud hosting platform

## 12. Author

Developed as part of the PROG6212 RaceDay API project.
