# 📑 Agent Entry Point (docs/AGENTS.md)

[CRITICAL] This document is the primary entry point for AI agents managing the documentation and planning layer of the `POTOP` project. Read the root `AGENTS.md` for general protocols.

## When Starting a Task
1. **Milestone Alignment**: Every operation MUST align with a milestone in `07_development_milestones.md`.
2. **Context Verification**: Read [`SUMMARY.xml`](../SUMMARY.xml) to understand project structure.
3. **Role Check**:
    3.1. Logic: Follow `Jules` directives (Architect).
    3.2. Visual: Follow `Antigravity` directives (Builder).

## When Modifying Documentation
1. **`SUMMARY.xml` Maintenance**: Update root [`SUMMARY.xml`](../SUMMARY.xml) whenever project structure changes.
2. **Bilingual Standard**: Use `English` for code/comments; `Korean/English` for high-level documentation.
3. **File Integrity**: Ensure exactly `1 empty line` at the end of every file (`EOF`).
4. **Linting**: Ensure compliance via `ruff check` or project-specific validation tools.

## Documentation Management Rules
1. **Standard Format**: All documentation MUST be written in `.md` (`Markdown`) format.
2. **Human-Readable Content**:
    2.1. **Granularity**: Content MUST be detailed without omissions (e.g., `GDD` specs).
    2.2. **Cohesion**: Each document MUST strictly contain a single `category/domain`.
3. **Context Optimization**:
    3.1. **Document Split**: Split docs by `domain` if they exceed `150 lines` or become complex.
    3.2. **Discovery**: Register all split documents in [`SUMMARY.xml`](../SUMMARY.xml) immediately.

## Behavioral Directives
1. **Jules (Logic)**:
    1.1. Prioritize `type safety` and explicit `access modifiers`.
    1.2. Follow `SOLID` principles; keep methods focused and small.
2. **Antigravity (Visual/UI)**:
    2.1. Maintain strict `naming conventions` and `hierarchy` cleanliness.
    2.2. Use `USS` for styling; avoid inline styles in `UXML`.
    2.3. Ensure all reusable components are saved as `Prefabs` in `Assets/Prefabs/`.

## When Blocked
1. **Dependency Issues**: Check [`SUMMARY.xml`](../SUMMARY.xml) or local `README.md` before asking.
2. **Ambiguity**: If a milestone is unclear, stop and request clarification via `ask_question`.

## Definition of Done
1. A `walkthrough.md` is created with evidence of success.
2. All public APIs and core modules are documented in [`SUMMARY.xml`](../SUMMARY.xml).
3. [`SUMMARY.xml`](../SUMMARY.xml) structure is verified via `cat SUMMARY.xml`.
