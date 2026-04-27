🤖 System Instructions for AI Agents (AGENTS.md)

This document defines the strict rules, context, and coding standards for all AI coding agents and assistants (e.g., Jules, Codex, Cursor, Devin, GitHub Copilot) operating within this repository.
[CRITICAL] You must read and internalize this document before analyzing, modifying, or generating any code.

1. 🎯 Core Principles

Readability First: Write clean, self-documenting code that is easily understandable by both humans and AIs.

Keep It Simple (KISS): Avoid over-engineering. Provide the simplest, most maintainable solutions.

Strict Consistency: When modifying existing files, flawlessly mimic the established coding style, patterns, and architecture of the project.

Ask When Unsure: Do not hallucinate or make assumptions regarding ambiguous requirements or major architectural decisions. Prompt the user for clarification.

2. 🛠️ Tech Stack & Environment

Language: C# (Latest version supported by Unity 6)

Framework: Unity 3D (Target: Unity 6.0 LTS)

UI System: Unity UI Toolkit (STRICTLY AVOID uGUI unless explicitly requested).

Input System: Unity New Input System.

Architecture: MVC / MVP patterns, Event-Driven Architecture.

3. 📝 Coding Standards

Adhere to the following C# and Unity coding conventions strictly:

Naming Conventions:

Classes, Interfaces, Methods, Properties: PascalCase (e.g., PlayerController)

Local Variables, Parameters: camelCase (e.g., moveSpeed)

Private / Protected Fields: _camelCase (e.g., _healthPoints)

Constants / Static Readonly: UPPER_SNAKE_CASE (e.g., MAX_INVENTORY_SIZE)

Access Modifiers: Always declare access modifiers explicitly (e.g., use private void Awake() instead of void Awake()).

Serialization: Never use public fields just to expose them to the Inspector. Always use [SerializeField] private Type _fieldName;.

Comments: Document the "Why", not the "How". Use XML documentation (///) for all public APIs and classes.

4. 📁 Directory Structure Guidelines

Place newly generated files in their appropriate semantic locations:

Assets/Scripts/Core/: Singletons, Game/State Managers, Common Utilities.

Assets/Scripts/Gameplay/: Player control, Combat mechanics, In-game logic.

Assets/Scripts/UI/: UI Controllers, Views, UI Toolkit bindings.

Assets/Scripts/Data/: ScriptableObjects, Data models, Structs.

5. ⚡ Unity Specific Optimizations

No Costly Lookups: NEVER use GameObject.Find(), GameObject.FindWithTag(), or FindObjectOfType<T>(). Rely on Inspector references, singletons, or Dependency Injection.

Update() Loop Discipline: NEVER call GetComponent<T>(), perform string manipulations/concatenations, or allocate memory (new objects) inside Update(), FixedUpdate(), or LateUpdate(). Cache references in Awake() or Start().

Asynchronous Operations: Use Unity Coroutines for simple frame-delays or visual effects. Use async/await (e.g., UniTask or Awaitable in Unity 6) for heavy asynchronous operations like asset loading or web requests.

6. 🚫 Negative Prompts (What NOT to do)

DO NOT perform large-scale refactoring of functional code unless explicitly instructed by the user.

DO NOT hardcode "Magic Numbers" or strings. Extract them into constants or [SerializeField] variables.

DO NOT introduce unauthorized external packages, plugins, or NuGet dependencies.

DO NOT output the entire file if only a small change is made. Provide concise diffs or highlight only the modified sections when interacting in chat, but output clean, complete files when creating new ones.

AGENT ACKNOWLEDGEMENT: By processing this repository, you automatically agree to apply all rules defined in this AGENTS.md file to your outputs implicitly, without needing to be reminded in every prompt.
