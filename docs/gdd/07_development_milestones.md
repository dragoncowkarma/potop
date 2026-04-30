## **7. 개발 마일스톤 및 AI 작업 상세 (Milestones & AI Prompts)**

### **Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP)**

*   **[Milestone 1] 기본 포탑 회전 및 투사체 발사**
    *   **내용:** 입력 시스템에 따른 포탑 회전 로직 및 기본 사격 구현.
    *   **상세:** `TurretShooter.cs`를 통한 `Projectile.prefab` 생성 및 전방 추진력 부여.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:CoreCombat]Implement Turret rotation logic based on Input system;Create TurretShooter.cs for instantiating Projectile.prefab at FirePoint;Handle simple forward velocity in Projectile.cs;`

*   **[Milestone 2] 1인칭 마우스 룩**
    *   **내용:** FPS 방식의 시점 제어 및 클램핑.
    *   **상세:** 수직 회전각 제한(-90~90도) 및 게임 오버 시 입력 차단 로직.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:PlayerController]Implement FirstPersonLook.cs;Clamp vertical rotation between -90 and 90 degrees;Integrate with Player.cs for input enabling/disabling;`

*   **[Milestone 3] 적 스폰 및 기초 AI**
    *   **내용:** 360도 전 방향 스폰 및 플레이어 추적.
    *   **상세:** 반지름 25m 구역 내 랜덤 스폰 및 플레이어를 향해 직진하는 이동 로직.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:EnemySystem]Implement EnemySpawner.cs using 360-degree radial spawning (Radius:25m, Height:1-5m);Create Enemy.cs with basic LookAt and MoveTowards(Player) logic;`

*   **[Milestone 4] GameManager 및 기본 HUD**
    *   **내용:** 게임 상태 관리 싱글톤 및 기본 UI 표시.
    *   **상세:** HP, Score, GameState(Start, Playing, GameOver) 관리 및 HUD 연동.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:GameManagement]Implement Singleton GameManager.cs;Manage Health(int), Score(int), GameState(Enum:Start,Playing,GameOver);Implement GameHUD.cs for HP/Score display;`

*   **[Milestone 5] URP 및 물리 설정**
    *   **내용:** 그래픽 파이프라인 최적화 및 레이어 충돌 설정.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Graphics/Physics]Configure URP Settings (Bloom, Post-processing);Define Physics Layers (Player, Enemy, Projectile) and Matrix;`

---

### **Phase 2: 시스템 확장 및 구조 고도화**

*   **[Milestone 6] 이벤트 브로커 시스템**
    *   **내용:** `Action<T>` 기반 중앙 이벤트 버스를 통한 디커플링.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Arch/EventBroker]Define static EventBroker.cs;Implement Actions: Action<int> OnPlayerHealthChanged, Action<int> OnEnemyKilled;Replace direct calls in Enemy.cs and Projectile.cs with EventBroker;`

*   **[Milestone 7] 오브젝트 풀링 매니저**
    *   **내용:** `UnityEngine.Pool`을 활용한 런타임 최적화.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Optimization/Pooling]Implement Singleton ObjectPoolManager.cs;Update EnemySpawner and TurretShooter to use pooling;`

*   **[Milestone 8] ScriptableObject 기반 데이터화**
    *   **내용:** 에셋 기반 데이터 관리 (Enemy, Weapon, Wave).
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Data/SO]Implement EnemyData.cs, WeaponData.cs, WaveData.cs;Update Enemy.cs to accept EnemyData for initialization;`

*   **[Milestone 9] 웨이브 매니저 구현**
    *   **내용:** 시간 경과에 따른 난이도 및 스폰 규칙 관리.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Wave]Implement WaveManager.cs;Handle Wave sequence using List<WaveData>;Update EnemySpawner to receive current WaveData;`

*   **[Milestone 10] 무기 시스템 아키텍처**
    *   **내용:** 인터페이스 기반 확장형 무기 시스템 설계.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Arch/Weapon]Define IWeapon interface (Fire(), Reload());Implement abstract WeaponBase class;Update TurretShooter.cs to hold current IWeapon;`

---

### **Phase 3: 로그라이트 성장 및 투사체 진화**

*   **[Milestone 11] 레벨업 시스템 및 UI**
    *   **내용:** 경험치 기반 성장 및 3개 무작위 강화 선택.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Progression]Implement LevelUpManager.cs;Manage EXP and 3 random upgrade choices;`

*   **[Milestone 12] 투사체 변이 물리 로직**
    *   **내용:** 다연발, 관통, 도탄, 폭발 등 물리적 속성 확장.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Combat/Evolution]Extend Projectile.cs with Multi-shot, Pierce, Ricochet, Explosive, Growth logic;Focus on physics engine stability;`

---

### **Phase 4: 콘텐츠 폴리싱 및 모바일 출시 준비**

*   **[Milestone 13] 보스전: 타이탄 코어**
    *   **내용:** 15분 시점 보스 페이즈 전환 및 패턴 구현.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Content/Boss]Implement BossPhase logic at 15:00;Handle entrance events and Titan Core patterns;`

*   **[Milestone 14] 오버클럭 모드 (무한)**
    *   **내용:** 보스 격파 후 기하급수적 난이도 상승 로직.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Infinite]Implement OverclockMode;Exponential stat scaling every 60s;`

*   **[Milestone 15] 특수 적 AI 구현**
    *   **내용:** 블리츠, 아머드, 스웜, 헬파이어 AI 고도화.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Enemy/Variety]Implement BlitzMissile(Zigzag), ArmoredMeteor(Tanker), SwarmPod(Split), Hellfire(Explode) logic;`

*   **[Milestone 16] 연출 및 최종 폴리싱**
    *   **내용:** 카메라 쉐이크, 피버 타임, 사운드 및 모바일 광고 연동.
    *   **AI Prompt:** `[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Polishing]Implement VisualEffectManager (ScreenShake);Add FeverTime audio pitch-shift;Integrate AdMob for Rewarded items;`

---

> [!TIP]
> Phase 5 이후의 서버 구축, PC/VR/콘솔 출시 등 향후 확장 계획은 **[08. 향후 로드맵](08_future_roadmap.md)** 문서에서 확인하실 수 있습니다.
