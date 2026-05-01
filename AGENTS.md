# 🤖 Global Agent Operations (AGENTS.md)

[CRITICAL] This document defines the base standards for all sub-projects in POTOP. Platform-specific rules (Unity Client, Django/NestJS Server) are located in their respective subdirectories.

## Universal Agent Protocols
1. **Mimicry**: Flawlessly adapt to the established local coding style, architecture, and naming conventions of the specific sub-project.
2. **Ambiguity**: If requirements are unclear or contradictory, **Stop** and call `ask_question`.
3. **Planning**: For complex tasks, architectural changes, or significant deviations, **Stop** and propose an `implementation_plan.md`.
4. **Refactoring**: Do not perform large-scale refactoring unless explicitly instructed. Extract magic numbers/strings into constants or config variables.
5. **Documentation**:
   - Write documentation that explains the "Why" (intent) rather than just the "What" (action).
   - Maintain and update `SUMMARY.xml` whenever the project structure or core modules change.
6. **File Integrity**: Ensure exactly `1 empty line` at the end of every file (EOF) to maintain git cleanliness.

## Definition of Done
1. The code satisfies 100% of the `USER_REQUEST`.
2. All platform-specific coding standards (naming, braces, etc.) are met.
3. No build errors or critical warnings in the respective environment.
4. A `walkthrough.md` is created with evidence (screenshots/logs) of successful completion.
5. All public APIs and core modules are documented according to project standards.

[NEXT STEP] Proceed to `potop_client/AGENTS.md` or `potop_server/AGENTS.md` for platform-specific constraints.
