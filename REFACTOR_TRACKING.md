# 🛠️ Project Refactor Tracking (REFACTOR_TRACKING.md)

This file tracks technical debt, deprecated fields, and pending refactors that cannot be completed in a single pass. Agents MUST check this file at the start of every task and resolve eligible items.

## Pending Refactors

### Unity Client (potop_client)
- [x] Phase 6.5: GameManager God Object 분리 등 완료
- [x] Phase 7.5: GameHUD God Class 분리 완료 (Decompose into independent widgets)
- [x] Phase 7.5: GameState 및 GameFlowState 단일 FSM 상태 머신 통합 완료
- [x] Phase 7.5: WeaponData 클래스 중복 제거 및 리플렉션(Reflection) 제거 완료

- [ ] **Stability**: Investigate Unity MCP connection drops during Play Mode transitions.
- [ ] **Architecture**: 8 Singletons remain (GameManager, PlayerHealthController, PoolManager, EnergyManager, GameFlowController, OverclockMode, GemWallet, MetaUpgradeManager). Consider Service Locator pattern in future phases.
- [ ] **UI/UX**: Decomposed widgets subscribe to EventBroker directly, but some still poll state. Refactor widgets to be completely event-driven.

---
*Note: Delete items from this list once they are fully resolved and verified.*


