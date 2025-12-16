# Nutritional Recipe Book

Full-Stack nutritional recipe platform: recipes, nutrition tables, shopping lists, comments, favorites, ratings, PDF exports and AI-assisted features.

## Key features
- Recipe CRUD, favorites, comments and rating system  
- Nutrition tables per recipe and ingredient-level data  
- Shopping list generation from selected recipes  
- PDF generation via `QuestPDF` (community license)  
- User authentication (ASP\.NET Identity) and JWT token auth  
- Structured logging with `Serilog`  
- EF Core migrations auto-applied at startup  
- Swagger for API exploration (enabled in Development)  
- Gemini AI integration for recipe suggestions, nutrition analysis and conversational assistant  
- Frontend stack: React\.js with TypeScript, Redux, and `antd` UI library

## Tech stack
- Backend: C#, .NET (6/7/8 compatible), ASP\.NET Core Web API  
- ORM: Entity Framework Core  
- Logging: Serilog  
- PDF: QuestPDF  
- Frontend: React\.js + TypeScript, Redux, `antd`  
- AI: Google Gemini (integrated via backend endpoints; requires API key)

## Repository layout (important)
- `NutritionalRecipeBook.Api` — API project (entry: `Program.cs`)  
- `NutritionalRecipeBook.Domain` — domain models / entities  
- `migrations` / EF Core migration files inside the API project  

## Git metadata
- Current branch: `dev`  
- Upstream: `github` -> `https://github.com/shepelenkomikhail/NutritionBook`  

## Gemini AI integration
- Purpose: contextual recipe suggestions, ingredient substitution, nutrition estimation and conversational help.  
- Implementation: backend exposes endpoints that call Gemini (server-side) to avoid exposing keys in the frontend.  
- Required env vars (examples):  
  - `GEMINI_API_KEY` — API key / service account credential  
  - `GEMINI_MODEL` — model id to use (e.g., `gemini-pro` or configured model)  
- Ensure request/response size and cost controls when using the model in production.

## Frontend
- Tech: React\.js + TypeScript, Redux for state management, `antd` for UI components.  
- Typical structure: `frontend/src` with feature folders (recipes, auth, shopping-list, ai) and Redux slices.  
- CORS: API defines a `Frontend` CORS policy allowing `http://localhost:3000` and production origins — adjust as needed.

## Running locally (Windows / Rider)
1. Ensure .NET SDK installed (recommended: .NET 8) and Node\.js for frontend.  
2. Configure `appsettings.json` or environment variables:
   - `ConnectionStrings:DefaultConnection`  
   - JWT settings (issuer/audience/key)  
   - `GEMINI_API_KEY`, `GEMINI_MODEL`  
   - Serilog settings (optional)  
3. From repo root:
   - `dotnet restore`
   - `dotnet build`
   - `dotnet run --project NutritionalRecipeBook.Api`
   - API Swagger (Development): `https://localhost:{port}/swagger`  
4. Frontend:
   - `cd frontend`
   - `npm install`
   - `npm start` (runs on `http://localhost:3000` by default)

Note: `Program.cs` applies EF Core migrations automatically at startup (`db.Database.Migrate()`).

## Important runtime notes
- Serilog is configured as host logger and bootstrap logger.  
- `QuestPDF.Settings.License = LicenseType.Community` is set at startup.  
- CORS: a permissive middleware currently sets `Access-Control-Allow-*` headers — review or remove for production.  
- Static files served and HTTPS redirection enabled.

## Database & migrations
- Use EF Core tools for manual migrations when needed:
  - `dotnet ef migrations add <Name> -p NutritionalRecipeBook.Api -s NutritionalRecipeBook.Api`
  - `dotnet ef database update -p NutritionalRecipeBook.Api -s NutritionalRecipeBook.Api`

## Security & deployment
- Keep JWT secrets and `GEMINI_API_KEY` in a secure secret store or environment variables.  
- Configure Serilog sinks (file, Seq, Application Insights) for production telemetry.  
- Restrict CORS origins and remove permissive header middleware before production.

## Testing
- Run unit and integration tests with:
  - `dotnet test`

## Contributing
- Fork, create a branch, implement changes and tests, open a merge request to `dev`.