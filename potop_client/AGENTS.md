# 🎮 Client-Specific Agent Operations (AGENTS.md)

[CRITICAL] Read the root `AGENTS.md` for general protocols before applying these Unity-specific rules.

## C# Coding Standards
1. **Naming Conventions**:
   - `PascalCase`: Classes, Interfaces, Methods, Properties.
   - `camelCase`: localVariables, parameters.
   - `_camelCase`: private or protected fields.
   - `UPPER_SNAKE_CASE`: CONSTANTS and STATIC_READONLY.
2. **Structure**:
   - Always declare `access modifiers` explicitly (e.g., `private void Awake()`).
   - Use `[SerializeField] private Type _fieldName;` for Inspector exposure.
   - Use `K&R` style braces (opening brace on the same line).
   - Write `/// XML documentation` for all public APIs and Classes.

## Unity 6 Best Practices
1. **Performance**:
   - **Never** use `GameObject.Find()`, `FindWithTag()`, or `FindObjectOfType<T>()`.
   - **Never** call `GetComponent<T>()`, perform `string` manipulations, or `new` allocations inside `Update()`.
   - Rely on `Singletons` (e.g., `GameManager.Instance`) or `Inspector references`.
2. **UI**: Strictly avoid `uGUI`; use `Unity UI Toolkit` for all new UI development.
3. **Input**: **Exclusively** use the `Unity New Input System`. Legacy `UnityEngine.Input` is strictly prohibited.
4. **Concurrency**: Use `async/await` (`UniTask`/`Awaitable`) for heavy operations; use `Coroutines` for visual frame-delays.

## Organization
- `Assets/Scripts/Core/`: Singletons, Managers, and Common Utilities.
- `Assets/Scripts/Gameplay/`: PlayerControl, Combat, and In-Game Logic.
- `Assets/Scripts/UI/`: UIControllers, Views, and UIDocument bindings.
- `Assets/Scripts/Data/`: ScriptableObjects, DataModels, and Structs.

## Verification
1. Run `mcp_unityMCP_run_tests` to check for logic regressions.
2. Monitor `mcp_unityMCP_read_console` for Errors or Warnings.
3. Code must compile without errors in the Unity Editor.
4. Ensure `SUMMARY.xml` is updated if the client architecture changes.
