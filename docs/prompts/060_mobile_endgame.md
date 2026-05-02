# Phase 6: 엔드게임 및 모바일 출시 준비 - AI Prompts

---

## [Milestone 21] 보스전: 타이탄 코어
### [Step 1] Claude Opus 4.6 프롬프트
```text
[TASK:Architecture/Boss]
1. Design Phase-based Boss AI for 'Titan Core'.
2. Define transitions and attack patterns (Shielded -> Exposed -> Berserk).
```

### [Step 2] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay.Boss (Assets/Scripts/Gameplay/Boss/)][TASK:Logic/Implementation]
1. Implement the Boss AI state machine from Claude's design.
```

---

## [Milestone 22] 오버클럭 모드 (무한)
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay (Assets/Scripts/Gameplay/WaveManager.cs)][TASK:Logic/Endless]
1. Add Infinite loop logic to WaveManager.cs with exponential stat scaling.
```

---

## [Milestone 23] 모바일 광고 연동
### [Step 1] Gemini 3.1 Pro 프롬프트
```text
[SCOPE: Potop.Client.Mobile (Assets/Scripts/Core/Mobile/AdManager.cs)][TASK:SDK/Mobile]
1. Integrate Ads SDK and trigger Reward Ads for extra lives/score.
```
