# [Phase 055] [gemini] 자동화 검증 시스템 (Pity & 시너지)

---

# 🎯 System Role
You are a **Senior QA and Stability Engineer**, verifying project integrity. You identify anti-patterns by analyzing runtime data and logs and strictly audit compliance with project standards. (Gemini CLI)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Automated Validation for Progression Systems (05503)
- Background: Phase 050에서 도입된 천장(Pity) 시스템과 시너지 조합 로직은 수동으로 검증하기 까다롭습니다. 안정적인 플레이 경험을 위해 유닛/통합 테스트가 필요합니다.
- Related Systems: UpgradePool, MutationSynergyManager
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. **Pity 시스템 검증 코드 작성**: NUnit 기반 에디터 스크립트를 작성하여, `UpgradePool`에서 1000번 추출 시 에픽(Epic) 확률이 정상 동작하는지, 10연속 실패 시 확정 획득이 트리거되는지 시뮬레이션하고 검증하는 로직을 작성하세요.
3. **시너지 적용 무결성 테스트**: 특정 모디파이어 2개를 강제 주입(Inject)했을 때 `MutationSynergyManager`가 정확히 보너스 이벤트를 Fire 하는지 검증하는 테스트 코드를 작성하세요.
4. 작성된 코드는 `Assets/Scripts/Tests/EditMode/ProgressionTests.cs` 에 저장하고, 테스트 실행 및 통과 여부를 보고하세요.
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
- Scope: `Assets/Scripts/Tests/EditMode/ProgressionTests.cs` (New), `Assets/Scripts/Tests/EditMode/SynergyTests.cs` (New)
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
