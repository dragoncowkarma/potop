# 📋 POTOP GDD 최종 확정안 — 변경 델타 문서

> **기준:** `docs/gdd/01~08` 원본 대비 변경사항만 기술
> **검토:** 수석 기획자 + 3인 전문가 패널 × 3회 반복 검토 완료

---

## 01_overview.md 변경사항

### 🔄 장르 재분류
```diff
- * **장르:** 로그라이트(Roguelite) + 360도 웨이브 슈터(Wave Shooter)
+ * **장르:** 캐주얼 로그라이트(Casual Roguelite) + 360도 웨이브 슈터(Wave Shooter)
```
**근거:** 하이퍼캐주얼은 30초 이내 학습 곡선의 단순 게임. POTOP은 변이 시너지, 빌드 다양성, 메타 성장 등 깊은 시스템을 보유 → Archero/Survivor.io급 "캐주얼 로그라이트"가 정확함.

### 🔄 타겟 고객 수정
```diff
- * **메인 타겟:** ... **하이퍼캐주얼 게이머 (10~30대)**.
+ * **메인 타겟:** 출퇴근·휴식 시간에 짧고 강렬한 플레이를 원하는 **캐주얼 로그라이트 게이머 (15~35세)**.
+ * **코어 타겟:** 로그라이트 빌드 최적화와 리더보드 경쟁을 즐기는 **미드코어 게이머**.
```

---

## 02_gameplay_mechanics.md 변경사항

### ➕ 위협 인디케이터 시스템 (신규 섹션)
```markdown
### **🔺 위협 인디케이터 (Threat Indicator)**
* **관련 마일스톤:** `[Milestone 4, 08001]`
* 화면 가장자리에 적의 접근 방향과 거리를 색상 코딩으로 표시.
  * **Yellow:** 적 감지 (15m 이내)
  * **Orange:** 접근 중 (8m 이내)
  * **Red + Pulse:** 임박 (3m 이내)
* VR에서는 공간 음향(Spatial Audio)으로 대체.
* 모바일/PC에서는 화면 가장자리 반원형 아이콘 표시.
```

### 🔄 노바 터렛 밸런스 조정
```diff
- 4. **노바 (Nova):** 범위형. 에너지 구체 (공격력 15, 공속 1.0s, 적중 시 0.5m 폭발).
+ 4. **노바 (Nova):** 범위형. 에너지 구체 (공격력 12, 공속 1.0s, 적중 시 1.5m 폭발, 히트 수 무제한).
```
**근거:** 단일 DPS 15→12로 낮추되, AoE 반경 3배 확대. 밀집 구간에서 3~5체 동시 히트 시 실질 DPS 36~60으로 역할 차별화.

### 🔄 레벨업 흐름 변경
```diff
- 3. **Upgrade (강화)**: 무작위로 제시되는 3~4개의 업그레이드 중 하나를 선택하여 빌드업.
+ 3. **Upgrade (강화)**: 
+    * **Lv.1~5:** 패시브 자동 적용 (토스트 알림만, 게임 중단 없음)
+    * **Lv.6 이후:** 3레벨마다 선택형 강화 (Lv.6, 9, 12, 15, 18...)
+    * **선택 UI:** 슬로우모션(0.1x) + 5초 타이머 (미선택 시 랜덤 적용)
+    * **Lv.16+:** 기존 강화의 중복 스택 허용 (무한 성장)
```
**근거:** 15분 내 레벨업 20+회 → 매번 게임 중단은 몰입감 파괴. 선택 횟수를 ~7회로 축소하고 슬로우모션으로 흐름 유지.

### ➕ 피버 + 오버차지 상호작용 (신규)
```markdown
#### **피버 × 오버차지 시너지**
* 피버와 오버차지는 **독립적으로 동시 적용**됩니다.
* **추가 보너스:** 피버 Lv.2+ 상태에서 오버차지 게이지 소모 속도 **20% 감소**.
* **과열(Overheat) 시:** 피버 콤보 카운터는 유지됨 (과열로 콤보 초기화 없음).
```

### 🔄 HUD 요소 우선순위 (신규)
```markdown
#### **HUD 티어 분류**
* **Tier 1 (항상 표시):** HP 바, 콤보 카운터, 위협 인디케이터
* **Tier 2 (상황별):** 에너지 게이지 (50%+ 시), 오버차지 게이지 (사용 중), 피버 레벨 아이콘
* **Tier 3 (팝업):** XP 바 (획득 시 2초), 레벨업 알림, 아이템 획득
```

---

## 03_data_and_balance.md 변경사항

### 🔄 노바 데이터 수정
```diff
- | **노바** | 15 | 1.0 | 0 | 0.5m | 범위 공격, 탄속 12m/s |
+ | **노바** | 12 | 1.0 | 0 | 1.5m | 범위 공격, 탄속 12m/s, 히트 수 무제한 |
```

### ➕ 스웜 포드 / 마이크로 봇 / 보스 데이터 (신규)
```markdown
| 적 유형 | HP | 데미지 | EXP | 속도 | 비고 |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **스웜 포드** | 25 | 15 | 20 | 2.0 | 파괴 시 마이크로 봇 3~5개 생성 |
| **마이크로 봇** | 3 | 5 | 3 | 5.0 | 스웜 포드 파편 |
| **헬파이어** | 30 | 40 (자폭) | 50 | 2.5 | **에너지 +30** |

#### **🏆 타이탄 코어 (보스) 상세 데이터**
| 항목 | 수치 |
| :--- | :--- |
| **총 HP** | 5,000 |
| **Phase 1 (100%~60% HP)** | 회전 쉴드 — 전방 120도 면역, DMG 20/히트 |
| **Phase 2 (60%~25% HP)** | 타겟 레이저 — 2초 조준 후 직선 DMG 60, 1초 쿨다운 |
| **Phase 3 (25%~0% HP)** | 광폭화 — 공속 2배, 이동 속도 1.5배, 소환 스카우터 5마리/10초 |
```

### 🔄 EXP 커브 재설계
```diff
  기존: Lv.10→20은 "600~2000, +100~"으로 모호하게 표기
+ 
+ #### **EXP 커브 (지수적 증가)**
+ | 레벨 | 필요 EXP (누적) | 비고 |
+ | :--- | :--- | :--- |
+ | 1→2 | 10 | 튜토리얼 |
+ | 2→3 | 30 | |
+ | 3→4 | 60 | |
+ | 4→5 | 100 | 첫 패시브 완성 |
+ | 5→6 | 160 | **첫 선택형 강화** |
+ | 6→9 | 250/380/560 | |
+ | 9→12 | 800/1,100/1,500 | 중반 빌드 구간 |
+ | 12→15 | 2,000/2,700/3,600 | 시너지 완성 구간 |
+ | 15→18 | 4,800/6,300/8,200 | 궁극 진화 구간 |
+ | 18+ | 이전 레벨 ×1.3 | 무한 스택킹 구간 |
```

### 🔄 오버클럭 점수 배율 변경
```diff
- * **점수 배율:** 1분 생존 시마다 ... **+0.1x**씩 추가 가산.
+ * **점수 배율:** 30초 생존 시마다 **+0.2x** 추가 가산. (난이도와 보상 모두 기하급수적)
```

### ➕ 동시 활성 적 제한 (신규)
```markdown
#### **4. 성능 제약 상수 (Performance Constraints)**
* **Max Active Enemies:** 200 (상한 도달 시 가장 먼 적부터 강제 디스폰)
* **Max Active Projectiles:** 500
* **Max Active VFX Particles:** 10,000
```

---

## 04_technical_architecture.md 변경사항

### ➕ 입력 추상화 레이어 (신규 섹션)
```markdown
#### **4. 입력 추상화 레이어 (Input Abstraction Layer)**
* **기반:** Unity New Input System (`InputAction` Asset)
* **구조:** `IInputProvider` 인터페이스 → 플랫폼별 구현체
  * `MobileInputProvider`: 조이스틱 + 자이로 + 자동사격
  * `PCInputProvider`: 마우스 FPS 룩 + 키보드
  * `VRInputProvider`: HMD 시선 + 컨트롤러 트리거
* **런타임 전환:** `InputManager.SetProvider(IInputProvider)` — 핫스왑 지원
```

### ➕ EventBroker 카테고리 분리 (수정)
```markdown
#### **1. 이벤트 브로커 (Event Broker) — 개선**
* **구조:** 단일 `EventBroker.cs` → 카테고리별 분리
  * `CombatEvents`: OnEnemyKilled, OnDamageDealt, OnPlayerHit
  * `ProgressionEvents`: OnLevelUp, OnUpgradeSelected, OnFeverChanged
  * `UIEvents`: OnHUDUpdate, OnScoreChanged
  * `MetaEvents`: OnGameStateChanged, OnGemCollected
```

### ➕ 렌더링 최적화 명세 (추가)
```markdown
* **Rendering:**
  * **GPU Instancing:** 동일 메시/머티리얼 적(Scouter 등)에 대해 SRP Batcher + GPU Instancing 적용.
  * **LOD:** 카메라 거리 기반 2단계 LOD (Full / Billboard).
  * **Culling:** 시야각 밖 적은 렌더링 스킵 (AI 로직은 유지).
```

### 🔄 점수 검증 강화
```diff
- "hash": "VERIFICATION_HASH"
+ "hash": "VERIFICATION_HASH",
+ "eventLog": {
+   "totalKills": 1523,
+   "levelReached": 18,
+   "upgradesChosen": ["pierce_lv2", "explosion_lv1", ...],
+   "bossDefeated": true,
+   "overclockSurvivalSec": 120
+ }
```

---

## 05_meta_and_progression.md 변경사항

### 🔄 비즈니스 모델 확장 (IAA → IAA + IAP 하이브리드)
```markdown
### **💰 비즈니스 모델 (Monetization) — 하이브리드**

#### **IAA (광고 기반)** — 기존 유지
* 보상형 부활 (1회/판), 보상 배수 (2x Gem), 시작 버프
* 전면 광고: 3~5판마다 로비 복귀 시

#### **IAP (인앱 구매)** — 신규
| 상품 | 가격대 | 내용 |
| :--- | :--- | :--- |
| **Gem 패키지 (소)** | $0.99 | 500 Gem |
| **Gem 패키지 (대)** | $4.99 | 3,000 Gem + 보너스 500 |
| **스타터 팩** | $2.99 (1회) | 2,000 Gem + 터렛 스킨 1종 + 광고 제거 3일 |
| **시즌 패스** | $4.99/30일 | 일일 보상(200 Gem) + 독점 코스메틱 + 광고 제거 |

#### **코스메틱 (Cosmetics)** — 신규
* 터렛 스킨 (색상/이펙트 변경, 스탯 영향 없음)
* 투사체 트레일 (시각 효과만)
* 적 사망 연출 이펙트 (커스텀 파편 색상)
```

### ➕ 일일/주간 미션 시스템 (신규)
```markdown
### **📅 일일/주간 미션 (Daily & Weekly Missions)**

#### **일일 미션 (3개/일, 리셋: 00:00 UTC)**
| 예시 | 보상 |
| :--- | :--- |
| 적 300마리 처치 | 100 Gem |
| 10분 이상 생존 | 150 Gem |
| 피버 Lv.2 도달 | 100 Gem |
| **전체 완료 보너스** | +200 Gem |

#### **주간 미션 (5개/주)**
| 예시 | 보상 |
| :--- | :--- |
| 발키리로 보스 격파 | 500 Gem |
| 관통 빌드로 적 1,000마리 처치 | 800 Gem |
| **전체 완료 보너스** | +1,000 Gem + 코스메틱 상자 |
```

---

## 06_art_and_sound.md 변경사항

### ➕ 에셋 네이밍 컨벤션 (신규)
```markdown
### **📁 에셋 네이밍 컨벤션 (Asset Naming)**
| 카테고리 | 형식 | 예시 |
| :--- | :--- | :--- |
| **Prefab** | `PFB_{Category}_{Name}` | `PFB_Enemy_Scouter` |
| **Material** | `MAT_{Category}_{Name}` | `MAT_Turret_Guardian` |
| **Texture** | `TEX_{Name}_{Type}` | `TEX_Scouter_Emission` |
| **VFX** | `VFX_{Category}_{Name}` | `VFX_Hit_Explosion` |
| **SFX** | `SFX_{Category}_{Name}` | `SFX_Weapon_Railgun` |
| **Animation** | `ANIM_{Object}_{Action}` | `ANIM_Boss_PhaseShift` |
| **ScriptableObject** | `SO_{Type}_{Name}` | `SO_Enemy_Scouter` |
```

---

## 07_development_milestones.md 변경사항

### 🔄 Phase 7에 튜토리얼 기초 추가
```diff
  ### Phase 7: 엔드게임 & 보스전 — **Vertical Slice**
  ...
+ *   **[07005] 튜토리얼 프로토타입** — 기본 조작 가이드 (3단계), VS 플레이테스트 피드백 수집용
```
**근거:** Phase 9까지 튜토리얼 없이 진행하면 Vertical Slice 플레이테스트에서 유의미한 피드백 확보 불가.

---

## 08_wbs.md 변경사항

### Phase 7 WBS에 튜토리얼 작업 추가
```markdown
| 7.5 | [07005] 튜토리얼 프로토타입 | ① 3단계 인터랙티브 가이드 (회전→사격→강화) ② 스킵 가능 플래그 ③ 첫 실행 감지 | **A** | → 7.4 | 튜토리얼 씬, `TutorialController.cs` |
```
