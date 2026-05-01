## **4. 기술 아키텍처 (Technical Architecture)**

### **🏗️ 개발 원칙 (Architecture Vision)**
POTOP은 대규모 물량(적, 투사체)을 효율적으로 처리하기 위해 **디플링(Decoupling)**과 **성능 최적화**를 핵심 설계 원칙으로 삼습니다.

---

### **🧩 주요 시스템 설계**

#### **1. 이벤트 브로커 (Event Broker)**
* **방식:** C# `Action<T>` 기반의 중앙 집중형 이벤트 버스.
* **주요 이벤트:**
  * `OnPlayerHealthChanged(int currentHP)`: 플레이어 체력 변경 시 UI 업데이트.
  * `OnEnemyKilled(EnemyData data)`: 적 처치 시 점수 합산 및 에너지 충전.
  * `OnGameStateChanged(GameState newState)`: 게임 시작/종료 처리.
* **이점:** 객체 간 직접 참조를 제거하여 코드의 독립성과 유지보수성 확보.

#### **2. 오브젝트 풀링 (Object Pooling)**
* **대상:** `Projectile`, `Enemy`, `EXPGem`, `VFX`.
* **도구:** Unity 6 내장 `UnityEngine.Pool` API 활용.
* **목표:** 런타임 중 가비지 컬렉션(GC) 발생을 최소화하여 프레임 드랍 방지.

#### **3. 데이터 주도 설계 (ScriptableObject)**
모든 게임 데이터를 코드가 아닌 `ScriptableObject` 에셋에서 관리합니다.
* `TurretData`: 공격력, 공속, 투사체 속도 등.
* `EnemyData`: HP, 속도, 경험치, 스폰 가중치.
* `WaveData`: 웨이브 지속 시간, 스폰 목록, 주기.

---

### **🛠️ 기술 스택 및 최적화**
* **Engine:** Unity 6 (6000.x)
* **Render Pipeline:** Universal Render Pipeline (URP)
* **UI System:** UI Toolkit (성능 및 스타일시트 관리 이점)
* **최적화 기법:**
  * **SIMD/Jobs:** 수백 개의 적이 플레이어를 향해 이동하는 연산에 최적화 검토.
  * **Physics Layers:** 레이어 기반 충전 행렬 최적화로 불필요한 물리 연산 제거.

---

### **☁️ 서버 통신 인터페이스 (Server Communication)**

POTOP은 글로벌 리더보드 및 유저 데이터 영구 보존을 위해 RESTful API를 활용합니다. 백엔드 구현체(Django/NestJS)와 관계없이 아래 인터페이스 규격을 준수합니다.

#### **1. 인증 및 프로필 (Auth & Profile)**
*   **POST** `/api/v1/auth/guest`
    *   **Description:** 게스트 계정 생성 및 토큰 발급.
    *   **Request:** `{ "deviceId": "UUID-STRING" }`
    *   **Response:** `{ "token": "JWT_TOKEN", "userId": "ID_STRING" }`
*   **GET** `/api/v1/user/profile`
    *   **Description:** 유저의 영구 강화 상태 및 보유 재화 조회.
    *   **Response:**
        ```json
        {
          "gems": 5400,
          "upgrades": {
            "armor": 3,
            "motor": 5,
            "energy": 2,
            "magnet": 4
          },
          "achievements": ["AC_001", "AC_002"]
        }
        ```

#### **2. 리더보드 (Leaderboard)**
*   **POST** `/api/v1/leaderboard/submit`
    *   **Description:** 게임 종료 후 최고 점수 및 로그 제출.
    *   **Request:**
        ```json
        {
          "score": 1250000,
          "playTime": 960,
          "version": "1.0.2",
          "hash": "VERIFICATION_HASH"
        }
        ```
*   **GET** `/api/v1/leaderboard/global`
    *   **Description:** 글로벌 랭킹 Top 100 조회.
    *   **Response:**
        ```json
        {
          "rankings": [
            { "rank": 1, "name": "PlayerA", "score": 5000000 },
            { "rank": 2, "name": "PlayerB", "score": 4800000 }
          ],
          "myRank": { "rank": 152, "score": 1250000 }
        }
        ```

#### **3. 데이터 동기화 (Data Sync)**
*   **PUT** `/api/v1/user/upgrades`
    *   **Description:** 로비에서 구매한 영구 강화 상태 저장.
    *   **Request:** `{ "upgradeId": "motor", "level": 6, "cost": 1200 }`
