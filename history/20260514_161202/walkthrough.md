# 🚀 POTOP GDD 최종 고도화 완료 보고서

3단계 전문가 패널 리뷰(수석 기획자, TD, AD, UX 전문가)를 거쳐 **POTOP (Project Over-The-Top Power)**의 게임 기획서(GDD)를 최종 업데이트했습니다.

## 🛠️ 주요 변경 사항 요약

### 1. 장르 및 타켓층 재정의
- **장르:** 하이퍼캐주얼 → **캐주얼 로그라이트 (Casual Roguelite)**
- **타겟:** 단순 킬링타임 유저에서 빌드 최적화를 즐기는 **미드코어 게이머**까지 확장.

### 2. 핵심 메카닉 고도화
- **레벨업 흐름:** Lv.1~5 자동 강화로 템포 유지, Lv.6 이후 3레벨마다 선택형 UI 제공 (슬로우모션 및 타이머 적용).
- **위협 인디케이터:** 360도 전방위 위협을 색상 코딩으로 알리는 인디케이터 시스템 추가.
- **포탑 밸런스:** **노바(Nova)** 터렛의 폭발 반경(1.5m) 및 무제한 관통 속성 부여로 개성 강화.
- **시너지:** 피버(Fever) 모드와 오버차지(Overcharge) 간의 자원 효율 시너지 추가.

### 3. 경제 및 비즈니스 모델
- **하이브리드 BM:** 기존 광고(IAA) 모델에 **인앱 결제(IAP)** 및 **시즌 패스** 결합.
- **리텐션:** 일일/주간 미션 시스템을 통해 장기 리텐션 확보 전략 수립.

### 4. 기술 아키텍처 및 최적화
- **이벤트 시스템:** 단일 `EventBroker`를 **카테고리별(Combat, UI, Meta 등)**로 분리하여 확장성 확보.
- **입력 추상화:** 플랫폼별(Mobile, PC, VR) 조작을 통합 관리하는 **Input Abstraction Layer** 설계.
- **성능 최적화:** 최대 액티브 적 수(200개) 제한 및 **GPU Instancing** 활용 명시.

### 5. 프로젝트 관리 및 로드맵
- **조기 검증:** 핵심 루프 검증을 위한 **튜토리얼 프로토타입[07005]**을 Phase 7(Vertical Slice)에 전면 배치.
- **표준화:** 에셋 네이밍 컨벤션을 수립하여 협업 효율성 증대.

## 📂 업데이트된 문서 목록

| 파일명 | 주요 업데이트 내용 |
| :--- | :--- |
| [01_overview.md](file:///Users/macbook/Desktop/potop/docs/gdd/01_overview.md) | 장르 재분류, 타겟 오디언스 확장 |
| [02_gameplay_mechanics.md](file:///Users/macbook/Desktop/potop/docs/gdd/02_gameplay_mechanics.md) | 위협 인디케이터, 레벨업 흐름, 피버 시너지 |
| [03_data_and_balance.md](file:///Users/macbook/Desktop/potop/docs/gdd/03_data_and_balance.md) | 터렛/적/EXP 상세 데이터, 성능 제약 사항 |
| [04_technical_architecture.md](file:///Users/macbook/Desktop/potop/docs/gdd/04_technical_architecture.md) | 카테고리별 이벤트 브로커, 입력 추상화 레이어 |
| [05_meta_and_progression.md](file:///Users/macbook/Desktop/potop/docs/gdd/05_meta_and_progression.md) | 하이브리드 BM, 미션/시즌 패스 시스템 |
| [06_art_and_sound.md](file:///Users/macbook/Desktop/potop/docs/gdd/06_art_and_sound.md) | 에셋 네이밍 컨벤션 표준화 |
| [07_development_milestones.md](file:///Users/macbook/Desktop/potop/docs/gdd/07_development_milestones.md) | 튜토리얼 프로토타입[07005] 추가 |
| [08_wbs.md](file:///Users/macbook/Desktop/potop/docs/gdd/08_wbs.md) | 신규 마일스톤 반영 및 작업 분해 |

## 🏁 다음 단계 제안
1. **Phase 4 개발 착수:** 업데이트된 [04001] 터렛 클래스 및 [04003] XP/레벨업 로직 구현 가이드에 따라 개발 시작.
2. **프로토타입 검증:** Phase 7에 배치된 튜토리얼 프로토타입을 통해 조기에 조작감과 성장 루프의 재미 검증.

---
**POTOP 프로젝트의 기획 고도화가 성공적으로 완료되었습니다. 이제 본격적인 구현 단계로 나아갈 준비가 되었습니다.**
