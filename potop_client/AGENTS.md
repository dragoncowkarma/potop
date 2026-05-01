# 🤖 Agent Operations (AGENTS.md)

[CRITICAL] You must read `SUMMARY.xml` via `view_file` before analyzing any code.

## When Initializing a Task
1. Execute `view_file` on `SUMMARY.xml` to internalize `Architecture` and `CoreScripts`.
2. Locate existing patterns in `Assets/Scripts/` to ensure `Strict Consistency`.
3. If `SUMMARY.xml` is missing, stop and notify the user.

## When Writing C# Code
1. Use `PascalCase` for `Classes`, `Interfaces`, `Methods`, and `Properties`.
2. Use `camelCase` for `localVariables` and `parameters`.
3. Use `_camelCase` for `private` or `protected` fields.
4. Use `UPPER_SNAKE_CASE` for `CONSTANTS` and `STATIC_READONLY`.
5. Always declare `access modifiers` explicitly (e.g., `private void Awake()`).
6. Use `[SerializeField] private Type _fieldName;` for Inspector exposure.
7. Write `/// XML documentation` for all `public APIs` and `Classes`.
8. Use `K&R` style braces (opening brace on the same line).
9. Ensure exactly `1 empty line` at the end of every file (EOF).

## When Working with Unity 6
1. **Never** use `GameObject.Find()`, `GameObject.FindWithTag()`, or `FindObjectOfType<T>()`.
2. Rely on `Singletons` (e.g., `GameManager.Instance`) or `Inspector references`.
3. **Never** call `GetComponent<T>()`, perform `string` manipulations, or `new` allocations inside `Update()`.
4. Use `async/await` (`UniTask`/`Awaitable`) for heavy operations; use `Coroutines` for visual frame-delays.
5. Strictly avoid `uGUI`; use `Unity UI Toolkit` for all new UI development.

## When Organizing Files
1. `Assets/Scripts/Core/`: Place `Singletons`, `Managers`, and `Common Utilities`.
2. `Assets/Scripts/Gameplay/`: Place `PlayerControl`, `Combat`, and `InGameLogic`.
3. `Assets/Scripts/UI/`: Place `UIControllers`, `Views`, and `UIDocument` bindings.
4. `Assets/Scripts/Data/`: Place `ScriptableObjects`, `DataModels`, and `Structs`.

## When Modifying Existing Code
1. Flawlessly mimic the established `coding style` and `architecture`.
2. Do not perform `large-scale refactoring` unless explicitly instructed.
3. Extract `Magic Numbers` and `Magic Strings` into `constants` or `[SerializeField]` variables.
4. Do not introduce new `external packages` or `NuGet` dependencies.
5. Provide `concise diffs` in chat; provide `complete files` when creating new ones.

## When Verifying Changes
1. Run `mcp_unityMCP_run_tests` to check for logic regressions.
2. Monitor `mcp_unityMCP_read_console` for `Errors` or `Warnings`.
3. Verify that `SUMMARY.xml` is updated if the `Project Structure` changed.

## When Blocked or Uncertain
1. If requirements are ambiguous: **Stop** and call `ask_question`.
2. If a major architectural change is needed: **Stop** and propose an `implementation_plan.md`.
3. If blocked by a bug in `Unity` or `MCP`: **Stop** and report the error logs.

## Definition of Done
1. Code compiles without errors in the `Unity Editor`.
2. `SUMMARY.xml` accurately reflects the current `Architecture` and `CoreScripts`.
3. A `walkthrough.md` is created with `screenshots` or `videos` of the results.
4. All `public APIs` have complete `XML documentation`.
5. The task satisfies 100% of the `USER_REQUEST`.
