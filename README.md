# TodoApp – ASP.NET Backend Developer Test

This is a sample project implemented as part of a technical assessment for the ASP.NET Backend Developer position at Zumra.

## 🛠 Technologies Used

- ASP.NET Core 7.0
- MediatR
- Entity Framework Core (InMemory)
- xUnit for unit testing
- Moq
- FluentAssertions

## 📂 Project Structure

TodoApp/ │ ├── TodoApp.Application/ # Application layer (CQRS handlers, interfaces) ├── TodoApp.Domain/ # Domain models/entities ├── TodoApp.Infrastructure/ # Data access layer (EF Core, repositories) ├── TodoApp.Presentation/ # ASP.NET Core Web API (optional if included) ├── TodoApp.Tests/ # Unit tests ├── TodoApp.sln # Solution file └── README.md


## ✅ Features Implemented

- Add a new Todo item
- Get all Todo items
- Get a Todo item by ID
- Update a Todo item
- Delete a Todo item

All operations follow **CQRS** (Command Query Responsibility Segregation) using **MediatR**.

## 🧪 Testing

Unit tests are written using **xUnit** and **Moq**. To run tests:

```bash
dotnet test

🚀 Getting Started

    Clone the repository:

git clone https://github.com/your-username/TodoApp.git
cd TodoApp

    Run the project or the tests using:

dotnet run
dotnet test

📌 Notes

    The database is configured using EF Core InMemory for simplicity.

    Clean Architecture principles are followed: Domain, Application, Infrastructure, and Presentation layers.

    No actual authentication or user management is implemented – the focus is on Todo CRUD operations and clean structure.

🙋‍♂️ Author

Saman Bakhshi
Email: bakhshi.smn@gmail.com
GitHub: github.com/your-username
