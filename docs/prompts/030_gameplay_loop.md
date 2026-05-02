# Phase 3: 게임플레이 루프 및 AI 기초 - AI Prompts

---

## [Milestone 12] 웨이브 매니저 구현
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay (Assets/Scripts/Gameplay/WaveManager.cs)][TASK:Logic/Wave]
1. Implement WaveManager.cs:
   - Logic: Track waves using a list of WaveData. Manage spawn timers.
   - Integration: Trigger EnemySpawner.Spawn() based on current wave config.
```

---

## [Milestone 13] 특수 적 AI 구현
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay.AI (Assets/Scripts/Gameplay/AI/)][TASK:Logic/AI]
1. Implement specialized AI behaviors:
   - BlitzEnemy: High speed, low health.
   - ArmoredEnemy: High health, slow.
   - SwarmEnemy: Small, moves in packs.
```

### [Step 2] Antigravity (Gemini 3.1 Pro) 프롬프트
```text
[TASK:Unity/VFX]
1. Create unique prefabs for Blitz, Armored, and Swarm enemies in 'Assets/Prefabs/Enemies/'.
2. Visual differentiation: Use scale and color variation for now.
```

---

## [Milestone 14] 피버 타임 시스템
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay (Assets/Scripts/Gameplay/FeverManager.cs)][TASK:Logic/Mechanic]
1. Implement FeverManager.cs:
   - Logic: Meter increases on enemy kill. At 100%, trigger Fever Mode (Buffs fire rate).
```

### [Step 2] Gemini 3.1 Pro 프롬프트
```text
[TASK:Unity/UI]
1. Add a Fever Meter progress bar to 'GameHUD' UXML.
2. Bind logic to update the bar from FeverManager.
```
