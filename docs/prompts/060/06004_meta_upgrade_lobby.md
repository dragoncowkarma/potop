# [Phase 07] [jules+antigravity] 영구 강화 & 로비 UI 기초

---

# 🎯 System Role
**Jules (로직)**: Senior Software Engineer — 영구 강화 로직 및 데이터 구조.
**Antigravity (UI)**: Senior Unity Engineer — 로비 화면 UI Toolkit 구현.

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Meta Upgrade & Lobby UI (07004)
- Background: Phase 7 — Gem 경제 + 6개 영구 강화 항목 + 기본 로비 화면
- Related Systems: EventBroker, ScriptableObject, UI Toolkit
- GDD Reference: `03_data_and_balance.md` §영구 강화 (MetaUpgrade)
</context>

# 🛠️ Task

## Part A: 로직 (Jules)
<task>
1. Create `MetaUpgradeManager.cs`: 6개 영구 강화 항목 관리 (강화 외장/사격 드릴/전자 실드/에너지 코어/나노 재생/정밀 스캐너).
2. Create `MetaUpgradeData.asset` ×6 (SO): 각 강화별 레벨 상한, 비용 테이블, 효과 수치.
3. Create `GemWallet.cs`: Gem 획득/소모/잔고 관리. 게임 종료 결산 시 획득 Gem 계산.
4. 세이브 연동: 강화 레벨은 `PlayerPrefs` 또는 JSON 파일로 임시 저장 (Phase 10에서 본격 세이브 시스템 구축).
</task>

## Part B: UI (Antigravity)
<task>
1. Create `LobbyScreen.uxml` / `.uss`: 터렛 선택 영역 + 강화 상점 영역 + Gem 잔고 표시.
2. Create `MetaUpgradeCard.uxml`: 강화 항목 카드 (아이콘, 레벨, 비용, 구매 버튼).
3. Create `LobbyController.cs`: 카드 클릭 → GemWallet 소모 → 강화 레벨 +1 → UI 즉시 반영.
4. 디자인 테마: Neon Cyber Minimalism. 카드에 레벨별 glow 강도 차등.
</task>

# ⚠️ Constraints
<constraints>
- [Required] UI Toolkit (UXML/USS) 사용. Canvas/UGUI 금지.
- [Required] 모든 강화 수치는 SO 필드로 관리.
- [Prohibited] 게임 내(인게임) 로직과 로비 로직 직접 참조 금지. EventBroker 경유만 허용.
</constraints>

# 💻 Input
<input_data>
- Jules Scope: `Assets/Scripts/Gameplay/Meta/MetaUpgradeManager.cs`, `GemWallet.cs`, `Assets/Data/Meta/MetaUpgradeData.asset` ×6
- Antigravity Scope: `Assets/UI/Lobby/LobbyScreen.uxml`, `.uss`, `MetaUpgradeCard.uxml`, `Assets/Scripts/UI/LobbyController.cs`
</input_data>
