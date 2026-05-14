# [Phase 08] [jules] 오버클럭 모드 & 게임 플로우 통합

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Overclock Mode & Game Flow (08003, 08004)
- Background: Phase 8 — 보스 처치 후 무한 웨이브, 전체 게임 플로우 루프 연결
- Related Systems: WaveManager, GameManager, EventBroker, LevelingManager, GemWallet
- GDD Reference: `01_overview.md` §게임 플로우, `02_gameplay_mechanics.md` §오버클럭
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.

## 08003: 오버클럭 모드
2. Create `OverclockMode.cs`:
   - `OnBossDefeated` 이벤트 구독 → 오버클럭 진입.
   - WaveManager를 무한 모드로 전환 (반복 스폰, 종료 조건 없음).
   - 30초마다 적 HP/속도/대미지 ×1.5 스케일링.
   - 점수 배율 매분 +0.1x 가산.
   - 스케일링 수치는 `OverclockData.asset` SO로 외부화.

## 08004: 게임 플로우 통합
3. Create `GameFlowController.cs`:
   - 전체 루프: 로비 → 터렛 선택 → 인게임 시작 → 15분 타이머 → 보스 스폰 → 보스 처치 → 오버클럭 → 사망 → 결산 → 로비.
   - 15분 타이머: UI HUD 표시 + EventBroker `OnTimerFinished` 발행.
   - 결산 화면: 처치 수, 최대 웨이브, 획득 Gem, 사용 터렛, 생존 시간 표시.
4. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] GameFlowController는 `GameManager`와 분리. 플로우 제어만 담당.
- [Prohibited] 씬 전환 없이 단일 씬 내에서 상태 전환으로 구현.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Flow/OverclockMode.cs`, `GameFlowController.cs`, `Assets/Data/Flow/OverclockData.asset`
- Antigravity Scope (결산 UI): `Assets/UI/ResultScreen.uxml`, `.uss`, `Assets/Scripts/UI/ResultScreenController.cs`
- Reference (Read-only): `Assets/Scripts/Core/GameManager.cs`, `WaveManager.cs`, `EventBroker.cs`
</input_data>
