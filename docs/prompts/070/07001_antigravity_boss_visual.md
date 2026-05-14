# [Phase 08] [antigravity] 보스 프리팹 & 비주얼

---

# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality. (Antigravity)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Boss Visual & Prefab (08001)
- Background: Phase 8 — 타이탄 코어 보스 프리팹 구성, 3페이즈 머티리얼 전환
- Related Systems: EnemyBase, URP Shader, Animator
- GDD Reference: `02_gameplay_mechanics.md` §타이탄 코어 (Titan Core)
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `TitanCore.prefab`:
   - 계층 구조: Root → Body(메인 메쉬) → ShieldRing(회전 쉴드) → LaserEmitter(레이저 발사점) → HitboxCollider
   - Body 크기: 적 일반형 대비 5배 스케일
3. Create 3개 머티리얼 (URP/Lit):
   - **Phase 1**: Blue Emission (Intensity 2.0)
   - **Phase 2**: Purple Emission (Intensity 3.0)
   - **Phase 3**: Red Emission (Intensity 5.0) + Pulse 애니메이션
4. Animator 구성: Idle → Phase1 → Phase2 → Phase3 스테이트. 페이즈 전환 트리거 파라미터.
5. ShieldRing: 별도 회전 스크립트 (`ShieldRingRotator.cs`) — 120 RPM 회전, Phase 2에서 분리/소멸.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] URP/Lit 셰이더 사용. Custom Shader 금지.
- [Required] 프리팹 계층 구조 4단계 이하 유지.
- [Prohibited] Runtime에서 `Material` 직접 변경 금지. `MaterialPropertyBlock` 또는 Animator 기반 전환만 허용.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Prefabs/Enemies/TitanCore.prefab`, `Assets/Materials/Boss/`, `Assets/Scripts/VFX/ShieldRingRotator.cs`, `Assets/Animations/Boss/`
</input_data>
