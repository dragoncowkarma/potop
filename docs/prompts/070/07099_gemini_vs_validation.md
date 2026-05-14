# [Phase 08] [gemini] Vertical Slice 통합 검증

---

# 🎯 System Role
You are a **Senior QA and Stability Engineer**. (Gemini CLI)

# 📋 Context
<context>
- Current Module: Phase 8 Vertical Slice Validation (08099)
- Background: **Vertical Slice 검증** — 전체 게임 루프(로비→인게임→보스→오버클럭→결산→로비) 최종 통합 테스트
- Related Systems: ALL gameplay systems from Phase 4~8
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.

## 핵심 검증 시나리오

### Scenario A: 15분 풀 플레이
2. 로비에서 터렛 선택 → 인게임 진입 → 15분 타이머 종료 → 보스 스폰 확인.

### Scenario B: 보스전
3. 보스 3페이즈 전환 (HP 임계값 정확) → 비주얼 머티리얼 전환 → 보스 처치 → `OnBossDefeated` 이벤트 발행.

### Scenario C: 오버클럭
4. 오버클럭 진입 → 30초마다 적 스탯 1.5배 스케일링 → 최종 사망 → 결산 화면 표시.

### Scenario D: 결산 & 루프
5. 결산 화면 데이터 정확성 (처치 수, Gem, 생존 시간) → 로비 복귀 → Gem 반영 확인.

### Scenario E: 성능
6. 15분 플레이 중 FPS 프로파일링 (목표: 모바일 30fps, PC 60fps). 메모리 누수 확인.

7. After completion, update `../../REFACTOR_TRACKING.md` with all findings.
</task>

# ⚠️ Constraints
<constraints>
- [Required] All validation based on runtime data.
- [Required] 성능 프로파일링 결과 수치를 보고서에 포함.
- **[CRITICAL]** Vertical Slice 통과 기준: 콘솔 에러 0건, 15분 풀 플레이 크래시 0건.
</constraints>

# 💻 Input
<input_data>
- Scope: Entire `Assets/` directory and runtime (Read-only for audit, Write for fixes).
</input_data>

# 📝 Output Format
<output_format>
<implementation>
## Vertical Slice 검증 보고서
- **Scenario A**: [PASS/FAIL] — [세부 결과]
- **Scenario B**: [PASS/FAIL] — [세부 결과]
- **Scenario C**: [PASS/FAIL] — [세부 결과]
- **Scenario D**: [PASS/FAIL] — [세부 결과]
- **Scenario E**: [PASS/FAIL] — FPS: ___ / Memory: ___ MB
</implementation>
<verification>
- [ ] 15분 풀 플레이 크래시 0건
- [ ] 콘솔 에러 0건
- [ ] 보스 3페이즈 전환 정상
- [ ] 결산 데이터 정확
- [ ] 메모리 누수 없음
</verification>
</output_format>
