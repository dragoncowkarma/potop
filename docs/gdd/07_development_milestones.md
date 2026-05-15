## **7. 개발 마일스톤 (Development Milestones)**

### **Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP)**
> [!TIP]
> AI Prompts: [010_core_loop_mvp.md](../prompts/010_core_loop_mvp.md)

*   **[Milestone 1] 기본 포탑 회전 및 투사체 발사**
*   **[Milestone 2] 1인칭 마우스 룩**
*   **[Milestone 3] 적 스폰 및 기초 AI**
*   **[Milestone 4] GameManager 및 기본 HUD**
*   **[Milestone 5] URP 및 물리 설정**

---

### **Phase 1.5: 기술 부채 해결 및 기본 시스템 안정화**
> [!TIP]
> AI Prompts: [015_stability_refactoring.md](../prompts/015_stability_refactoring.md)

*   **[Milestone 6] 코드 스타일 및 파일 무결성 정비**
*   **[Milestone 7] 입력 시스템 고도화 및 회전 로직 통합**
*   **[Milestone 8] Unity 환경 설정 및 링크 검증**

---

### **Phase 2: 기초 아키텍처 및 데이터 시스템**
> [!TIP]
> AI Prompts: [020_architecture.md](../prompts/020_architecture.md)

*   **[Milestone 9] 이벤트 브로커 시스템**
*   **[Milestone 10] 오브젝트 풀링 매니저**
*   **[Milestone 11] ScriptableObject 기반 데이터화**

---

### **Phase 2.5: 기초 시스템 고도화 및 무기 모듈화 (Foundation & Modularization)**
> [!TIP]
> AI Prompts: 
> - 02501: [Jules] [02501_jules_p01_weapon_architecture.md](../prompts/025/02501_jules_p01_weapon_architecture.md)
> - 02502: [Jules] [02502_jules_p02_damage_api.md](../prompts/025/02502_jules_p02_damage_api.md), [02502_jules_p03_health_system.md](../prompts/025/02502_jules_p03_health_system.md)
> - 02502: [Jules] [02502_merge_health_integration_test.md](../prompts/025/02502_merge_health_integration_test.md)
> - 02503: [Antigravity] [02503_antigravity_combat_feedback.md](../prompts/025/02503_antigravity_combat_feedback.md)
> - 02504: [Antigravity] [02504_antigravity_camera_polish.md](../prompts/025/02504_antigravity_camera_polish.md)
> - QA: [Gemini CLI] [02599_gemini_validation.md](../prompts/025/02599_gemini_validation.md)

*   **[02501] 파츠 기반 무기 아키텍처 (Weapon Parts)**
*   **[02502] 통합 체력 및 피해 시스템 (Health & Damage System)**
*   **[02502] 포스트-머지 체력 및 데미지 통합 테스트 (Post-Merge Health Integration Test)**
*   **[02503] 컴뱃 피드백 아키텍처 (Combat Feedback Architecture)**
*   **[02504] 시네머신 및 카메라 로직 정비 (Cinemachine & Camera Control)**

---

### **Phase 3: 게임플레이 루프 및 AI 기초**
> [!TIP]
> AI Prompts: 
> - 03001: [Jules: Gemini 3.1 Pro] [03001_wave_management.md](../prompts/030/03001_wave_management.md)
> - 03002: [Jules: Gemini 3.1 Pro] [03002_enemy_variants.md](../prompts/030/03002_enemy_variants.md), [Antigravity: Gemini 3.1 Pro] [03002_enemy_visuals.md](../prompts/030/03002_enemy_visuals.md)
> - 03003: [Antigravity: Gemini 3.1 Pro] [03003_tactical_hazards.md](../prompts/030/03003_tactical_hazards.md)
> - 03004: [Jules: Gemini 3.1 Pro] [03004_fever_system.md](../prompts/030/03004_fever_system.md)
> - QA: [Gemini CLI: Gemini 3.1 Pro] [03099_validation.md](../prompts/030/03099_validation.md)

*   **[03001] 웨이브 매니저 구현**
*   **[03002] 특수 적 AI 및 비주얼 구현**
*   **[03003] 환경 상호작용 기믹 (Tactical Hazards)**
*   **[03004] 피버 타임 시스템**

---

### **Phase 3.5: 시스템 안정화 및 최적화 (System Refinement)** `[진행 중]`
> [!TIP]
> AI Prompts:
> - 03501: [Jules] [03501_jules_p01_enemy_ai.md](../prompts/035/03501_jules_p01_enemy_ai.md)
> - 03502: [Jules] [03502_jules_p02_fever_decouple.md](../prompts/035/03502_jules_p02_fever_decouple.md)
> - QA: [Gemini CLI] [03599_gemini_validation.md](../prompts/035/03599_gemini_validation.md)

*   **[03501] 연속 적 스폰 및 AI 최적화** — 상태 머신 기반 적 AI, 시간 분할 회전 로직
*   **[03502] 피버 모드 디커플링 및 고도화** — FeverManager 독립 모듈화
*   **[03599] 코어 시스템 성능 프로파일링** — PoolManager/WaveManager 메모리 안정성 검증

---

### **Phase 4: 로그라이트 기반 (Roguelite Foundation)** `[예상: 2주]`
> [!TIP]
> AI Prompts:
> - 04001: [Jules] [04001_jules_p01_turret_classes.md](../prompts/040/04001_jules_p01_turret_classes.md)
> - 04002: [Jules] [04002_jules_p02_exp_gem.md](../prompts/040/04002_jules_p02_exp_gem.md)
> - 04003: [Jules] [04003_jules_p03_xp_leveling.md](../prompts/040/04003_jules_p03_xp_leveling.md)
> - 04003b: [Antigravity] [04004_antigravity_p04_upgrade_ui.md](../prompts/040/04004_antigravity_p04_upgrade_ui.md)
> - QA: [Gemini CLI] [04099_gemini_validation.md](../prompts/040/04099_gemini_validation.md)

*   **[04001] 터렛 클래스 구현** — Guardian/Valkyrie/Juggernaut/Nova 4종, `WeaponBase` 상속체
*   **[04002] 경험치 보석 드랍/수집 시스템** — EXPGem 풀링 + 자력 흡수 로직
*   **[04003] XP/레벨업 시스템 + 업그레이드 선택 UI** — 3~4개 선택지 제시, 일시정지 레벨업 화면

---

### **Phase 4.5: 코어 루프 리팩토링 및 안정화 (Core Loop Refactor & Stabilization)** `[예상: 1주]`
> [!IMPORTANT]
> **Phase 4.5는 기술 부채 해결 및 코어 루프 통합을 위한 필수 마일스톤입니다.**
> [!TIP]
> AI Prompts:
> - 04501: [Jules] [04501_jules_p01_weapon_integration.md](../prompts/045/04501_jules_p01_weapon_integration.md)
> - 04502: [Jules] [04502_jules_p02_projectile_features.md](../prompts/045/04502_jules_p02_projectile_features.md)
> - 04503: [Jules] [04503_jules_p03_data_ui_cleanup.md](../prompts/045/04503_jules_p03_data_ui_cleanup.md)
> - 04504: [Antigravity] [04504_antigravity_p04_feedback_polish.md](../prompts/045/04504_antigravity_p04_feedback_polish.md)
> - QA: [Gemini CLI] [04599_gemini_validation.md](../prompts/045/04599_gemini_validation.md)

*   **[04501] 무기 아키텍처 통합 (Weapon Integration)** — `TurretShooter.cs`를 `WeaponBase` 및 `IFireStrategy` 기반으로 리팩토링
*   **[04502] 투사체 핵심 특성 구현 (Projectile Features)** — `NovaWeapon`의 광역 피해(AoE) 및 `JuggernautWeapon`의 넉백/관통 로직 추가
*   **[04503] 데이터 및 UI 기술 부채 해결 (Data & UI Cleanup)** — `EXPGemData` 분리, `UpgradeSelectController`에서 `FindFirstObjectByType` 제거
*   **[04504] 컴뱃 피드백 및 최적화 (Feedback & Polish)** — `CameraShakeController` 전투 연동, `VFXTrigger` 코루틴을 `PoolManager` 기반으로 최적화

---

### **Phase 5: 빌드 다양성 (Build Diversity)** `[예상: 2주]`
> [!TIP]
> AI Prompts:
> - 05001: [Jules] [05001_jules_p01_upgrade_table.md](../prompts/050/05001_jules_p01_upgrade_table.md)
> - 05002: [Jules] [05002_jules_p02_mutation_synergy.md](../prompts/050/05002_jules_p02_mutation_synergy.md)
> - 05003: [Jules] [05003_jules_p03_projectile_mutation.md](../prompts/050/05003_jules_p03_projectile_mutation.md)
> - 05004: [Jules] [05004_jules_overdrive_evolution.md](../prompts/050/05004_jules_overdrive_evolution.md)
> - QA: [Gemini CLI] [05099_gemini_validation.md](../prompts/050/05099_gemini_validation.md)

*   **[05001] 업그레이드 풀 & 등장 확률 테이블** — 레어리티별 가중치, 중복 방지 로직
*   **[05002] 무기 변이 & 시너지** — Pierce+Explosion, Multi+Bounce, Scale+Knockback
*   **[05003] 투사체 변이 물리 로직** — 관통/도탄/유도/거대화 개별 물리 처리
*   **[05004] 궁극 진화 시스템** — 오비탈 스트라이크/프리즘 체인/개틀링 레일건

---

### **Phase 6: 전술 시스템 & 메타 경제 (Tactics & Meta Economy)** `[예상: 2.5주]`
> [!TIP]
> AI Prompts:
> - 06001: [Jules] [06001_jules_p01_overcharge.md](../prompts/060/06001_jules_p01_overcharge.md)
> - 06002: [Jules] [06002_jules_p02_tactical_skills.md](../prompts/060/06002_jules_p02_tactical_skills.md)
> - 06003: [Jules] [06003_jules_p03_item_drop.md](../prompts/060/06003_jules_p03_item_drop.md)
> - 06004: [Jules+Antigravity] [06004_meta_upgrade_lobby.md](../prompts/060/06004_meta_upgrade_lobby.md)
> - QA: [Gemini CLI] [06099_gemini_validation.md](../prompts/060/06099_gemini_validation.md)

*   **[06001] 오버차지 시스템** — 공격 속도 200%↑, 게이지 과부하 시 3초 과열 패널티
*   **[06002] 액티브 전술 스킬 3종** — EMP(500E), 궤도 폭격(700E), 과부하 보호막(1000E)
*   **[06003] 아이템 드랍 시스템** — 수리키트(HP 30 회복), 초강력 자석, 스마트 폭탄
*   **[06004] 영구 강화 시스템 기초** — Gem 경제 + 기본 로비 UI + 6개 강화 항목

---

### **Phase 7: 엔드게임 & 보스전 (Endgame & Boss)** `[예상: 2.5주]`
> [!IMPORTANT]
> **Phase 7 완료 = Vertical Slice.** 이 시점에서 내부 플레이테스트를 실시하여 코어 루프를 검증합니다.
> [!TIP]
> AI Prompts:
> - 07001: [Antigravity] [07001_antigravity_boss_visual.md](../prompts/070/07001_antigravity_boss_visual.md)
> - 07002: [Jules] [07002_jules_boss_ai.md](../prompts/070/07002_jules_boss_ai.md)
> - 07003: [Jules] [07003_jules_overclock_gameflow.md](../prompts/070/07003_jules_overclock_gameflow.md)
> - QA: [Gemini CLI] [07099_gemini_vs_validation.md](../prompts/070/07099_gemini_vs_validation.md)

*   **[07001] 보스 프리팹 & 비주얼** — 타이탄 코어, 페이즈별 컬러 전환 (Blue→Red)
*   **[07002] 보스 AI 3페이즈** — ① 회전 쉴드 → ② 타겟 레이저 → ③ 광폭화
*   **[07003] 오버클럭 모드** — 무한 웨이브, 30초마다 적 스탯 1.5배 스케일링
*   **[07004] 게임 플로우 통합** — 15분→보스→오버클럭→게임오버→결산 전체 루프 연결
*   **[07005] 튜토리얼 프로토타입** — 핵심 조작 및 업그레이드 흐름 조기 검증을 위한 기초 가이드 시스템

---

### **Phase 8: 폴리싱 & 게임필 (Polish & Game Feel)** `[예상: 2주]`

*   **[08001] 컴뱃 쥬스 최종 정비** — 히트스탑, 슬로우모션, 화면 흔들림 강도 튜닝
*   **[08002] 사운드 시스템** — Adaptive BGM 5단계(Lo-fi→EDM) + SFX 통합
*   **[08003] VFX 최종 정비** — 적 사망 연출, 보스 페이즈 전환 연출, UI 전환 애니메이션
*   **[08004] 밸런스 미세 조정** — 4종 터렛 × 변이 조합 교차 테스트, 플레이테스트 기반 수치 보정

---

### **Phase 9: 모바일 출시 준비 (Mobile Launch)** `[예상: 3~4주]`

*   **[09001] 로비 UI 완성** — 터렛 선택/강화 상점/설정 화면
*   **[09002] 업적 시스템** — AC_001~AC_010 (10종 업적)
*   **[09003] 로컬 세이브 시스템** — JSON 파일 기반 영구 데이터 관리
*   **[09004] 모바일 입력 최적화** — 조이스틱/자이로/자동 사격
*   **[09005] 모바일 광고 연동** — 보상형 부활(1회 한정) + 전면 광고(3~5판 주기)
*   **[09006] 모바일 최적화** — 성능 프로파일링, 배터리/발열, LOD
*   **[09007] 튜토리얼 시스템** — 인터랙티브 가이드 (콘텐츠 freeze 후 작성)
*   **[09008] 다국어 지원** — KO/EN/JP 최소 3개 언어
*   **[09009] 스토어 등록 & 심사** — Google Play / App Store

---

### **Phase 10: 글로벌 서버 (Server Infrastructure)** `[예상: 4~6주]`

*   **[10001] 서버 인프라** — 호스팅, DB 설계, CI/CD 파이프라인
*   **[10002] RESTful API** — 인증/프로필/리더보드/데이터 동기화
*   **[10003] 점수 검증 & 치트 방지** — 서버 사이드 해시 검증
*   **[10004] 글로벌 리더보드 & 클라우드 세이브** — Top 100 + 개인 랭킹
*   **[10005] 부하 테스트 & 모니터링** — 스트레스 테스트, 알림 시스템

---

### **Phase 11: PC 출시 (Steam Release)** `[예상: 4주]`

*   **[11001] SteamWorks SDK 연동** — 업적/리더보드/클라우드 세이브
*   **[11002] PC 입력 최적화** — 마우스 감도 커브, 키 리매핑 UI
*   **[11003] 그래픽 옵션 UI** — 해상도/품질/VSync/프레임 제한
*   **[11004] PC 전용 UI** — 와이드스크린/울트라와이드 대응
*   **[11005] Steam 스토어 페이지 & 빌드** — 스토어 에셋, 빌드 파이프라인

---

### **Phase 12: VR 이식 (VR Port)** `[예상: 6~8주]`

*   **[12001] OpenXR 추상화** — SteamVR / Meta Quest 호환 레이어
*   **[12002] VR 입력 시스템** — 시선 조준 + 모션 컨트롤러 트리거
*   **[12003] 월드 스페이스 UI 재설계** — VR 전용 인터랙티브 UI
*   **[12004] 공간 음향** — HRTF 기반 Spatial Audio
*   **[12005] 컴포트 설정** — 비네팅/스냅턴/텔레포트 옵션
*   **[12006] VR 전용 최적화** — 72/90fps 타겟, Foveated Rendering

---

### **Phase 13: 콘솔 이식 (Console Port)** `[예상: 6~8주]`

*   **[13001] DevKit 대응** — Nintendo Switch / PS5
*   **[13002] 컨트롤러 매핑 & 햅틱** — DualSense 적응형 트리거, HD Rumble
*   **[13003] 플랫폼 가이드라인 준수** — TRC/Lotcheck/XR 심사 대응
*   **[13004] 글로벌 동시 출시 & 라이브 서비스** — 패치 파이프라인, 커뮤니티 관리
