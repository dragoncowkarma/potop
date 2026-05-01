# ☁️ Server-Specific Agent Operations (AGENTS.md)

[CRITICAL] Read the root `AGENTS.md` for general protocols. The server implementation is currently undecided between **Django (Python)** and **NestJS (TypeScript)**.

## Naming Standards
- **If Django (Python)**:
  - `snake_case`: Variables, functions, and file names.
  - `PascalCase`: Classes.
- **If NestJS (TypeScript)**:
  - `camelCase`: Variables, functions, and properties.
  - `PascalCase`: Classes, Interfaces, and Types.
  - `kebab-case`: File names (e.g., `user-controller.ts`).

## Backend Principles
1. **API Design**:
   - Use **RESTful** principles or gRPC where applicable.
   - Return clear, structured JSON responses.
   - Use standard HTTP status codes (200 OK, 201 Created, 400 Bad Request, 401 Unauthorized, 500 Server Error).
2. **Security**:
   - **Never Trust the Client**: Validate all incoming data.
   - Secure all endpoints with appropriate authentication (JWT, OAuth2).
   - Use Environment Variables (`.env`) for all secrets and sensitive configs.
3. **Database**:
   - Use migrations for all schema changes.
   - Ensure indexing on frequently queried fields (e.g., `score`, `user_id`).
4. **Performance**:
   - Implement asynchronous operations throughout to handle high concurrency.
   - Use caching (Redis) for frequently accessed data like Leaderboards.

## Organization
- `src/controllers/` or `apps/`: API endpoint definitions.
- `src/services/` or `models/`: Business logic and database interactions.
- `src/dto/` or `serializers/`: Data validation and transformation layers.

## Verification
1. Run server-side unit and integration tests.
2. Verify API endpoints using `curl` or testing tools.
3. Ensure error logs are clean and provide actionable information.
