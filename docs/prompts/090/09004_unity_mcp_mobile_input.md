# [TARGET: Assets/Scripts/Input/MobileInputManager.cs] [TASK: 9.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.4` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/conflict_report.md`.

- **Map**: `docs/map.md` — Required symbols: `MobileInputManager`, `EventBroker`
- **Delta**: `docs/delta/9.3.json`
- **GDD**: `docs/requirements/02_gameplay_mechanics.md` — control schemes and max rotation speed

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Input/MobileInputManager.cs`
- `Assets/Scripts/Input/AutoFireController.cs`
- Existing input provider or input action assets required to close the conflict report entries

### Technical Requirements (10-Year Expert Feedback)
1. **Input Abstraction Alignment**: Implement mobile input through the existing input abstraction/provider pattern rather than branching gameplay logic by platform.
2. **Virtual Joystick and Touch Controls**: Support drag-to-rotate, optional gyro assist, skill icon touches, and clear dead-zone handling.
3. **Auto-Fire Integration**: Auto-fire scans valid target ranges and fires only when target alignment and cooldown thresholds are satisfied.
4. **Conflict Report Closure**: Implement or explicitly route support for the two open items: rotation speed cap (`180 degrees/sec`) and keyboard rotation (`WASD`/arrow keys) for desktop fallback.
5. **Assist Curves**: Use configurable aim-assist and rotation smoothing curves; do not bury tuning values in code.
6. **Memory Safety**: Unsubscribe all touch/input events on disable, scene change, and provider switch.
7. **Accessibility Hooks**: Expose joystick size, sensitivity, and auto-fire toggle values for the Phase 9 lobby/settings UI.

### Verification Criteria (QA Perspective)
1. **Input Mock Tests**: Simulate touch drags and verify rotation vector/dead-zone output.
2. **Rotation Cap Test**: High-sensitivity input cannot exceed `180 degrees/sec` unless a documented meta upgrade modifies the cap.
3. **Keyboard Fallback Test**: WASD/arrow input rotates the turret on desktop provider.
4. **Auto-Fire Test**: Targets outside range/alignment do not fire; valid targets do fire after cooldown.
5. **Conflict Report Sync**: Update `docs/conflict_report.md` if the open input items are resolved.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
