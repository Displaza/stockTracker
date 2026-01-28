# FinFrontend

Angular 19 frontend application for the Fin financial data platform. This application provides a user interface for searching financial symbols and viewing financial data retrieved from a .NET backend API.

## Overview

FinFrontend is a modern, standalone Angular application built with Angular 19.2.15. It communicates with a .NET backend API to fetch and display financial information, including stock symbols and market data.

## Architecture

The application follows Angular best practices with a modular structure:

```
src/app/
├── core/              # Core services and guards (singleton services)
├── features/          # Feature modules with routable components
│   └── content/       # Symbol search and display components
├── shared/            # Shared models, utilities, and reusable components
│   └── models/        # Data models (StockSymbol, etc.)
└── app/               # Root component and routing configuration
```

### Key Architectural Patterns

- **Standalone Components**: Uses Angular's standalone API for modular component design
- **Reactive Forms**: Form handling with `FormControl` for symbol search
- **Signal-based State**: Uses Angular signals for reactive state management
- **HttpClient**: Communicates with .NET backend via HTTP requests
- **Modular Routing**: Feature-based routing through `app.routes.ts`

## Prerequisites

- **Node.js**: 18+ (includes npm)
- **Angular CLI**: 19.2.15+
- **Backend**: .NET backend running on `http://localhost:5000` (configurable via proxy)

## Installation

1. Navigate to the frontend directory:
```bash
cd frontend
```

2. Install dependencies:
```bash
npm install
```

## Development

### Start Development Server

```bash
ng serve
```

or from the monorepo root:

```bash
npm run start:frontend
```

The application will be available at `http://localhost:4200/` and automatically reload on file changes.

### Development Proxy

API requests are proxied to the backend via `proxy.config.json`. By default, requests to `/api/*` are forwarded to `http://localhost:5000`.

## Building

Create a production build:

```bash
ng build
```

Optimized artifacts are output to the `dist/` directory.

Build for production from monorepo root:

```bash
npm run build:frontend
```

## Testing

### Unit Tests

Run unit tests with Karma:

```bash
ng test
```

### End-to-End Tests

Scaffold e2e tests using your preferred testing framework:

```bash
ng e2e
```

## Code Generation

Generate new components, services, or other Angular constructs:

```bash
ng generate component features/my-feature/my-component
ng generate service core/my-service
ng generate interface shared/models/my-model
```

For a complete list of available schematics:

```bash
ng generate --help
```

## Key Features

### Symbol Search
The **symbols component** (`features/content/symbols.component.ts`) provides:
- Real-time symbol search input
- GET request to `/api/Home/searchSymbol`
- Displays matching stock symbols with descriptions and type information
- Reactive forms with `FormControl`
- Signal-based response display

### Market news
The **market news component** (`features/content/marketNews.component.ts`) provides:
- Real time market news updates
- Get request to `/api/Home/getNews`
- Displays cached market news updates

## Project Structure

```
src/
├── index.html                 # Entry HTML
├── main.ts                    # Bootstrap file
├── main.server.ts             # Server-side rendering bootstrap
├── server.ts                  # SSR server
├── styles.css                 # Global styles
└── app/
    ├── app.component.*        # Root component
    ├── app.routes.ts          # Routing configuration
    ├── app.routes.server.ts   # SSR routes
    ├── app.config.ts          # App configuration
    ├── app.config.server.ts   # SSR configuration
    ├── core/                  # Core services
    ├── features/              # Feature modules
    │   └── content/           # Symbol search feature
    └── shared/                # Shared models and utilities
        └── models/
            └── symbol.ts      # StockSymbol model
public/                        # Static assets
```

## Configuration Files

- **angular.json**: Angular project configuration
- **tsconfig.json**: TypeScript compiler options
- **tsconfig.app.json**: App-specific TypeScript configuration
- **tsconfig.spec.json**: Test-specific TypeScript configuration
- **proxy.config.json**: Development API proxy configuration

## Environment Setup

### Backend Connectivity

Ensure the .NET backend is running before starting the frontend:

```bash
# From backend directory
dotnet run
```

The default backend URL is `http://localhost:5000`. Modify `proxy.config.json` if your backend runs on a different port.

## Common Tasks

### Format Code
```bash
ng lint
```

### Update Angular
```bash
ng update @angular/cli @angular/core
```

### Serve with Production Configuration
```bash
ng serve -c production
```

## Troubleshooting

### CORS Issues
If you encounter CORS errors, verify:
1. Backend CORS policy allows `http://localhost:4200`
2. Backend is running and accessible
3. API endpoint paths match backend routes

### Module Resolution Issues
Ensure all imports use correct relative paths and that `tsconfig.json` is properly configured with path mappings.

### Null Type Errors
When passing form control values to API parameters, use the nullish coalescing operator:
```typescript
symbol: this.searchControl.value ?? ''
```

## Resources

- [Angular Documentation](https://angular.dev)
- [Angular CLI Documentation](https://angular.dev/tools/cli)
- [Angular Best Practices](https://angular.dev/guide/styleguide)
- [TypeScript Documentation](https://www.typescriptlang.org/docs)

## License

Part of the Fin monorepo project.
