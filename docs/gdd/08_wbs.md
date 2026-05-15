# 📊 WBS (Work Breakdown Structure) — Phase 3.5 이후

> **작성일:** 2026-05-15 | **작성자:** PM (Antigravity) | **기준:** Phase 1~3 완료

## 범례

| 기호 | 의미 |
|:---|:---|
| **J** | Jules (로직/아키텍처) |
| **A** | Antigravity (Unity/UI/VFX) |
| **G** | Gemini CLI (QA/검증) |
| **→** | 선행 의존성 |

---

## Phase 3.5: 시스템 안정화 (1주) `[진행 중]`

| WBS ID | 작업 | 세부 분해 | 담당 | 의존성 | 산출물 |
|:---|:---|:---|:---|:---|:---|
| 3.5.1 | [03501] 적 AI 최적화 | ① EnemyBase에 FSM 패턴 적용 ② 시간 분할 회전(Time-sliced rotation) ③ PoolManager 연동 테스트 | **J** | → Phase 3 완료 | `EnemyStateMachine.cs`, 수정된 `EnemyBase.cs` |
| 3.5.2 | [03502] 피버 디커플링 | ① FeverManager를 EventBroker 기반으로 리팩토링 ② 콤보 계산 로직 분리 ③ 피버 레벨별 이벤트 발행 | **J** | → 3.5.1 | 수정된 `FeverManager.cs` |
| 3.5.3 | [03599] 성능 프로파일링 | ① Unity Profiler 기반 메모리 스냅샷 ② PoolManager 재활용률 검증 ③ WaveManager 스폰 부하 측정 | **G** | → 3.5.2 | 성능 보고서, `REFACTOR_TRACKING.md` 업데이트 |

---

## Phase 4: 로그라이트 기반 (2주)

| WBS ID | 작업 | 세부 분해 | 담당 | 의존성 | 산출물 |
|:---|:---|:---|:---|:---|:---|
| 4.1 | [04001] 터렛 클래스 (4종) | ① `GuardianWeapon`, `ValkyrieWeapon`, `JuggernautWeapon`, `NovaWeapon` — `WeaponBase` 상속 ② 각 터렛별 `TurretData.asset` SO 생성 ③ `IFireStrategy` 구현체별 발사 패턴 | **J** (병렬 p01) | → Phase 3.5 완료 | 4개 WeaponBase 파생 클래스, 4개 SO |
| 4.2 | [04002] 경험치 보석 시스템 | ① `EXPGem.cs` + PoolManager 등록 ② 자력 흡수 반경 로직 (`MagnetRadius`) ③ 3단계 보석 (Blue/Green/Red) SO 데이터 | **J** (병렬 p02) | → Phase 3.5 완료 | `EXPGem.cs`, `EXPGemData.asset` ×3 |
| 4.3a | [04003] XP/레벨업 로직 | ① `LevelingManager.cs` — XP 누적/레벨업 판정 ② `UpgradePool.cs` — 랜덤 3~4개 선택지 추출 ③ EventBroker: `OnLevelUp(int level)` 이벤트 | **J** (병렬 p03) | → 4.2 | `LevelingManager.cs`, `UpgradePool.cs` |
| 4.3b | [04003] 업그레이드 선택 UI | ① `UpgradeSelectPanel.uxml` / `.uss` ② `UpgradeSelectController.cs` — 선택 시 `Time.timeScale=0` ③ 선택지 카드 애니메이션 | **A** | → 4.3a | UXML/USS 파일, Controller 스크립트 |
| 4.4 | [04099] Phase 4 통합 검증 | ① 4종 터렛 발사 테스트 ② 보석 드랍→XP→레벨업→선택 UI 전체 루프 ③ 콘솔 에러 0건 확인 | **G** | → 4.3b | 검증 보고서 |

---

## Phase 4.5: 코어 루프 리팩토링 및 안정화 (1주)

| WBS ID | 작업 | 세부 분해 | 담당 | 의존성 | 산출물 |
|:---|:---|:---|:---|:---|:---|
| 4.5.1 | [04501] 무기 아키텍처 통합 | ① `TurretShooter.cs`를 `WeaponBase` 상속 구조로 리팩토링 ② `IFireStrategy` 적용 | **J** (단일) | → Phase 4 완료 | `TurretShooter.cs` 수정, 무기 아키텍처 |
| 4.5.2 | [04502] 투사체 핵심 특성 추가 | ① `NovaWeapon`용 AoE 데미지 판정 로직 추가 ② `JuggernautWeapon`용 관통 및 넉백 구현 | **J** (단일) | → 4.5.1 | `Projectile.cs` 수정, 각 무기 로직 |
| 4.5.3 | [04503] 데이터 및 UI 기술 부채 해결 | ① `EXPGemData`를 독립 스크립트로 분리 ② `UpgradeSelectController` 내 `FindFirstObjectByType` 제거 | **J** (병렬 p03) | → Phase 4 완료 | `EXPGemData.cs`, UI 컨트롤러 수정 |
| 4.5.4 | [04504] 피드백 및 최적화 통합 | ① 전투 루프에 `CameraShakeController` 연동 ② `VFXTrigger` 디스폰 로직을 코루틴에서 `PoolManager`로 이관 | **A** (병렬 p04) | → Phase 4 완료 | 연동된 카메라 및 최적화된 VFX |
| 4.5.5 | [04599] Phase 4.5 통합 검증 | ① 무기 발사 및 특성 정상 작동 여부 검증 ② 런타임 성능 프로파일링 및 에러 로그 체크 | **G** | → 4.5.1 ~ 4.5.4 | Phase 4.5 검증 보고서 |

---

## Phase 5: 빌드 다양성 (2주)

| WBS ID | 작업 | 세부 분해 | 담당 | 의존성 | 산출물 |
|:---|:---|:---|:---|:---|:---|
| 5.1 | [05001] 업그레이드 확률 테이블 | ① `UpgradeTableData.asset` SO 설계 (레어리티별 가중치) ② 중복 방지 + 보장 시스템 (Pity) ③ 밸런스 테이블 CSV 임포터 | **J** (병렬 p01) | → Phase 4.5 완료 | `UpgradeTableData.cs/.asset` |
| 5.2 | [05002] 무기 변이 & 시너지 | ① `MutationSynergyManager.cs` — 조합 감지 ② 3종 시너지 규칙 (Pierce+Explosion 등) ③ `IModifier` 인터페이스 확장 | **J** (병렬 p02) | → Phase 4.5 완료 | `MutationSynergyManager.cs` |
| 5.3 | [05003] 투사체 변이 물리 | ① 관통 로직 (`PierceModifier`) ② 도탄 로직 (`BounceModifier`) — 레이캐스트 기반 ③ 거대화/유도 로직 | **J** (병렬 p03) | → 5.2 | 4개 Modifier 클래스 |
| 5.4 | [05004] 궁극 진화 | ① `OverdriveEvolution.cs` — 시너지 완성 감지 ② 3종 궁극 무기 프리팹 & SO ③ 보스 상자 획득 시 진화 트리거 | **J** | → 5.2, 5.3 | `OverdriveEvolution.cs`, 3개 프리팹 |
| 5.5 | [05099] Phase 5 통합 검증 | ① 변이 조합 교차 테스트 ② 투사체 물리 안정성 (무한 루프/관통 무한 방지) ③ 궁극 진화 발동 조건 검증 | **G** | → 5.4 | 검증 보고서 |

---

## Phase 6: 전술 & 메타 경제 (2.5주)

| WBS ID | 작업 | 세부 분해 | 담당 | 의존성 | 산출물 |
|:---|:---|:---|:---|:---|:---|
| 6.1 | [06001] 오버차지 시스템 | ① `OverchargeController.cs` — 게이지 관리 ② 공속 200% 버프 적용/해제 ③ 과열 3초 페널티 + UI 피드백 | **J** (병렬 p01) | → Phase 5 완료 | `OverchargeController.cs` |
| 6.2 | [06002] 전술 스킬 3종 | ① `TacticalSkillBase.cs` + `ISkillEffect` ② EMP/궤도 폭격/보호막 각 구현체 ③ 에너지 소모/충전 로직 | **J** (병렬 p02) | → Phase 5 완료 | 3개 스킬 클래스, `TacticalSkillBase.cs` |
| 6.3 | [06003] 아이템 드랍 | ① `ItemDrop.cs` + PoolManager 등록 ② 3종 아이템 SO (`RepairKit`/`SuperMagnet`/`SmartBomb`) ③ 드랍 확률 테이블 (웨이브 Phase별 차등) | **J** (병렬 p03) | → Phase 5 완료 | `ItemDrop.cs`, 3개 SO |
| 6.4a | [06004] 영구 강화 로직 | ① `MetaUpgradeManager.cs` — 6개 강화 항목 관리 ② Gem 경제 (획득/소모) ③ 세이브 데이터 구조 설계 | **J** | → 6.3 | `MetaUpgradeManager.cs` |
| 6.4b | [06004] 로비 UI 기초 | ① 기본 로비 화면 UXML/USS ② 강화 항목 6종 카드 UI ③ Gem 잔고 표시 + 구매 버튼 | **A** | → 6.4a | UXML/USS 파일, `LobbyController.cs` |
| 6.5 | [06099] Phase 6 통합 검증 | ① 오버차지 게이지 사이클 테스트 ② 스킬 에너지 소모/쿨다운 검증 ③ 메타 강화 수치 적용 확인 | **G** | → 6.4b | 검증 보고서 |

---

## Phase 7: 엔드게임 & 보스전 (2.5주) — **Vertical Slice**

| WBS ID | 작업 | 세부 분해 | 담당 | 의존성 | 산출물 |
|:---|:---|:---|:---|:---|:---|
| 7.1 | [07001] 보스 비주얼 | ① `TitanCore.prefab` 계층 구조 ② 페이즈별 머티리얼 전환 (Blue→Red) ③ 보스 Animator 구성 | **A** | → Phase 6 완료 | 프리팹, 머티리얼, Animator |
| 7.2 | [07002] 보스 AI | ① `TitanCoreAI.cs` — 3페이즈 FSM ② Phase 1: 회전 쉴드 패턴 ③ Phase 2: 타겟 레이저 ④ Phase 3: 광폭화 | **J** | → 7.1 | `TitanCoreAI.cs` |
| 7.3 | [07003] 오버클럭 모드 | ① `OverclockMode.cs` — 30초마다 1.5배 스케일링 ② 점수 배율 가산 (+0.1x/분) ③ WaveManager 무한 모드 플래그 | **J** | → 7.2 | `OverclockMode.cs` |
| 7.4 | [07004] 게임 플로우 통합 | ① `GameFlowController.cs` — 15분 타이머→보스 스폰→오버클럭 진입 ② 게임오버 결산 화면 ③ 로비 복귀 루프 | **J** + **A** | → 7.3 | `GameFlowController.cs`, 결산 UI |
| 7.5 | [07005] 튜토리얼 프로토타입 | ① 핵심 조작(Look/Shoot) 가이드 ② 업그레이드 흐름 기초 UI ③ 튜토리얼 전용 소규모 웨이브 구성 | **J** + **A** | → 7.4 | `TutorialSystem.cs`, 튜토리얼 씬 |
| 7.6 | [07099] VS 통합 검증 | ① 15분 풀 플레이 테스트 ② 보스전 3페이즈 전환 검증 ③ 오버클럭 스케일링 안정성 ④ 튜토리얼 흐름 확인 | **G** | → 7.5 | **Vertical Slice 검증 보고서** |

---

## Phase 8~13: 요약 (세부 WBS는 해당 Phase 착수 시 작성)

| Phase | 주제 | 예상 기간 | 핵심 산출물 |
|:---|:---|:---|:---|
| **8** | 폴리싱 & 게임필 | 2주 | 사운드 시스템, VFX 최종본, 밸런스 패치 |
| **9** | 모바일 출시 | 3~4주 | 스토어 빌드, 튜토리얼, 다국어, 광고 SDK |
| **10** | 글로벌 서버 | 4~6주 | API 서버, 리더보드, 클라우드 세이브 |
| **11** | PC (Steam) | 4주 | SteamWorks 연동, PC 최적화 |
| **12** | VR 이식 | 6~8주 | OpenXR, VR UI, 공간 음향 |
| **13** | 콘솔 이식 | 6~8주 | DevKit 대응, 햅틱, 심사 통과 |

---

## 크리티컬 패스

```
Phase 3.5 → Phase 4 → Phase 4.5 → Phase 5 → Phase 6 → Phase 7(VS) → Phase 8 → Phase 9(출시)
```

**총 예상 기간 (모바일 출시까지):** 15~18주 (약 4개월)
