# Prompt: 통합 체력 및 피해 시스템 (Health & Damage System)

## Role: Jules (Logic & Architecture Specialist)
## Milestone: [02502] 통합 체력 및 피해 시스템

### 🎯 Objective
현재 `EnemyBot`과 `GameManager`에 파편화되어 있는 체력 관리 및 데미지 전달 로직을 하나의 통합된 시스템으로 추상화합니다. 이를 통해 다양한 적 타입과 플레이어 업그레이드에 유연하게 대응할 수 있는 기반을 마련합니다.

### 🛠️ Requirements
1. **IDamageable Interface**:
    - `void TakeDamage(DamageInfo info)` 메서드를 포함하는 인터페이스 정의.
    - `DamageInfo` 구조체: `int Amount`, `Vector3 HitPoint`, `Vector3 HitNormal`, `GameObject Instigator`, `DamageType Type` 포함.
2. **Health Component**:
    - 모든 공격 가능한 오브젝트(적, 플레이어, 구조물)에 부착 가능한 범용 컴패넌트.
    - `MaxHealth`, `CurrentHealth` 관리.
    - `OnHealthChanged`, `OnDeath`, `OnDamaged` 이벤트를 `EventBroker`를 통해 발행하거나 C# 이벤트를 노출.
3. **Refactoring**:
    - `EnemyBot.cs`에서 자체 체력 변수를 제거하고 `Health` 컴포넌트를 사용하도록 수정.
    - `Projectile.cs`가 충돌 시 `IDamageable`을 찾아 `TakeDamage`를 호출하도록 수정.
4. **DamageType Enum**:
    - `Normal`, `Fire`, `Electric`, `Explosive` 등 기초 타입 정의 (향후 시너지 시스템 대비).

### ⚠️ Constraints
- Unity 6.0 LTS (C# 12) 표준 준수.
- 인터페이스를 활용하여 OCP(Open-Closed Principle)를 준수하세요.
- `SerializedField` 활용 및 캡슐화 철저.
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
