# Phase 1.5: 기술 부채 해결 및 기본 시스템 안정화 (Stability & Refactoring) - AI Prompts

## [Milestone 6] 코드 스타일 및 파일 무결성 정비
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Cleanup]
1. Ensure exactly 1 empty line at the end of all .cs files in Assets/Scripts/.
2. Remove all [FormerlySerializedAs] attributes from all scripts.
3. Ensure all private fields follow the _camelCase naming convention.
```

## [Milestone 7] 입력 시스템 고도화 및 회전 로직 통합
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Refactor]
1. Update Assets/Scripts/Gameplay/TurretShooter.cs:
   - Remove direct Mouse.current.delta.x polling.
   - Add [SerializeField] private InputActionReference _lookAction.
   - Use _lookAction to handle horizontal rotation.
2. Resolve Rotation Conflict:
   - Ensure only one script (either Player or Turret) handles horizontal Yaw.
   - FirstPersonLook.cs should handle Pitch (Camera), and TurretShooter.cs (or a new PlayerController) should handle Yaw.
```

## [Milestone 8] Unity 환경 설정 및 링크 검증
### Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Audit/Unity]
1. Verify Milestone 5 Settings:
   - Check Global Volume for Bloom (1.5) and Color Adjustments (Contrast: 10).
   - Check Physics Collision Matrix: 'Enemy' vs 'Enemy' = False, 'Projectile' vs 'Player' = False.
2. Verify Prefab Links:
   - Check 'Turret' in MainScene: Ensure TurretShooter._projectilePrefab and _firePoint are assigned.
   - Check 'Main Camera': Ensure FirstPersonLook.cs is attached and linked.
   - Check 'Manager': Ensure GameManager and GameHUD are linked to UIDocument.
```

