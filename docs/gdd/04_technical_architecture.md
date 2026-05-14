## **4. 기술 아키텍처 (Technical Architecture)**

### **🏗️ 개발 원칙 (Architecture Vision)**
POTOP은 대규모 물량(적, 투사체)을 효율적으로 처리하기 위해 **디플링(Decoupling)**과 **성능 최적화**를 핵심 설계 원칙으로 삼습니다. 모든 코드는 `docs/AGENTS.md`의 기술 지침을 준수합니다.

---

### **📁 폴더 구조 및 네임스페이스 (Folder Structure & Namespaces)**

| 레이어 | 폴더 경로 (`Assets/Scripts/`) | 네임스페이스 (`Potop.Client.*`) | 담당 에이전트 |
| :--- | :--- | :--- | :--- |
| **Core** | `/Core` | `.Core` | **Jules** |
| **Gameplay** | `/Gameplay` | `.Gameplay` | **Jules** |
| **UI** | `/UI` | `.UI` | **Antigravity** |
| **Data** | `/Data` | `.Data` | **Jules** |
| **VFX/Art** | `/VFX` | `.VFX` | **Antigravity** |

---

### **🧩 주요 시스템 설계 (Core Systems Design)**

#### **1. 이벤트 브로커 (Event Broker - Category Based)**
* **방식:** 관심사 분리를 위해 카테고리별 정적 클래스 또는 서브 시스템으로 분리.
* **분류:**
  * **CombatEvents:** 데미지, 적 처치, 투사체 상호작용.
  * **ProgressionEvents:** 경험치 획득, 레벨업, 업그레이드 선택.
  * **UIEvents:** HUD 티어 전환, 토스트 알림, 메뉴 팝업.
  * **MetaEvents:** 점수 정산, 데이터 저장/로드, 서버 통신.
* **지침:** `Jules`는 모든 이벤트에 대해 명확한 접근 제한자(`public`, `private`)를 사용합니다.

#### **2. 입력 추상화 레이어 (Input Abstraction)**
* **방식:** `IInputProvider` 인터페이스를 통한 플랫폼별 로직 격리.
* **구현:**
  * `MobileInputProvider`: 자이로 + 가상 조이스틱.
  * `StandaloneInputProvider`: 마우스(Look) + 키보드(Skill).
  * `VRInputProvider`: HMD 시선 + 모션 컨트롤러.
* **이점:** 플랫폼 추가 시 핵심 조작 로직 수정 없이 `Provider`만 교체 가능.

#### **2. 오브젝트 풀링 (Object Pooling)**
* **대상:** `Projectile`, `Enemy`, `EXPGem`, `VFX`.
* **도구:** Unity 6 `UnityEngine.Pool<T>` 활용.
* **목표:** 런타임 가비지 컬렉션(GC) 최소화.

#### **3. 데이터 주도 설계 (Data-Driven Design)**
* **형식:** `ScriptableObject` 기반 에셋 관리.
* **구성:**
  * `TurretData`: 스탯 및 프리팹 정보.
  * `EnemyData`: AI 스탯 및 드랍 테이블.
  * `WaveData`: 타임라인 및 스폰 구성.

---

### **🛠️ 코딩 컨벤션 (Coding Conventions)**

| 항목 | 규격 (Standard) | 예시 |
| :--- | :--- | :--- |
| **클래스/메서드** | `PascalCase` | `CalculateDamage()` |
| **변수 (Private)** | `_camelCase` (Prefix `_`) | `_maxHealth` |
| **변수 (Public)** | `PascalCase` | `CurrentHealth` |
| **UI 요소 (UXML)** | `kebab-case` | `main-hud-container` |
| **스타일 (USS)** | 전용 `.uss` 파일 사용 | 인라인 스타일 금지 |

---

### **🛠️ 기술 스택 및 최적화 (Tech Stack & Optimization)**
* **Engine:** Unity 6 (6000.x)
* **Render Pipeline:** Universal Render Pipeline (URP)
* **UI System:** UI Toolkit
* **최적화:**
  * **Physics:** 레이어 기반 충돌 행렬(`Physics Layers`) 최적화.
  * **Memory:** `Jules`의 지침에 따라 가비지 발생이 적은 구조 설계.
  * **Rendering (Over-The-Top Optimization):**
    * **GPU Instancing:** 동일한 적/투사체 모델에 대해 1 Draw Call 처리.
    * **Batching:** 정적 환경 프롭의 Static Batching 적용.
    * **Particle System:** `GPU Simulation` 옵션을 활용하여 CPU 부하 경감.
    * **Shader:** 커스텀 셰이더 그래프를 통한 가벼운 네온/글로우 효과 구현.

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
          "hash": "VERIFICATION_HASH",
          "eventLog": "ENCRYPTED_LOG_DATA"
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
