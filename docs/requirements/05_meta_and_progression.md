## **5. 메타 시스템 및 성장 (Meta & Progression)**

### **💰 비즈니스 모델 (Monetization - IAA + IAP Hybrid)**
POTOP은 캐주얼 로그라이트 장르의 지속 가능성을 위해 **IAA(광고)와 IAP(인앱 결제)가 결합된 하이브리드 모델**을 채택합니다.

#### **1. 광고 기반 모델 (IAA)**
* **보상형 광고 (Rewarded Ads):**
  * **부활:** 게임 오버 시 1회 한정 풀 체력으로 부활.
  * **보상 배수:** 플레이 종료 후 획득한 골드(Gem)를 2배로 획득.
  * **무료 재화:** 상점에서 소량의 보석(Gem) 매일 1회 획득.
* **전면 광고 (Interstitial Ads):** 3~5판 플레이 후 로비로 복귀할 때 노출 (광고 제거 상품 구매 시 제거).

#### **2. 인앱 결제 모델 (IAP)**
* **재화 팩:** 보석(Gem) 번들 (Small/Medium/Large).
* **광고 제거 (No Ads):** 전면 광고 영구 제거 + 매일 소량의 보석 지급.
* **초보자 패키지 (Starter Pack):** 광고 제거 + 5,000 Gem + 발키리 터렛 즉시 해금.
* **시즌 패스 (Season Pass):** 프리미엄 트랙 활성화 시 특수 코스메틱 및 대량의 재화 획득.

#### **3. 코스메틱 (Cosmetics)**
* **터렛 스킨:** 공격 이펙트 색상 변경 (네온 블루, 핑크, 골드 등).
* **투사체 트레일:** 투사체 뒤에 남는 잔상 효과 커스터마이징.

---

### **🏛️ 영구 강화 시스템 (Meta Progression)**
게임 플레이 중 획득한 보석(Gem)을 사용하여 터렛의 기본 성능을 영구적으로 강화합니다.

| 강화 항목 | 효과 | 레벨당 비용 (예시) |
| :--- | :--- | :--- |
| **강화 외장** | 시작 HP +10 | 500 / 1,000 / 2,000... |
| **고속 모터** | 회전 속도 +10% | 300 / 600 / 1,200... |
| **에너지 집적** | 스킬 충전 효율 +5% | 700 / 1,400 / 2,800... |
| **자력 코어** | 보석 흡수 반경 +10% | 400 / 800 / 1,600... |
| **냉각 시스템** | 오버차지 지속시간 +15% | 600 / 1,200 / 2,400... |
| **정밀 스캐너** | 하저드 오브젝트 출현율 +5% | 500 / 1,000 / 2,000... |

> **[Task 6.4 — Implemented]** The meta upgrade system is implemented via:
> - `Assets/Scripts/Gameplay/Meta/GemWallet.cs` — PlayerPrefs-backed gem balance singleton.
> - `Assets/Scripts/Gameplay/Meta/MetaUpgradeData.cs` — ScriptableObject per upgrade type.
> - `Assets/Scripts/Gameplay/Meta/MetaUpgradeManager.cs` — Manages 6 upgrade slots, `TryPurchase()`, `GetStatBundle()`.
> - `Assets/Scripts/Gameplay/Meta/MetaEvents.cs` — `GemChangedEvent`, `MetaUpgradePurchasedEvent`.
> - `Assets/UI/Lobby/LobbyScreen.uxml / .uss` — Lobby screen root layout.
> - `Assets/UI/Lobby/MetaUpgradeCard.uxml` — Per-card template.
> - `Assets/Scripts/UI/LobbyController.cs` — UI ↔ logic binding via EventBroker.


---

### **🏆 도전 과제 및 업적 (Achievements)**
특정 조건을 달성하면 보상(Gem) 및 특수 효과가 해금됩니다.

| ID | 업적 명칭 | 달성 조건 | 보상 (Gem) | 비고 |
| :--- | :--- | :--- | :--- | :--- |
| **AC_001** | **첫 걸음** | 적 100마리 처치 | 100 | 기본 업적 |
| **AC_002** | **생존 전문가** | 한 판에서 10분 생존 | 500 | |
| **AC_003** | **네온 마스터** | 누적 점수 1,000,000점 도달 | 2,000 | |
| **AC_004** | **불사신** | 체력 10% 이하로 보스 격파 | 1,500 | |
| **AC_005** | **완벽한 수비** | 데미지를 입지 않고 5분 생존 | 1,000 | |
| **AC_006** | **한계 돌파** | 오버클럭 모드 진입 | 800 | |
| **AC_007** | **자력광** | 한 판에서 보석 5,000개 흡수 | 1,200 | |
| **AC_008** | **학살자** | 1초 내에 적 20마리 동시 처치 | 700 | |
| **AC_009** | **자산가** | 누적 50,000 보석 획득 | 3,000 | |
| **AC_010** | **포탑의 신** | 모든 영구 강화 항목 Max 레벨 달성 | 10,000 | 최종 업적 |

---

### **📅 미션 및 시즌 패스 (Missions & Season Pass)**

#### **1. 일일/주간 미션 (Retention)**
* **일일 미션:** 
  * "오늘 3판 플레이", "적 500마리 처치", "오버차지 5회 사용".
  * 보상: Gem + 시즌 패스 경험치.
* **주간 미션:**
  * "누적 15분 생존 5회", "보스 3회 격파", "모든 터렛 클래스로 플레이".
  * 보상: 대량의 Gem + 전용 코스메틱 토큰.

#### **2. 시즌 패스 (Season Pass)**
* **구조:** 총 50단계로 구성된 보상 트랙.
* **진행:** 미션 완료 및 게임 플레이를 통해 패스 레벨업.
* **보상:** 
  * **Free:** 기본 재화, 일반 스킨.
  * **Premium:** 유니크 스킨, 한정판 이펙트, 재화 가중치 버프.

---

### **🔄 전체 게임 플로우 (Game Flow)**

```mermaid
flowchart TD  
    A([게임 실행]) --> B{최초 진입?}  
    B -- Yes --> TUT[튜토리얼]
    B -- No --> LOBBY[메인 로비]
    
    TUT --> LOBBY
    LOBBY --> SETUP[터렛 클래스 선택]
    SETUP --> WAVE[15분 웨이브 시작]
    
    WAVE --> COMBAT[전투 및 경험치 보석 획득]
    COMBAT --> LVUP{레벨업?}
    LVUP -- Yes --> SELECT[능력 선택 및 진화]
    SELECT --> COMBAT
    
    WAVE --> TIME{15분 도달?}
    TIME -- Yes --> BOSS[최종 보스전]
    BOSS --> WIN{보스 격파?}
    
    WIN -- Yes --> OVERCLOCK[오버클럭 모드 - 무한 웨이브]
    OVERCLOCK --> SCORE[최고 점수 갱신 및 생존]
    SCORE --> GAMEOVER[게임 오버]
    
    WIN -- No (HP 0) --> GAMEOVER
    COMBAT -- HP 0 --> GAMEOVER
    
    GAMEOVER --> RANK[글로벌 리더보드 등록]
    RANK --> UPGRADE[영구 업그레이드 및 해금]
    UPGRADE --> LOBBY
```
