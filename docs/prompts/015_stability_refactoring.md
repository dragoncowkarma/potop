# Phase 1.5: 기술 부채 해결 및 기본 시스템 안정화 (Stability & Refactoring) - AI Prompts

## [Milestone 6] 네임스페이스 및 코드 무결성 정비
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Refactor/Cleanup]
1. Namespace Assignment:
   - Assign appropriate namespaces to all .cs files (e.g., Potop.Client.Core, Potop.Client.Gameplay, Potop.Client.UI).
2. Redundant Import Removal:
   - Remove unused `using UnityEngine.Serialization;` from all scripts.
3. Property & Logic Cleanup:
   - Resolve property naming inconsistencies in GameManager.cs (e.g., Player vs PlayerTransform) and update references in EnemyBot.cs.
4. File Integrity:
   - Ensure exactly 1 empty line at the end of all .cs files (EOF newline).
```

## [Milestone 7] 아키텍처 정렬 및 문서화 고도화
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Architecture/Docs]
1. Centralize System Control:
   - Move Cursor lock/unlock logic from FirstPersonLook.cs into GameManager.cs for centralized state management.
2. XML Documentation:
   - Add missing XML documentation for all public classes, methods, and properties across the project.
3. Accessor & Encapsulation:
   - Audit all [SerializeField] fields and ensure proper encapsulation (e.g., private fields with public getters).
4. Style Consistency:
   - Strictly enforce K&R brace style and _camelCase for private fields as per AGENTS.md.
```

## [Milestone 8] Unity 에디터 감사 및 프리팹 검증
### Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Audit/Unity]
1. Scene Hierarchy Audit:
   - Check for and remove any duplicate GameManager, EventSystem, or UI Documents in MainScene and StartScene.
2. Physics & Layer Validation:
   - Verify that 'Enemy' and 'Projectile' layers are correctly assigned and that the Collision Matrix reflects Phase 1.5 requirements.
3. Asset Link Verification:
   - Ensure TurretShooter._projectilePrefab and _firePoint are assigned in the inspector.
   - Verify EnemySpawner._enemyPrefab is correctly linked to the EnemyBot prefab.
4. Global Volume Audit:
   - Verify Bloom (Threshold 1.5) and Color Adjustments (Contrast 10) are correctly configured in the Global Volume profile.
```
