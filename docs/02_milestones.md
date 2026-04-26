# 🚀 POTOP 개발 마일스톤 (Vibe Coding 최적화)

본 문서는 `docs/01_detailed_plan.md`를 바탕으로, LLM 바이브 코딩(Vibe Coding) 세션에 최적화되도록 작업을 매우 세밀하게 나눈 마일스톤입니다.
'Context Isolation', 'Interface First', 'Clear I/O' 원칙에 따라 설계되었습니다.

---

## [Phase 1.5] 코어 아키텍처 리팩토링

### Milestone 1: 이벤트 브로커 시스템 설계 (인터페이스 및 뼈대)
- **목표:** 스크립트 간 직접 참조를 줄이기 위한 이벤트 브로커 클래스를 설계합니다. 실제 이벤트 연결은 다음 마일스톤에서 진행합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Core/GameManager.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Core/EventBroker.cs`]
- **작업 지침:** `static` 기반 혹은 C# `Action`을 이용한 `EventBroker` 클래스를 생성합니다. `OnEnemyKilled(int score)`, `OnPlayerTakeDamage(int damage)`, `OnScoreChanged(int newScore)`, `OnPlayerHealthChanged(int newHealth)`, `OnGameOver()` 이벤트를 선언만 합니다.
- **검증 방법:** 컴파일 에러가 발생하지 않는지 확인합니다. (유니티 빌드 또는 `dotnet build` 확인)

### Milestone 2: 이벤트 브로커 적용 - GameManager 및 HUD
- **목표:** 기존 `GameManager`와 `GameHUD`에 엮여 있는 직접 참조 로직을 제거하고, `EventBroker`를 구독/발행하도록 수정합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Core/GameManager.cs`, `potop_client/Assets/Scripts/Core/GameHUD.cs`, `potop_client/Assets/Scripts/Core/EventBroker.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Core/GameManager.cs`, `potop_client/Assets/Scripts/Core/GameHUD.cs`]
- **작업 지침:** `GameManager`는 HP나 점수 증감을 직접 UI에 쏘지 않고 `EventBroker`를 통해 이벤트를 발행합니다. `GameHUD`는 `Start`나 `OnEnable`에서 해당 이벤트를 구독하여 UI를 업데이트합니다.
- **검증 방법:** 기존 게임을 플레이했을 때 HP 감소 및 점수 증가 UI가 정상 작동하는지 육안 확인합니다.

### Milestone 3: 이벤트 브로커 적용 - Enemy 및 Projectile
- **목표:** `Enemy`와 `Projectile` 클래스가 플레이어에게 데미지를 주거나 파괴될 때, 직접 `GameManager.instance`를 호출하지 않고 이벤트를 발행하도록 수정합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Enemy.cs`, `potop_client/Assets/Scripts/Projectile.cs`, `potop_client/Assets/Scripts/Core/EventBroker.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Enemy.cs`, `potop_client/Assets/Scripts/Projectile.cs`]
- **작업 지침:** 플레이어 피격 시 `EventBroker.CallPlayerTakeDamage(damage)`, 적 파괴 시 `EventBroker.CallEnemyKilled(score)`를 호출하도록 변경합니다.
- **검증 실습:** 적이 플레이어에게 닿았을 때 HP가 깎이는지, 적이 총알에 맞았을 때 점수가 오르는지 확인합니다.

### Milestone 4: 오브젝트 풀링 매니저 설계
- **목표:** 잦은 생성/파괴를 방지하기 위해 `UnityEngine.Pool.ObjectPool`을 사용하는 싱글톤 풀링 매니저의 인터페이스와 기본 구조를 작성합니다.
- **Token Context (Read):** [] (새로 생성하므로 최소 컨텍스트)
- **Target Files (Write):** [`potop_client/Assets/Scripts/Core/ObjectPoolManager.cs`]
- **작업 지침:** `Enemy`와 `Projectile`을 각각 풀링할 수 있는 제네릭 풀 혹은 Dictionary 기반 다중 풀링 구조를 잡습니다. 구현부의 로직(Get, Release)만 작성합니다.
- **검증 방법:** 코드 내 문법 오류가 없는지 컴파일 확인합니다.

### Milestone 5: 오브젝트 풀링 적용 - 적(Enemy) 및 발사체(Projectile)
- **목표:** `EnemySpawner`와 `TurretShooter`가 `Instantiate` 대신 `ObjectPoolManager.Get`을, 적/총알 파괴 시 `Destroy` 대신 `Release`를 사용하도록 수정합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Core/ObjectPoolManager.cs`, `potop_client/Assets/Scripts/EnemySpawner.cs`, `potop_client/Assets/Scripts/TurretShooter.cs`, `potop_client/Assets/Scripts/Enemy.cs`, `potop_client/Assets/Scripts/Projectile.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/EnemySpawner.cs`, `potop_client/Assets/Scripts/TurretShooter.cs`, `potop_client/Assets/Scripts/Enemy.cs`, `potop_client/Assets/Scripts/Projectile.cs`]
- **작업 지침:** `Destroy(gameObject)`를 모두 풀 반환 로직으로 교체합니다. 풀에서 꺼낼 때 오브젝트의 상태(위치, 체력 등)를 초기화하는 로직(`OnEnable` 활용 등)을 꼼꼼히 작성합니다.
- **검증 방법:** 게임 실행 후 Hierarchy 창에서 오브젝트가 무한정 늘어나지 않고 비활성화/활성화 되는지 확인합니다.

---

## [Phase 2] 시스템 확장 (데이터, 웨이브, 무기, 적)

### Milestone 6: ScriptableObject 데이터 스키마 정의 (Enemy, Weapon, Wave)
- **목표:** 하드코딩된 수치를 분리하기 위해 데이터 컨테이너 역할을 하는 SO 클래스들을 정의합니다.
- **Token Context (Read):** [`docs/01_detailed_plan.md`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Data/EnemyData.cs`, `potop_client/Assets/Scripts/Data/WeaponData.cs`, `potop_client/Assets/Scripts/Data/WaveData.cs`]
- **작업 지침:** `ScriptableObject`를 상속받고 `[CreateAssetMenu]` 속성을 추가합니다. 필드만 정의하며, 로직은 포함하지 않습니다. (상세 스키마는 `01_detailed_plan.md`의 Section 4 참조)
- **검증 방법:** Unity 에디터에서 해당 SO 에셋들을 정상적으로 생성할 수 있는지 우클릭 메뉴를 확인합니다.

### Milestone 7: 웨이브 매니저(Wave Manager) 뼈대 설계
- **목표:** 웨이브 상태(현재 웨이브, 남은 적 수 등)를 관리하는 매니저의 틀을 잡습니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Data/WaveData.cs`, `potop_client/Assets/Scripts/Core/EventBroker.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Core/WaveManager.cs`]
- **작업 지침:** `WaveData` 리스트를 인스펙터에서 받아 관리합니다. `EventBroker`에서 적 사망 이벤트를 구독하여 '웨이브 종료 조건'을 체크하는 로직을 뼈대만 작성합니다. (디버그 로그로 대체)
- **검증 방법:** 스크립트 컴파일 에러 유무 확인.

### Milestone 8: 웨이브 매니저 기능 구현 및 Spawner 연동
- **목표:** `WaveManager`의 상태에 따라 `EnemySpawner`의 스폰 로직이 변경되도록 결합합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Core/WaveManager.cs`, `potop_client/Assets/Scripts/EnemySpawner.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Core/WaveManager.cs`, `potop_client/Assets/Scripts/EnemySpawner.cs`]
- **작업 지침:** 기존 타이머 기반의 무한 스폰을, `WaveManager`에서 전달받은 `WaveData`의 `SpawnInterval`과 `AllowedEnemyTypes`를 참고하여 스폰하도록 바꿉니다.
- **검증 방법:** 웨이브 1 목표치 달성 후 웨이브 2로 넘어갈 때 스폰 주기나 적 타입이 변하는지 콘솔 로그 및 육안 확인.

### Milestone 9: 적(Enemy) 다양화 - 데이터 연동
- **목표:** 기존 단일 로직의 `Enemy` 스크립트가 `EnemyData(SO)`를 바탕으로 속도, HP, 점수 등을 초기화하도록 수정합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Enemy.cs`, `potop_client/Assets/Scripts/Data/EnemyData.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Enemy.cs`]
- **작업 지침:** `public EnemyData data;` 필드를 추가하고, `Start` 혹은 풀에서 꺼내질 때 데이터의 값을 객체의 실제 값으로 할당합니다.
- **검증 방법:** 에디터에서 서로 다른 `EnemyData`를 주입한 프리팹들이 제각기 다른 속도로 이동하는지 확인.

### Milestone 10: 무기 시스템 인터페이스(IWeapon) 설계
- **목표:** 다양한 무기를 지원하기 위한 인터페이스 구조를 작성합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/TurretShooter.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Weapon/IWeapon.cs`, `potop_client/Assets/Scripts/Weapon/WeaponBase.cs`]
- **작업 지침:** `void Fire(Transform firePoint, WeaponData data)` 메서드를 가진 인터페이스 혹은 추상 클래스를 작성합니다.
- **검증 방법:** 컴파일 에러 확인.

### Milestone 11: 터렛 슈터(TurretShooter) 무기 시스템 연동
- **목표:** `TurretShooter`가 하드코딩된 사격 로직 대신 `IWeapon`을 호출하도록 리팩토링합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/TurretShooter.cs`, `potop_client/Assets/Scripts/Weapon/IWeapon.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/TurretShooter.cs`, `potop_client/Assets/Scripts/Weapon/BasicGun.cs`]
- **작업 지침:** 기존 사격 로직을 `BasicGun` 스크립트로 분리하여 `IWeapon`을 상속받게 합니다. `TurretShooter`는 현재 장착된 `IWeapon`의 `Fire`만 호출합니다.
- **검증 방법:** 마우스 좌클릭 시 기존과 동일하게 총알이 발사되는지 확인합니다.

### Milestone 12: 신규 무기(샷건) 구현
- **목표:** `IWeapon`을 상속받는 새로운 무기 패턴(샷건)을 구현합니다.
- **Token Context (Read):** [`potop_client/Assets/Scripts/Weapon/IWeapon.cs`, `potop_client/Assets/Scripts/Core/ObjectPoolManager.cs`]
- **Target Files (Write):** [`potop_client/Assets/Scripts/Weapon/Shotgun.cs`]
- **작업 지침:** 한 번 클릭에 여러 발의 총알이 부채꼴 각도로 퍼져 나가도록 `Shotgun` 클래스를 작성합니다.
- **검증 방법:** `TurretShooter`에 `Shotgun`을 장착하고 발사 시 산탄총 형태로 퍼져 나가는지 인게임에서 확인.

---

## [Phase 3] 폴리싱 (예정)
*(Phase 3의 세부 마일스톤은 Phase 2가 안정화된 이후 진행되며, 사운드, VFX, 최적화 모듈로 추가 쪼개기 작업이 필요합니다.)*
