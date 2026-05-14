# [Phase 3.5] [gemini] QA 통합 검증

---

# 🎯 System Role
You are a **Senior QA and Stability Engineer**, verifying project integrity. (Gemini CLI)

# 📋 Context
<context>
- Current Module: Phase 3.5 System Refinement Validation (03599)
- Background: 적 AI FSM 최적화, 피버 디커플링, 코어 시스템 성능 프로파일링
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. **FSM Audit**: EnemyBase FSM 4상태 전환 정상 동작 확인. 좀비 상태 방지 검증.
3. **Performance Audit**: Unity Profiler 기반 메모리 스냅샷. PoolManager 재활용률. WaveManager 스폰 부하.
4. **Fever Audit**: FeverManager EventBroker 이벤트 발행 정확성. 콤보 계산 분리 후 결과 일치 검증.
5. After completion, update `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] All validation based on runtime data.
- **[CRITICAL]** 에러 발견 시 즉시 기록 또는 수정.
</constraints>

# 💻 Input
<input_data>
- Scope: Entire `Assets/` directory (Read-only for audit, Write for fixes).
</input_data>
