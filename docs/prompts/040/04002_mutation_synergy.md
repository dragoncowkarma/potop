# Prompt: Weapon Mutation & Synergy System

## Role: Jules (Logic & Architecture Specialist)
## Milestone: [Milestone 15.5] 무기 변이 및 시너지 시스템

### 🎯 Objective
Implement a flexible synergy system where combining specific upgrades or parts results in unique projectile mutations.

### 🛠️ Requirements
1. **Mutation Logic**:
    - Extend the `Projectile` class or create a `ProjectileModifier` system.
    - Support real-time property injection (e.g., `SetBounce(int count)`, `SetExplosion(float radius)`).
2. **Synergy Resolver**:
    - A manager class that monitors the player's current upgrades.
    - When a "Synergy Pair" is detected (e.g., `Pierce` + `Explosion`), modify the weapon's firing logic.
    - Example Synergy: **"Shatter Shot"** (Pierce + Explosion) - Projectiles explode on every hit instead of just the last one.
3. **Dynamic Projectile Swap**:
    - Ability to swap the entire `ProjectilePrefab` when a high-tier synergy is active.
4. **Data-Driven**:
    - Synergy definitions should be stored in a `ScriptableObject` (e.g., `SynergyData`).

### ⚠️ Constraints
- Maintain high performance (avoid `GetComponent` in hot loops).
- Use `ProjectileContext` to pass mutation data to pooled objects.
- Ensure compatibility with the existing `WeaponBase` and `IWeapon` architecture.
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
