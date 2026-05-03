# [Milestone 050] [jules] [p02] localization_system
- parallel: 
    - [docs/prompts/050/0500201_sound_manager.md](file:///Users/macbook/Desktop/potop/docs/prompts/050/0500201_sound_manager.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Localization System (05003)
- Background: Implementing a data-driven localization system to support multiple languages.
- Related Systems: Resource Management, UI Toolkit Data Binding
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `LocalizationManager.cs`: A system that loads JSON-based localization tables from `Assets/Resources/Localization/`.
3. Design the API: `GetLocalizedString(string key)` should return the translated text for the current system language.
4. Support Language Switching: Implement a method to change the active language at runtime and publish a `LanguageChangedEvent`.
5. Create sample localization files: `en.json` and `ko.json` with basic UI strings (e.g., "Start", "Options", "Exit").
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Localization data must be stored in JSON format for easy external editing.
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Core/Localization/LocalizationManager.cs`, `Assets/Resources/Localization/en.json`, `Assets/Resources/Localization/ko.json`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the memory impact of loading large localization tables into a Dictionary.
- Plan the fallback logic for missing keys (e.g., return the key itself or a default English string).
- **Verify Scope Restriction and potential conflicts with parallel task p01.**
</thinking>
<implementation>
- Create `LocalizationManager.cs` and the sample JSON files.
</implementation>
<verification>
- [ ] Confirm `LocalizationManager` correctly loads JSON data.
- [ ] Verify `GetLocalizedString` returns the correct translation for the active language.
- [ ] Ensure `LanguageChangedEvent` is published when the language is switched.
- [ ] EOF empty line and JSON formatting verified.
- [ ] **Scope Restriction (Localization files only) strictly verified.**
</verification>
</output_format>
