# [Phase 05] [jules] [p01] 터렛 클래스 구현
- parallel: [p02](05002_jules_p02_exp_gem.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Turret Class Implementation (05001)
- Background: Phase 5 — 4종 포탑 클래스(Guardian/Valkyrie/Juggernaut/Nova)를 `WeaponBase` 상속 구조로 구현
- Related Systems: WeaponBase (Phase 2.5), IFireStrategy, TurretData SO, PoolManager
- GDD Reference: `02_gameplay_mechanics.md` §플레이어 포탑 클래스, `03_data_and_balance.md` §포탑 상세 데이터
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Create `GuardianWeapon.cs`: 직선형 레일건. 공격력 10, 공속 0.5s, 탄속 20m/s. `IFireStrategy` 구현.
3. Create `ValkyrieWeapon.cs`: 고속 기관총. 공격력 4, 공속 0.15s, 탄퍼짐 15도, 탄속 25m/s. 스프레드 로직 필수.
4. Create `JuggernautWeapon.cs`: 철갑탄. 공격력 35, 공속 1.5s, 기본 관통 1회, 넉백 5.0, 탄속 15m/s.
5. Create `NovaWeapon.cs`: 에너지 구체. 공격력 15, 공속 1.0s, 적중 시 0.5m 폭발, 탄속 12m/s.
6. Create 4개 `TurretData.asset` ScriptableObject (각 포탑별 스탯 데이터).
7. **모든 수치는 상수(const) 또는 SO 필드로 관리.** 하드코딩 금지.
8. After completion, resolve the "Combat Integration" entry in `../../REFACTOR_TRACKING.md` (TurretShooter → WeaponBase 리팩토링).
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Namespace: `Potop.Client.Gameplay`.
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or SO fields.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/GuardianWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/ValkyrieWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/JuggernautWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/NovaWeapon.cs`, `Assets/Data/Turrets/` (SO 에셋)
- Reference (Read-only): `Assets/Scripts/Gameplay/Weapons/WeaponBase.cs`, `Assets/Scripts/Gameplay/Weapons/IWeapon.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- WeaponBase의 추상 메서드를 분석하여 각 포탑이 오버라이드해야 할 메서드 목록 확인
- 발키리의 탄퍼짐(Spread) 로직 설계: 원뿔형 랜덤 방향 vs 고정 패턴
- 저거너트의 관통(Pierce) 로직: Projectile에서 처리 vs Weapon에서 설정
- **Verify Scope Restriction and potential conflicts with p02 (EXP Gem)**
</thinking>
<implementation>
- Create 4 weapon classes and 4 TurretData SO assets.
</implementation>
<verification>
- [ ] 4종 포탑 클래스가 WeaponBase를 정상 상속하며 컴파일 통과
- [ ] 각 포탑의 스탯이 GDD 03_data_and_balance.md 수치와 일치
- [ ] IFireStrategy 구현체가 올바른 발사 패턴 생성
- [ ] EOF empty line and naming conventions verified
- [ ] **Scope Restriction (Weapon files + SO assets only) strictly verified**
</verification>
</output_format>
