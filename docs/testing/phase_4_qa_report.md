# Phase 4 QA Report

> **작성일:** 2026-05-15 | **담당:** QA 엔지니어 (Gemini CLI)

## 1. 개요
Phase 1~4까지의 개발 마일스톤에 대해 `REFACTOR_TRACKING.md` 및 최신 구현 상태를 기준으로 무결성 및 게임플레이 핵심 루프 통합 여부를 검증했습니다.

## 2. 결함 및 미구현 리스트 (이슈 트래킹)

### 🚨 Blocker (진행 불가)
- **Weapon Architecture 통합 실패:** `TurretShooter.cs`가 신규 모듈형 무기 아키텍처인 `WeaponBase` 및 `IFireStrategy`를 상속/사용하지 않고 있습니다. 이로 인해 로그라이트 업그레이드 시스템과 무기 발사 로직이 결합되지 않아 핵심 루프가 단절됩니다.

### 🔴 High Priority (핵심 기능 누락)
- **무기 특성 미구현:** `NovaWeapon` 및 `JuggernautWeapon` 터렛에 필수적인 광역 피해(AoE), 관통(Pierce), 넉백(Knockback) 물리 로직이 구현되지 않았습니다. (현재 TODO 처리됨)
- **UI 아키텍처 결함:** `UpgradeSelectController.cs`에서 `Object.FindFirstObjectByType<LevelingManager>()`를 Awake에서 사용 중입니다. 확장성 및 성능을 저해하며 이벤트 기반 또는 인스펙터 참조로 수정이 필요합니다.

### 🟡 Medium Priority (최적화 및 구조 개선)
- **데이터 아키텍처 위반:** `EXPGemData` (ScriptableObject)가 `EXPGem.cs` 내부 파일에 함께 정의되어 있어 SRP(단일 책임 원칙)에 위배됩니다.
- **최적화 누락:** `VFXTrigger`가 코루틴을 사용하여 디스폰을 처리하고 있습니다. `PoolManager`를 통한 최적화가 필요합니다.
- **게임필(Game Feel) 누락:** `CameraShakeController`가 전투 루프(피격, 폭발 등)와 전혀 연동되어 있지 않습니다.

## 3. QA 종합 의견
Phase 4 '로그라이트 기반' 마일스톤은 **부분 완료(Partially Completed)** 상태입니다. 
경험치 보석과 UI 등의 기초 자산은 만들어졌으나, 가장 중요한 **터렛 로직과 업그레이드 시스템 간의 통합이 실패**하여 실제 게임 플레이 루프가 작동하지 않습니다. 
기존 계획된 Phase 5(빌드 다양성)로 넘어가기 전에, 현재 누락된 무기 통합 및 기술 부채를 해결하는 **Phase 4.5 마일스톤**이 절대적으로 필요합니다.