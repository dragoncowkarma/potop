# 🚨 Implementation Conflict Report (docs/conflict_report.md)

> [!NOTE]
> **원본 작성 시점**: Phase 2 (2026-05-02). Phase 3~7을 거치며 대부분의 항목이 해결되었습니다.
> **마지막 검토**: Phase 8+ 전문가 점검 (2026-06-01)

This document records discrepancies between the project's Game Design Document (GDD) and the current technical implementation in `potop_client/`.

## 1. Core Gameplay & Controls

| Feature | GDD Specification | Current Implementation | Status |
| :--- | :--- | :--- | :--- |
| **Turret Rotation Speed** | Constant 180°/sec. | Input-based (Delta * Sensitivity). | ⚠️ **Open → 09004 Owner**: 회전 속도 상한을 모바일 입력 작업의 필수 합격 기준으로 승격 |
| **Keyboard Input** | WASD/Arrow keys for rotation. | LookAction (Delta/Vector2) only. | ⚠️ **Open → 09004 Owner**: 키보드 회전을 모바일/데스크톱 입력 폴백 합격 기준으로 승격 |
| **Turret Classes** | 4 distinct types (Guardian, Valkyrie, etc.). | 4종 WeaponBase 상속체 구현 완료. | ✅ **Resolved** (Phase 4) |

## 2. Enemy System

| Feature | GDD Specification | Current Implementation | Status |
| :--- | :--- | :--- | :--- |
| **Enemy Types** | 6종 (Scouter~Titan Core). | EnemyBase FSM + 4 variant + TitanCoreAI 구현. | ✅ **Resolved** (Phase 3~7) |
| **Spawning Logic** | Wave-based timeline (Phase 1-5). | WaveManager 5단계 타임라인 구현. | ✅ **Resolved** (Phase 3) |

## 3. Architecture & Data

| Feature | GDD Specification | Current Implementation | Status |
| :--- | :--- | :--- | :--- |
| **Data Management** | `ScriptableObject` driven. | 15+ SO 타입 구현 (WeaponData, EnemyData 등). | ✅ **Resolved** (Phase 2~4) |
| **Object Pooling** | `UnityEngine.Pool` for projectiles/enemies. | PoolManager 구현 완료. | ✅ **Resolved** (Phase 2) |
| **Event System** | Centralized Event Broker. | EventBroker 정적 클래스 + 12개 이벤트 타입. | ✅ **Resolved** (Phase 2) |
| **Assembly Definitions** | 레이어별 컴파일 격리. | 5개 asmdef 파일 설정 완료 (Core, Data, Gameplay, UI, Tests.Editor). | ✅ **Resolved** (Phase 7.5) |

## 4. Progression & Meta

| Feature | GDD Specification | Current Implementation | Status |
| :--- | :--- | :--- | :--- |
| **RPG Elements** | EXP, Leveling, Passive choices. | EXPGem + LevelingSystem + UpgradeSelectUI 구현. | ✅ **Resolved** (Phase 4) |
| **Game Lifecycle** | 15m Wave → Boss → Overclock. | GameFlowController 6상태 FSM 구현. | ✅ **Resolved** (Phase 7) |

## 5. Summary of Divergence
Phase 7 Vertical Slice 완료 시점에서 대부분의 GDD↔구현 간 차이가 해소되었습니다.
**잔여 항목**: 입력 시스템 상세 (회전 속도 상한, 키보드 회전) — Phase 9.4 모바일 입력 최적화 프롬프트와 task JSON의 필수 검증 항목으로 반영됨.

---
**Last Updated**: 2026-06-01
**Agent**: Codex (Phase 8+ Expert Review)
