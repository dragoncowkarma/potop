## **8. 개발 마일스톤 및 AI 작업 프롬프트 (Timeline & AI Prompts) - POTOP**

※ **[Agent Roles]**
- **Jules (J):** 시스템 아키텍처, 핵심 로직, UI, 데이터 스키마, 백엔드 구현 전문.
- **Antigravity (A):** 유니티 물리 엔진 최적화, VFX 연출, 성능 QA, 버그 수정, 그래픽 파이프라인 설정 전문.

---

### **Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP) ✅ 완료**

#### **[Milestone 1] (J) 기본 포탑 회전 및 투사체 발사**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:CoreCombat]Implement Turret rotation logic based on Input system;Create TurretShooter.cs for instantiating Projectile.prefab at FirePoint;Handle simple forward velocity in Projectile.cs;

#### **[Milestone 2] (J) 1인칭 마우스 룩**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:PlayerController]Implement FirstPersonLook.cs;Clamp vertical rotation between -90 and 90 degrees;Integrate with Player.cs for input enabling/disabling;

#### **[Milestone 3] (J) 적 스폰 및 AI**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:EnemySystem]Implement EnemySpawner.cs using 360-degree radial spawning (Radius:25m, Height:1-5m);Create Enemy.cs with basic LookAt and MoveTowards(Player) logic;

#### **[Milestone 4] (J) GameManager 시스템**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:GameManagement]Implement Singleton GameManager.cs;Manage Health(int), Score(int), GameState(Enum:Start,Playing,GameOver);

#### **[Milestone 5] (J) HUD UI 및 시작 화면**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:UI]Implement GameHUD.cs for HP/Score display;Create StartMain.cs for menu navigation;

#### **[Milestone 6] (A) URP 및 물리 레이어 설정**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Graphics/Physics]Configure URP Settings (Bloom, Post-processing);Define Physics Layers (Player, Enemy, Projectile) and Matrix;

#### **[Milestone 7] (A) 초기 구현 디버깅 및 입력 로직 검증**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:QA/BugFix]Review Player.cs for input leaks during GameOver;Validate rotation clamping bugs;

---

### **Phase 2: 시스템 확장 및 구조 고도화 (진행 중)**

#### **[Milestone 8] (J) 이벤트 브로커 - 시스템 구축**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Arch/EventBroker]Define static EventBroker.cs;Implement Actions: Action<int> OnPlayerHealthChanged, Action<int> OnEnemyKilled, Action<GameState> OnStateChanged;

#### **[Milestone 9] (J) 이벤트 브로커 - 리팩토링 적용**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Refactoring]Decouple Enemy.cs and Projectile.cs from GameManager;Replace direct calls with EventBroker;

#### **[Milestone 10] (J) 오브젝트 풀 매니저 - 코어**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Optimization/Pooling]Implement Singleton ObjectPoolManager.cs using UnityEngine.Pool.ObjectPool;Support generic T where T : Component;

#### **[Milestone 11] (J) 오브젝트 풀링 - 구현 적용**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:PoolingImpl]Update EnemySpawner and TurretShooter to use ObjectPoolManager;Implement IPoolable interface;

#### **[Milestone 12] (J) ScriptableObject - EnemyData**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Data/SO]Implement EnemyData.cs (HP, Speed, Damage, Score, Prefab);

#### **[Milestone 13] (J) ScriptableObject - WeaponData**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Data/SO]Implement WeaponData.cs (FireRate, Damage, Speed, Level, Icon);

#### **[Milestone 14] (J) ScriptableObject - WaveData**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Data/SO]Implement WaveData.cs (Duration, SpawnList, Interval);

#### **[Milestone 15] (J) WaveManager - 핵심 로직**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Wave]Implement WaveManager.cs;Handle Wave sequence using List<WaveData>;

#### **[Milestone 16] (J) WaveManager - Spawner 연동**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Integration]Update EnemySpawner to receive current WaveData;

#### **[Milestone 17] (J) 적 다양화 - 데이터 통합**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Content]Update Enemy.cs to accept EnemyData for initialization;

#### **[Milestone 18] (J) 무기 시스템 - 아키텍처 설계**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Arch/Weapon]Define IWeapon interface (Fire(), Reload());Implement abstract WeaponBase class;

#### **[Milestone 19] (J) 무기 시스템 - 사격 로직 리팩토링**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Refactoring]Update TurretShooter.cs to hold current IWeapon;

#### **[Milestone 20] (J) 신규 무기 - 샷건(Shotgun)**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Content/Weapon]Implement Shotgun.cs inheriting WeaponBase;Fire spread pattern;

---

### **Phase 3: 로그라이트 성장 및 투사체 진화 (예정)**

#### **[Milestone 21] (J) LevelUpManager 및 UI**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Progression]Implement LevelUpManager.cs;Manage EXP and 3 random upgrade choices;

#### **[Milestone 22] (A) 투사체 진화 - 물리 로직 구현**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Combat/Evolution]Extend Projectile.cs with Multi-shot, Pierce, Ricochet, Explosive, Growth logic;Focus on physics engine stability;

#### **[Milestone 23] (A) 투사체 진화 - 성능 최적화 QA**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Performance/QA]Validate physics overhead for high projectile counts;Optimize collision frequency;

---

### **Phase 4: 콘텐츠 폴리싱 및 기믹 대응**

#### **[Milestone 24] (J) 보스전 - 타임라인 및 전환**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Content/Boss]Implement BossPhase logic at 15:00;Handle entrance events;

#### **[Milestone 25] (J) 무한 모드 - 오버클럭 로직**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Infinite]Implement OverclockMode;Exponential stat scaling every 60s;

#### **[Milestone 26] (J) 특수 적 - 블리츠 미사일**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Enemy/Blitz]Implement BlitzMissile AI (Fast, Zigzag);

#### **[Milestone 27] (J) 특수 적 - 아머드 메테오**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Enemy/Armored]Implement ArmoredMeteor (Tanker, Knock-back immunity);

#### **[Milestone 28] (J) 특수 적 - 스웜 포드**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Enemy/Swarm]Implement SwarmPod (Splits into Micro-Bots on death);

#### **[Milestone 29] (J) 특수 적 - 헬파이어**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Enemy/Hellfire]Implement Hellfire (Suicide Bomber delay logic);

---

### **Phase 5: 메타 시스템 및 시스템 안정화**

#### **[Milestone 30] (J) 도전 과제 시스템**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:System/Achievement]Implement AchievementManager (Observer pattern);

#### **[Milestone 31] (J) 영구 강화 상점 UI**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:UI/Shop]Implement LobbyShop for stat upgrades;

#### **[Milestone 32] (J) 데이터 세이브 및 로드**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Data/Persistence]Implement JSON SaveSystem via persistentDataPath;

#### **[Milestone 33] (A) 세이브 데이터 무결성 검증 툴**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Utility/QA]Create editor tool to validate/fix corrupted SaveData.json;

---

### **Phase 6: 출시 준비 및 플랫폼 최적화**

#### **[Milestone 34] (A) 연출 - 카메라 쉐이크 및 VFX**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:VFX]Implement VisualEffectManager;Add ScreenShake and Particle pooling;

#### **[Milestone 35] (A) 연출 - 피버 타임 및 오디오**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Audio/VFX]Implement FeverTime audio pitch-shift and Neon Trails;

#### **[Milestone 36] (J) 수익화 - 광고 SDK 연동**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Monetization]Integrate AdMob/Unity Ads for Rewarded items;

#### **[Milestone 37] (A) 플랫폼 타겟 최적화 (Mobile -> PC -> VR -> Console)**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Optimization]Adjust quality settings and LODs for target platforms;

---

### **Phase 7: 백엔드 및 글로벌 확장**

#### **[Milestone 38] (J) 글로벌 리더보드 (Firebase)**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Backend/Leaderboard]Integrate Firebase Realtime DB for high scores;

#### **[Milestone 39] (J) 클라우드 세이브**
- **AI Prompt:** [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Backend/CloudSave]Implement Firebase Auth & Firestore sync;
