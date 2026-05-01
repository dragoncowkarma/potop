# 🎮 Client-Specific Agent Operations (AGENTS.md)

[CRITICAL] Read the root `AGENTS.md` for general protocols before applying these `Unity-specific` rules.

## When Writing C# Code
1. **`C#` Standards**: All code MUST follow these numbered priorities:
    1.1. **Brace Style**: Use `K&R` style (opening brace on the same line).
    1.2. **Modifiers**: Always declare `access modifiers` explicitly (e.g., `private void Awake()`).
    1.3. **Serialization**: Use `[SerializeField] private Type _fieldName;` for Inspector exposure.
    1.4. **Naming Conventions**:
        1.4.1. `PascalCase`: Classes, Interfaces, Methods, Properties.
        1.4.2. `camelCase`: localVariables, parameters.
        1.4.3. `_camelCase`: private/protected fields.
        1.4.4. `UPPER_SNAKE_CASE`: CONSTANTS/STATIC_READONLY.
2. **Lint Enforcement**: Use `mcp_unityMCP_validate_script` (Equivalent to `ruff check`) to check style and `mcp_unityMCP_read_console` to verify errors.

## When Modifying Unity Logic
1. **`Optimization` Protocols**: Follow these performance rules:
    1.1. **Never** use `GameObject.Find()`, `FindWithTag()`, or `FindObjectOfType<T>()`.
    1.2. Prohibit `GetComponent<T>()`, `string` manipulations, or `new` allocations inside `Update()`.
2. **System Usage**:
    2.1. **Input**: Use **only** the `Unity New Input System`. Legacy `UnityEngine.Input` is prohibited.
    2.2. **UI**: Use `Unity UI Toolkit` (UXML/USS) for all new UI. Avoid `uGUI`.
    2.3. **Concurrency**: Use `async/await` (`UniTask`/`Awaitable`) for operations.
3. **Workflow**: Save the Unity scene immediately upon completion of scene modifications via `mcp_unityMCP_manage_scene(action='save')`.

## When Organizing Unity Assets
1. **Asset Priority**: Organize `Assets/Scripts/` via `mcp_unityMCP_manage_asset` in this order:
    1.1. `Core/`: Singletons, Managers, Utilities.
    1.2. `Gameplay/`: PlayerControl, Combat, Logic.
    1.3. `UI/`: UIControllers, Views, `UIDocument` bindings.
    1.4. `Data/`: ScriptableObjects, DataModels, Structs.

## When Blocked
1. **Console Errors**: If `mcp_unityMCP_read_console` shows `Red Errors`, stop and fix them immediately.
2. **Missing Refs**: If a serialized field is unassigned, use `mcp_unityMCP_manage_components` or ask the `USER`.
3. **Linter Failures**: If `mcp_unityMCP_validate_script` fails more than 3 times for the same issue, **Stop** and report the full diagnostic to the `USER`.
4. **Debt Logging**: If `[FormerlySerializedAs]` is used, `jules` MUST log it in root [`REFACTOR_TRACKING.md`](../REFACTOR_TRACKING.md).

## Definition of Done
`Done` status requires passing these checks:
1. `mcp_unityMCP_validate_script` returns zero diagnostics.
2. `mcp_unityMCP_read_console` is clear of `Critical Warnings` and `Red Errors`.
3. `mcp_unityMCP_run_tests` (EditMode/PlayMode) passes for affected modules.
4. Changed scripts are committed and `SUMMARY.xml` is updated.
