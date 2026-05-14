# [Phase 05] [antigravity] [p04] 업그레이드 선택 UI

---

# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage. (Antigravity)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Upgrade Selection UI (05003b)
- Background: Phase 5 — 레벨업 시 3~4개 업그레이드 선택지를 표시하는 UI 패널 구현
- Related Systems: LevelingManager (05003), EventBroker (`OnLevelUp`), UI Toolkit
- GDD Reference: `02_gameplay_mechanics.md` §성장 및 진화 시스템
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `UpgradeSelectPanel.uxml`: 3~4개 업그레이드 카드를 수평 배치. 각 카드에 아이콘, 이름, 설명, 레어리티 테두리 포함.
3. Create `UpgradeSelectPanel.uss`: Neon Cyber Minimalism 테마. 카드 hover/선택 시 glow 효과. 레어리티별 색상 (Common: Cyan, Rare: Purple, Epic: Gold).
4. Create `UpgradeSelectController.cs`: ① `OnLevelUp` 이벤트 구독 → 패널 표시 ② 카드 클릭 → 선택 확정 → `Time.timeScale = 1` 복원 ③ 패널 등장/퇴장 트랜지션 (scale + opacity, 0.3s).
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [Required] UI Toolkit (UXML/USS) 사용. Canvas/UGUI 금지.
- [Required] USS에서 인라인 스타일 금지. 전용 `.uss` 파일 사용.
- [Required] UXML 요소 ID는 kebab-case (`upgrade-card-0`, `upgrade-panel`).
- [Prohibited] 기존 LevelingManager.cs 수정 금지 (EventBroker 이벤트만 구독).
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/UI/UpgradeSelectPanel.uxml`, `Assets/UI/UpgradeSelectPanel.uss`, `Assets/Scripts/UI/UpgradeSelectController.cs`
- Reference (Read-only): `Assets/Scripts/Core/EventBroker.cs`
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- 카드 레이아웃: FlexBox 수평 배치 vs Grid — 모바일/PC 반응형 대응
- 트랜지션: USS transition-property 사용 vs C#에서 Tween 직접 구현
- 레어리티 시각 차별화: 테두리 색상 + glow 강도 차등
</thinking>
<implementation>
- Create UXML/USS files and UpgradeSelectController.
</implementation>
<verification>
- [ ] 레벨업 시 패널이 정확히 3~4개 카드와 함께 표시
- [ ] 카드 클릭 시 선택 확정 후 패널 닫힘 + Time.timeScale 복원
- [ ] USS 트랜지션 정상 동작 (등장 0.3s)
- [ ] kebab-case ID 네이밍 준수
</verification>
</output_format>
