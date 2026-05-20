# [Phase 055] [antigravity] 시너지 UX 및 시각 피드백 폴리싱

---

# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage. (Antigravity)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Synergy & Overdrive UX Feedback (05502)
- Background: Phase 050에서 시너지 및 궁극 진화 로직이 구현되었으나, 플레이어가 이를 인지할 수 있는 UI/VFX 피드백이 전무한 상태입니다.
- Related Systems: Upgrade UI (Phase 040), MutationSynergyManager, EventBroker
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. **업그레이드 UI 연동**: 업그레이드 선택 화면(Upgrade UI)에서 카드에 부여된 모디파이어가 특정 시너지 조합에 해당할 경우, 플레이어에게 힌트(예: "관통 시너지 1/2")를 보여주는 UI 요소를 USS/UXML에 추가하세요.
3. **시너지 달성 피드백**: 시너지 완성 및 궁극 무기 진화(`Overdrive`) 이벤트 발생 시, 화면에 텍스트 알림(Floating Text 또는 HUD Pop-up)을 띄우는 시스템을 구현하세요.
4. **VFX 훅(Hook) 연결**: 진화 시 화면 흔들림(Camera Shake)이나 특정 파티클 이벤트를 발생시킬 수 있도록 빈 메서드(VFX Hook) 또는 EventBroker 리스너를 작성하세요. (실제 무거운 에셋 작업은 생략하고 로직만 연결)
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
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
- Scope: `Assets/UI/UpgradeScreen.uxml`, `Assets/UI/UpgradeScreen.uss`, `Assets/Scripts/UI/UpgradeScreenController.cs`
- Scope: `Assets/Scripts/UI/SynergyFeedbackUI.cs` (New)
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
