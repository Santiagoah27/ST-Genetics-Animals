# ST Genetics Web API

This project is a Web API that allows ST Genetics' customers to create, edit, delete, filter, and buy bulls or cows from our farms. The project was developed using .NET 6, and utilizes the Micro ORM Dapper for communication with the SQL Server database.

## Table of Contents

- [Project Setup](#project-setup)
- [Usage](#usage)
- [Development](#development)

## Project Setup

To set up this project on your local environment, follow these steps:

1. Clone this repository on your local machine.
2. Make sure you have [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) and [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) installed.
3. Open the `appSettings.json` file and update the `DefaultConnection` value in `ConnectionStrings` with your SQL Server details. For example: `"DefaultConnection": "Server=yourServer;Database=STGenetics;User Id=yourUser;Password=yourPassword;"`.

## Usage

To run the application, follow these steps:

1. Open a terminal and navigate to the project directory.
2. Run `dotnet run`.
3. The application should start and be available at `https://localhost:5001/`.
4. You can use tools such as Postman or Swagger to interact with the API.

## Development

This project follows best programming practices, using async/await for all operations and ensuring the code is clean and without warnings when compiling.

### Database

The SQL scripts for creating the database and populating it with test data can be found in the `DB` folder of the repository.

### Authentication

All endpoints require authentication via JSON Web Tokens (JWT). You can obtain a token using the `/api/Animals/token` endpoint.

### Endpoints

- `POST /api/Animals`: Creates a new animal.
- `PUT /api/Animals/{id}`: Updates the animal with the provided ID.
- `DELETE /api/Animals/{id}`: Deletes the animal with the provided ID.
- `GET /api/Animals/filter`: Filters animals by ID, name, sex, or status.
- `POST /api/Animals/order`: Creates a new purchase order.

Please refer to the API documentation for more details on each endpoint.
