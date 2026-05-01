# Phase 4: 콘텐츠 폴리싱 및 모바일 출시 준비 - AI Prompts

## [Milestone 19] 보스전: 타이탄 코어
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/Boss/TitanCore.cs:
- FSM: private enum BossState { Idle, Phase1, Phase2 }.
- Logic: Trigger Phase2 when _health < 50%.
- Attacks: Use Raycast for laser beams and instantiate smaller missiles from pool.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Boss Creation: Build large Sphere core with orbiting Cube satellites.
2. Animation: Create 'Entrance' animation moving boss from sky to center-top.
3. UI: Create 'BossHealthBar' (UI Toolkit ProgressBar or Slider) at top of screen.
```

## [Milestone 20] 오버클럭 모드 (무한)
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/OverclockMode.cs:
- Timer: Start after BossDeathEvent.
- Progression: Increase global multiplier '_difficultyScale' every 60s.
- Integration: EnemySpawner uses _difficultyScale to multiply EnemyData stats.
```



## [Milestone 21] 사운드 시스템 통합
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Core/SoundManager.cs (Singleton):
- Methods: public void PlayBGM(string clipName); public void PlaySFX(string clipName, Vector3 position).
- Logic: Use an AudioSource pool for concurrent SFX playback.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Create 'SoundManager' object.
2. Configure AudioMixer with Master, BGM, and SFX groups.
3. Link weapon fire and enemy death events to SoundManager calls.
```

## [Milestone 22] 모바일 광고 연동
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Core/AdManager.cs:
- Wrapper: Integration with Google AdMob SDK.
- Methods: public void LoadRewardedAd(); public void ShowRewardedAd(Action onComplete).
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Integration]
1. Open Google AdMob Settings: Input App IDs for Android and iOS.
2. Build Settings: Configure for mobile platforms (Portrait orientation).
```

## [Milestone 23] 다국어 지원 (Multilingual Support)
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Core/LocalizationManager.cs:
- Logic: Load localized strings from JSON/CSV files based on SystemLanguage.
- Method: public string GetText(string key).
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Data]
1. Create 'Assets/Resources/Localization/' folder.
2. Add 'ko.json', 'en.json', 'ja.json' with key-value pairs for UI text.
```

