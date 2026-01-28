# FinBackend

A modern .NET 9 backend for financial data aggregation, symbol search, and news retrieval, featuring JWT authentication, async programming, and clean architecture.

## Features

- **Symbol Search:** Search for financial symbols using the Finnhub API.
- **Financial News:** Retrieve and paginate financial news.
- **User Management:** Secure user registration and authentication with hashed passwords.
- **JWT Authentication:** Secure endpoints with industry-standard JWT tokens.
- **OpenAPI/Swagger:** Interactive API documentation.
- **Async/Await:** Scalable, non-blocking operations.
- **CORS:** Configured for frontend integration (e.g., Angular).

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (or update connection string for your DB)
- [Finnhub API Key](https://finnhub.io/)

### Configuration

Edit `appsettings.json`:
