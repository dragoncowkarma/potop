## **8\. 개발 마일스톤 및 AI 작업 프롬프트 (Timeline & AI Prompts)**

※ **\[Jules: 코드 제너레이터\]**, **\[Antigravity: 유니티 물리/테스트\]**

* **Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP) ✅ 완료 (2026-04-24)**
  * [x] [Jules] 기본 포탑 회전 및 투사체 발사 구현
  * [x] [Jules] 1인칭 마우스 룩 (`Player.cs`, `FirstPersonLook.cs`)
  * [x] [Jules] 적 스폰 및 AI (`EnemySpawner.cs`, `Enemy.cs`)
  * [x] [Jules] GameManager 기반 HP/점수/게임오버 시스템
  * [x] [Jules] HUD UI 및 시작 화면 구성
  * [x] [Antigravity] URP 렌더링 파이프라인 및 물리 레이어 테스트
  * [x] [Antigravity] Player.cs 버그 수정 및 입력 차단 로직 검증

* **Phase 2: 시스템 확장 및 구조 고도화 (진행 중)**
  * **[Milestone 3]** 이벤트 브로커 적용: `Enemy` 및 `Projectile`이 `EventBroker`를 통해 통신하도록 리팩토링.
  * **[Milestone 4-5]** 오브젝트 풀링: `ObjectPoolManager` 구현 및 적/발사체 생성/파괴 로직을 풀링으로 전환.
  * **[Milestone 6]** 데이터 스키마 정의: `ScriptableObject` 기반 `EnemyData`, `WeaponData`, `WaveData` 정의.
  * **[Milestone 7-8]** 웨이브 매니저: `WaveManager` 구현 및 `EnemySpawner`와 연동하여 타임라인 기반 스폰 제어.
  * **[Milestone 9]** 적 다양화: SO 데이터를 바탕으로 속도, HP 등이 다른 다양한 적 타입 구현.
  * **[Milestone 10-12]** 무기 시스템: `IWeapon` 인터페이스 기반 무기 교체 시스템 및 신규 무기(샷건 등) 추가.

* **Phase 3: 로그라이트 성장 및 투사체 진화 (예정)**
  * [ ] [Jules] 레벨업 매니저 및 4-3 섹션의 강화 선택지 수치 구현
  * [ ] [Jules] 투사체 변이 (다연발, 관통, 도탄, 거대화, 폭발) 물리 시너지 로직
  * [ ] [Antigravity] 투사체 물리 시너지 연산 성능 QA

* **Phase 4: 콘텐츠 폴리싱 및 기믹 대응**
  * [ ] [Jules] 보스전 및 무한 모드 15분 웨이브 매니저
  * [ ] [Jules] 특수 적 클래스 4종 (미사일, 메테오, 스웜, 자폭) 구현
  * [ ] [Antigravity] 신규 기믹 및 보스전 밸런스 테스트

* **Phase 5: 메타 시스템 및 시스템 안정화**
  * [ ] [Jules] 옵저버 패턴 기반 도전 과제 시스템
  * [ ] [Jules] 로컬 세이브 데이터(JSON) 연동 및 영구 강화 상점 로직
  * [ ] [Antigravity] 세이브 데이터 무결성 검증 툴 제작

* **Phase 6: 출시 준비 및 플랫폼 최적화**
  * [ ] [Jules] 카메라 쉐이크, 피버 연출, 가변 피치 오디오 매니저
  * [ ] [Jules] AdMob/Unity Ads 연동
  * [ ] [Antigravity] 타격감 조율 및 VR 최적화

**[추가 구현 사항: 백엔드 및 확장]**

* **Phase 7: 백엔드 연동**  
  * [ ] [Jules] 무한 모드 글로벌 리더보드 연동 (Firebase/PlayFab)  
  * [ ] [Jules] 클라우드 세이브 시스템

