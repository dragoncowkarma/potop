# Prompt: 모듈화된 터렛 무기 아키텍처 설계 및 구현

## Role: Jules (Logic & Architecture Specialist)
## Milestone: [02501] 파츠 기반 무기 아키텍처 (Weapon Parts)

### 🎯 Objective
Project POTOP은 4종의 기본 포탑을 제공하지만, 향후 '파츠 교체' 및 '시너지 변이'가 가능해야 합니다. 무기를 Body, Barrel, Magazine으로 모듈화하여 확장성을 확보합니다.

### 🛠️ Requirements
1. **Interface & Base Class**:
    - `IWeapon` 인터페이스 정의: `Fire()`, `Reload()`, `UpdateState()` 포함.
    - `WeaponBase` 추상 클래스: `ScriptableObject`로부터 공격력, 공속, 탄속 등 기초 스탯을 로드.
2. **파츠 시스템 (Part System)**:
    - `WeaponBody`, `WeaponBarrel`, `WeaponMagazine` 클래스를 분리.
    - 각 파츠는 `ScriptableObject` 데이터 기반으로 스탯 가중치와 특수 효과(예: 탄퍼짐 감소, 관통 추가)를 부여할 수 있게 설계하세요.
3. **Strategy Pattern 적용**:
    - 발사 로직(`FireStrategy`)을 분리하여 '직선 발사', '확산 발사', '곡사 발사'를 런타임에 교체 가능하도록 구현하세요.
    - 각 발사 로직은 `02502`에서 정의될 `IDamageable` 인터페이스를 통해 적에게 피해를 전달해야 합니다.
4. **Event System**:
    - `EventBroker`를 통해 발사 시점, 재장전 시점 등에 이벤트를 발행해야 함.

### ⚠️ Constraints
- Unity 6.0 LTS (C# 12) 문법 사용.
- `SerializedField`와 `FormerlySerializedAs`를 적절히 활용하여 인스펙터 노출 유지.
- 성능 최적화를 위해 불필요한 `Update` 호출을 지양하고 가비지 컬렉션을 최소화하세요.
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
