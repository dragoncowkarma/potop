# ☁️ Server-Specific Agent Operations (AGENTS.md)

[CRITICAL] Read the root `AGENTS.md` for general protocols. The server implementation is currently undecided between **Django (Python)** and **NestJS (TypeScript)**.

## Naming Standards
1. **If Django (Python)**:
    1.1. `snake_case`: Variables, functions, and file names.
    1.2. `PascalCase`: Classes.
2. **If NestJS (TypeScript)**:
    2.1. `camelCase`: Variables, functions, and properties.
    2.2. `PascalCase`: Classes, Interfaces, and Types.
    2.3. `kebab-case`: File names (e.g., `user-controller.ts`).

## Backend Principles
1. **API Design**:
    1.1. Use `RESTful` principles or `gRPC` where applicable.
    1.2. Return clear, structured `JSON` responses.
    1.3. Use standard `HTTP` status codes (e.g., `200`, `201`, `400`, `401`, `500`).
2. **Security**:
    2.1. **Validation**: Validate all incoming data. Never trust the `client`.
    2.2. **Auth**: Secure all endpoints with `JWT` or `OAuth2`.
    2.3. **Secrets**: Use Environment Variables (`.env`) for all secrets.
3. **Database**:
    3.1. Use `migrations` for all schema changes.
    3.2. Ensure indexing on frequently queried fields (e.g., `score`, `user_id`).
4. **Performance**:
    4.1. Implement `asynchronous` operations to handle concurrency.
    4.2. Use `Redis` caching for frequently accessed data like Leaderboards.

## Organization
1. API endpoints: `src/controllers/` (NestJS) or `apps/` (Django).
2. Business logic: `src/services/` (NestJS) or `models/` (Django).
3. Data validation: `src/dto/` (NestJS) or `serializers/` (Django).

## When Blocked
1. **Ambiguity**: If framework is unselected, stop and ask the **USER** before `implementation`.
2. **Errors**: If logs show `500` errors, check database connection first.

## Definition of Done
1. API endpoints are verified using `curl` or automated testing tools.
2. Server-side `unit/integration` tests return zero failures.
3. Error logs are clean of critical issues.
4. `SUMMARY.xml` is updated with new modules/endpoints.
