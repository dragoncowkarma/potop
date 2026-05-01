# 🤖 Global Agent Operations (AGENTS.md)

[CRITICAL] This document defines the base standards for all `POTOP` sub-projects. Platform-specific rules are located in `potop_client/AGENTS.md` and `potop_server/AGENTS.md`.

## When Starting a Task
1. **Context Awareness**: Read `SUMMARY.xml` prior to accessing any files to prevent redundant operations.
2. **Refactor Tracking**: Check [`REFACTOR_TRACKING.md`](REFACTOR_TRACKING.md) immediately. Resolve entries within scope (e.g., cleanup `FormerlySerializedAs`) and delete them from the file once done.
3. **Mimicry**: Flawlessly adapt to the established local `coding style`, `architecture`, and `naming conventions` (e.g., `ruff check` standards) of the specific sub-project.
4. **Ambiguity**: If requirements are unclear or contradictory, **Stop** and call `ask_question`.
5. **Planning**: For complex tasks, architectural changes, or significant deviations, **Stop** and propose an `implementation_plan.md`.

## When Modifying Code
1. **Scope Restriction**: Modifications MUST be strictly limited to the `requested scope`. Do not touch `unrelated files`.
2. **No Boilerplate**: Prohibit `unrequested boilerplate` generation. Implement only what is necessary.
3. **Refactoring**: Do not perform large-scale refactoring unless explicitly instructed. Extract `magic numbers` into constants or `config variables`.
4. **Concise Comments**: Use comments to explain "Why" (intent) rather than just the "What" (action). Omit conversational or repetitive `fluff`.
5. **Documentation**: Maintain and update `SUMMARY.xml` whenever the `project structure` or `core modules` change.
6. **File Integrity**: Ensure exactly `1 empty line` at the end of every file (`EOF`) to maintain git cleanliness.

## When Blocked
1. **Technical Debt**: If a task requires bypassing a protocol, log it in [`REFACTOR_TRACKING.md`](REFACTOR_TRACKING.md) and report to the `USER`.
2. **Dependency Issues**: Check local `README.md` or `package.json` before asking about missing dependencies.

## Definition of Done
`Done` status MUST be verified via these CLI commands:
1. All sub-project specific `validation tools` (e.g., linters, compilers) return zero errors for modified files.
2. The `console` or `logs` show zero `Critical Warnings` or `Red Errors`.
3. The code satisfies 100% of the `USER_REQUEST`.
4. A `walkthrough.md` is created with evidence (screenshots/logs) of success.
5. All `public APIs` and `core modules` are documented in `SUMMARY.xml`.

[NEXT STEP] Proceed to `potop_client/AGENTS.md` or `potop_server/AGENTS.md` for platform-specific constraints.
