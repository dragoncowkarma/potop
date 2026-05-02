# 🚨 Implementation Conflict Report (docs/conflict_report.md)

This document records discrepancies between the project's Game Design Document (GDD) and the current technical implementation in `potop_client/`.

## 1. Core Gameplay & Controls

| Feature | GDD Specification | Current Implementation | Status / Conflict Reason |
| :--- | :--- | :--- | :--- |
| **Turret Rotation Speed** | Constant 180°/sec. | Input-based (Delta * Sensitivity). | **Conflict**: No cap on rotation speed; sensitivity varies by device/setting. |
| **Keyboard Input** | WASD/Arrow keys for rotation. | LookAction (Delta/Vector2) only. | **Conflict**: Keyboard rotation is not explicitly handled for constant speed. |
| **Turret Classes** | 4 distinct types (Guardian, Valkyrie, etc.). | Single generic `TurretShooter`. | **Incomplete**: Class selection and variant logic missing. |

## 2. Enemy System

| Feature | GDD Specification | Current Implementation | Status / Conflict Reason |
| :--- | :--- | :--- | :--- |
| **Enemy Types** | Scouter, Blitz, Armored, Hellfire. | Single `EnemyBot` class. | **Incomplete**: Variant behaviors (e.g., Blitz zigzag) not implemented. |
| **Spawning Logic** | Wave-based timeline (Phase 1-5). | Constant interval spawning. | **Conflict**: Time-based difficulty and wave progression missing. |

## 3. Architecture & Data

| Feature | GDD Specification | Current Implementation | Status / Conflict Reason |
| :--- | :--- | :--- | :--- |
| **Data Management** | `ScriptableObject` driven. | Hardcoded / Serialized fields. | **Conflict**: Violation of Data-Driven Design principle mentioned in GDD 04. |
| **Object Pooling** | `UnityEngine.Pool` for projectiles/enemies. | `Instantiate` / `Destroy` usage. | **Conflict**: Performance concern; not aligned with Technical Architecture goals. |
| **Event System** | Centralized Event Broker. | Static `Action` events in `GameManager`. | **Partial**: Decoupling exists but is tied to the Singleton instance. |

## 4. Progression & Meta

| Feature | GDD Specification | Current Implementation | Status / Conflict Reason |
| :--- | :--- | :--- | :--- |
| **RPG Elements** | EXP, Leveling, Passive choices. | Score-only tracking. | **Incomplete**: Core Roguelite loop missing in code. |
| **Game Lifecycle** | 15m Wave -> Boss -> Overclock. | Start -> Playing -> GameOver. | **Incomplete**: Progression timer and endgame states missing. |

## 5. Summary of Divergence
The current implementation serves as a **functional MVP** for movement and basic shooting, but it lacks the **architectural foundation** (ScriptableObjects, Pooling, Event Broker) and the **roguelite depth** (Waves, Leveling, Variants) specified in the GDD.

---
**Last Updated**: 2026-05-02
**Agent**: Gemini CLI
