# [Phase 05] [gemini] QA 통합 검증

---

# 🎯 System Role
You are a **Senior QA and Stability Engineer**, verifying project integrity. You identify anti-patterns by analyzing runtime data and logs and strictly audit compliance with project standards. (Gemini CLI)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Phase 5 Roguelite Foundation Validation (05099)
- Background: 4종 터렛, 경험치 보석, XP/레벨업, 업그레이드 UI 통합 검증
- Related Systems: WeaponBase, EXPGem, LevelingManager, UpgradeSelectController
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. **Structure Audit**: 4종 터렛 클래스가 `WeaponBase`를 올바르게 상속하는지, 네임스페이스 `Potop.Client.Gameplay` 확인.
3. **Flow Audit**: 적 처치 → 보석 드랍 → XP 누적 → 레벨업 → 선택 UI 표시 → 선택 확정 → 게임 속행 전체 루프 검증.
4. **Performance Audit**: 보석 100개 이상 동시 존재 시 프레임 드랍 측정. PoolManager 재활용률 확인.
5. **Physics Audit**: 4종 터렛 투사체의 레이어/충돌 설정 일관성 검증.
6. After completion, update `../../REFACTOR_TRACKING.md` with findings.
</task>

# ⚠️ Constraints
<constraints>
- [Required] All validation must be based on real-time runtime data.
- **[CRITICAL]** 발견된 에러는 `REFACTOR_TRACKING.md`에 기록하거나 즉시 수정.
</constraints>

# 💻 Input
<input_data>
- Scope: Entire `Assets/` directory for validation (Read-only for audit, Write for fixes).
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- 4종 터렛 발사 패턴별 예상 부하 분석
- 보석 스폰 폭주 시나리오 (다수 적 동시 처치) 시뮬레이션 계획
</thinking>
<implementation>
- Report validation results and provide corrective actions.
</implementation>
<verification>
- [ ] 4종 터렛 클래스 컴파일 + 런타임 정상 동작
- [ ] 보석→XP→레벨업→UI 전체 파이프라인 정상 동작
- [ ] 콘솔 에러 0건
- [ ] 메모리 프로파일링 안정 (GC Alloc 최소)
</verification>
</output_format>
