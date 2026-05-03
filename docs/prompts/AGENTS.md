# 🤖 Prompt Engineering Standards (docs/prompts/AGENTS.md)

[CRITICAL] All prompt files (`.md`) in the `docs/prompts/` directory MUST strictly adhere to the **POTOP Standard Prompt Template** defined below. This rule is enforced to maintain consistent code quality and architecture across the project.

## 0. Core Rules (Mandatory)
1. **Parallel Processing**: Parallel processing MUST be executed ONLY by `jules`.
2. **Gemini CLI Restriction**: Gemini CLI cannot be used for Unity-related tasks (Scene modification, Prefab manipulation, etc.) due to MCP server issues.

## 1. Standard Prompt Templates
All prompts must strictly follow the structures below. Select the appropriate `System Role` from Section 2 based on the prompt's purpose.

---

### 1.1. Single-Process Prompt Template
# 🎯 System Role
[Refer to Section 2 for Role Definition]

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: [Module Name]
- Background: [Related previous tasks or REFACTOR_TRACKING entries]
- Related Systems: [Event Broker, PoolManager, etc.]
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. For complex logic or architectural changes, propose an `implementation_plan.md` for approval before modifying code.
3. [Detail the core features to implement]
4. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not leave unrequested boilerplate, temporary variables, or debug logs.
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring without instruction.
- [Recommended] Implement standard exception handling for each sub-project to prevent crashes.
</constraints>

# 💻 Input
<input_data>
[Insert source code or reference data here]
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Situation analysis and edge case handling plan
- Verification of `AGENTS.md` compliance
</thinking>

<implementation>
- [Instructions: Use agent tools or Diff format]
</implementation>

<verification>
- [ ] Context/Refactor Tracking verified
- [ ] EOF empty line and comment cleanup completed
- [ ] Magic Numbers removed
</verification>
</output_format>

---

### 1.2. Jules Parallel Prompt Template
# [Milestone 0xx] [jules] [p0x] task_name
- parallel: [Link to other parallel prompt]

---

# 🎯 System Role
[Refer to Section 2.1 for Jules Role Definition]

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: [Module Name]
- Background: [Related previous tasks or REFACTOR_TRACKING entries]
- Related Systems: [Event Broker, PoolManager, etc.]
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. [Detail the core features to implement]
3. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not leave unrequested boilerplate, temporary variables, or debug logs.
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring without instruction.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
[Insert source code or reference data. MUST NOT overlap with other parallel tasks]
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Situation analysis and edge case handling plan
- **Verification of Scope Restriction and potential conflicts with other parallel tasks**
- Verification of `AGENTS.md` compliance
</thinking>
<implementation>
- [Instructions: Use agent tools or Diff format]
</implementation>
<verification>
- [ ] Context/Refactor Tracking verified
- [ ] EOF empty line and comment cleanup completed
- [ ] Magic Numbers removed
- [ ] **Scope Restriction (No modification outside assigned files) verified**
</verification>
</output_format>

---

## 2. Agent Role Definitions

Apply the following roles based on the agent's characteristics.

### 2.1. Jules (Logic & Implementation)
> **Role**: You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`.
> **Characteristics**: Responsible for complex business logic, architectural design, and large-scale refactoring. Parallel processing is handled EXCLUSIVELY by Jules.

### 2.2. Antigravity (Unity, UI & Polish)
> **Role**: You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage.
> **Characteristics**: Responsible for Unity 6, UI Toolkit (UXML/USS), prefab configuration, particles, and overall polish. Always check `AGENTS.md` before starting.

### 2.3. Gemini CLI (Validation & Audit)
> **Role**: You are a **Senior QA and Stability Engineer**, verifying project integrity. You identify anti-patterns by analyzing runtime data and logs and strictly audit compliance with project standards.
> **Characteristics**: Responsible for runtime log analysis, physics setting verification, and build stability checks. **WARNING: Cannot be used for Unity tasks (Scene/Prefab).**

## 3. Communication Standards

Commands and prompts between agents must follow these standards.

### 3.1. PR Feedback and Revision Requests (Comment Prompt)
Used when there are errors or improvements needed in an AI-generated PR.
- **Required**: Target file paths, error logs or specific symptoms, and instruction to check `AGENTS.md`.

### 3.2. Build Error Fix Requests
Used for urgent fixes due to compiler errors or runtime exceptions.
- **Required**: `Compiler Output` or `StackTrace`, relevant file list, and emphasis on `AGENTS.md` compliance.

### 3.3. Merge Conflict Resolution Requests
Used to resolve conflicts during branch merging.
- **Required**: Conflicted code blocks (<<<<<<<, =======, >>>>>>>), and explanation of the base logic in the `master` branch.

## 4. Management Rules
1. **Consistency**: All new prompt files must be created by copying the templates above.
2. **Updates**: If global rules in `AGENTS.md` change, these templates must be synchronized immediately.

---
*Last Updated: 2026-05-03*
