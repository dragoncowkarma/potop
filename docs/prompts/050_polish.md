# Phase 5: 폴리싱 및 사용자 경험 (UX) - AI Prompts

---

## [Milestone 18] 카메라 쉐이크 연출
### [Step 1] Gemini 3 Flash 프롬프트
```text
[SCOPE: Potop.Client.VFX (Assets/Scripts/VFX/CameraShake.cs)][TASK:Boilerplate/VFX]
1. Implement CameraShake.cs: Simple Perlin noise or Random offset based shake.
2. Trigger: Call .Shake() on firing and taking damage.
```

---

## [Milestone 19] 사운드 시스템 통합
### [Step 1] Gemini 3 Flash 프롬프트
```text
[SCOPE: Potop.Client.Audio (Assets/Scripts/Core/Audio/AudioManager.cs)][TASK:Boilerplate/Audio]
1. Implement AudioManager.cs (Singleton): PlaySFX(clip), PlayMusic(clip).
```

### [Step 2] Gemini 3.1 Pro 프롬프트
```text
[TASK:Unity/Integration]
1. Hook up AudioClips to gameplay events (Fire, Kill, Hit).
```

---

## [Milestone 20] 다국어 지원 (Localization)
### [Step 1] Gemini CLI 프롬프트
```text
[TASK:Data/Batch]
1. Create Assets/Resources/Loca.json with [ko, en] translations.
```

### [Step 2] Gemini 3 Flash 프롬프트
```text
[SCOPE: Potop.Client.UI (Assets/Scripts/UI/LocaHelper.cs)][TASK:UI/Binding]
1. Implement LocaHelper.cs to update UI Toolkit labels from JSON data.
```
