using UnityEngine;
using Potop.Client.Data;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Gameplay.VFX;
using Potop.Client.Core.Events;
using Potop.Client.Core;


namespace Potop.Client.Gameplay {
    /// <summary>
    /// Titan Core 보스의 3페이즈 행동 패턴 및 전투 로직을 제어하는 AI 클래스입니다.
    /// </summary>
    public class TitanCoreAI : EnemyBase {
        [SerializeField] private TitanCoreData _titanCoreData;
        [SerializeField] private ShieldRingRotator _shieldRingRotator;
        [SerializeField] private Transform _laserEmitter;
        [SerializeField] private LineRenderer _laserLineRenderer;

        public int CurrentPhase { get; private set; } = 1;

        // Custom state instances
        public static readonly IEnemyState Phase1State = new BossPhase1State();
        public static readonly IEnemyState Phase2State = new BossPhase2State();
        public static readonly IEnemyState Phase3State = new BossPhase3State();

        // Testing/mocking variables
        private float? _testDeltaTime;
        private float _laserDamageAccumulator = 0f;

        public float DeltaTimeVal => _testDeltaTime ?? Time.deltaTime;

        // Exposed getters of configured data
        public float LaserChargeDuration => _titanCoreData != null ? _titanCoreData.LaserChargeDuration : 1f;
        public float LaserFireDuration => _titanCoreData != null ? _titanCoreData.LaserFireDuration : 2f;
        public float LaserInterval => _titanCoreData != null ? _titanCoreData.LaserInterval : 3f;
        public float BulletInterval => _titanCoreData != null ? _titanCoreData.BulletInterval : 3f;

        public override float MoveSpeed {
            get {
                float baseSpeed = base.MoveSpeed;
                if (CurrentPhase == 3) {
                    float multiplier = _titanCoreData != null ? _titanCoreData.OverclockSpeedMultiplier : 2f;
                    return baseSpeed * multiplier;
                }
                return baseSpeed;
            }
        }

        public void SetTargetForTest(Transform target) {
            _target = target;
        }

        public void SetTestDeltaTime(float? dt) {
            _testDeltaTime = dt;
        }

        protected override void Awake() {
            base.Awake();

            if (_titanCoreData == null && _enemyData is TitanCoreData data) {
                _titanCoreData = data;
            }

            if (_shieldRingRotator == null) {
                _shieldRingRotator = GetComponentInChildren<ShieldRingRotator>();
            }

            if (_laserEmitter == null) {
                _laserEmitter = transform.Find("LaserEmitter");
            }

            if (_laserLineRenderer == null && _laserEmitter != null) {
                _laserLineRenderer = _laserEmitter.GetComponent<LineRenderer>();
                if (_laserLineRenderer == null) {
                    _laserLineRenderer = _laserEmitter.gameObject.AddComponent<LineRenderer>();
                }
            }

            if (_laserLineRenderer != null) {
                _laserLineRenderer.enabled = false;
            }
        }

        protected override void OnEnable() {
            if (_titanCoreData != null) {
                _enemyData = _titanCoreData;
            }

            base.OnEnable();

            if (_healthComponent != null) {
                _healthComponent.OnDeath += HandleBossDeath;
            }

            TransitionToPhase(1);
        }

        protected override void OnDisable() {
            base.OnDisable();

            if (_healthComponent != null) {
                _healthComponent.OnDeath -= HandleBossDeath;
            }

            SetLaserActive(false, false);
        }

        public void TransitionToPhase(int phase) {
            CurrentPhase = phase;

            EventBroker.Publish(new BossPhaseChangedEvent(phase));

            if (phase == 1) {
                StateMachine.ChangeState(Phase1State, this);
            } else if (phase == 2) {
                StateMachine.ChangeState(Phase2State, this);
            } else if (phase == 3) {
                StateMachine.ChangeState(Phase3State, this);
            }
        }

        public override void TakeDamage(DamageInfo damageInfo) {
            if (CurrentPhase == 1 && IsFrontalDamage(damageInfo)) {
                ReflectDamage(damageInfo);
                return;
            }

            base.TakeDamage(damageInfo);
            CheckPhaseTransition();
        }

        public override void ApplyKnockback(Vector3 force) {
            if (CurrentPhase == 3) {
                return; // Immue to knockback in Phase 3 (Overclocked)
            }
            base.ApplyKnockback(force);
        }

        private void CheckPhaseTransition() {
            if (_healthComponent == null || _healthComponent.MaxHealth <= 0) return;

            float hpPercent = (float)_healthComponent.CurrentHealth / _healthComponent.MaxHealth;
            if (hpPercent <= 0.3f && CurrentPhase < 3) {
                TransitionToPhase(3);
            } else if (hpPercent <= 0.6f && CurrentPhase < 2) {
                TransitionToPhase(2);
            }
        }

        public bool IsFrontalDamage(DamageInfo info) {
            if (info.Instigator == null) {
                return false;
            }

            Vector3 toInstigator = (info.Instigator.transform.position - transform.position).normalized;
            toInstigator.y = 0;

            Vector3 forward = transform.forward;
            forward.y = 0;

            float dot = Vector3.Dot(forward.normalized, toInstigator.normalized);
            return dot > 0.5f; // Frontal arc (within 60 degrees)
        }

        private void ReflectDamage(DamageInfo info) {
            // Absorb the damage (0 damage to boss) and reflect it back to player
            EventBroker.Publish(new PlayerTakeDamageEvent { Damage = info.Amount });
        }

        public void DecoupleShield() {
            if (_shieldRingRotator != null) {
                _shieldRingRotator.DecoupleAndTriggerVFX();
            }
        }

        public void SetLaserActive(bool active, bool isFiring) {
            if (_laserLineRenderer != null) {
                _laserLineRenderer.enabled = active;
                if (active) {
                    float width = _titanCoreData != null ? 
                        (isFiring ? _titanCoreData.LaserWidth : _titanCoreData.LaserChargeWidth) : 
                        (isFiring ? 1f : 0.1f);
                    _laserLineRenderer.startWidth = width;
                    _laserLineRenderer.endWidth = width;

                    Color color = isFiring ? Color.red : new Color(1f, 0.5f, 0f, 0.5f);
                    _laserLineRenderer.startColor = color;
                    _laserLineRenderer.endColor = color;
                }
            }
        }

        public void UpdateLaserAim(bool isCharging) {
            if (_laserLineRenderer == null || !_laserLineRenderer.enabled) return;

            Transform emitter = _laserEmitter != null ? _laserEmitter : transform;
            Vector3 startPos = emitter.position;
            Vector3 endPos;

            if (isCharging && Target != null) {
                endPos = Target.position;
            } else {
                float range = _titanCoreData != null ? _titanCoreData.LaserRange : 50f;
                endPos = startPos + emitter.forward * range;
            }

            _laserLineRenderer.SetPosition(0, startPos);
            _laserLineRenderer.SetPosition(1, endPos);
        }

        public void RotateTowardsPlayer() {
            if (Target == null) return;
            Vector3 direction = (Target.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero) {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, DeltaTimeVal * 5f);
            }
        }

        public void PerformLaserRaycast(float dt) {
            Transform emitter = _laserEmitter != null ? _laserEmitter : transform;
            float range = _titanCoreData != null ? _titanCoreData.LaserRange : 50f;
            float width = _titanCoreData != null ? _titanCoreData.LaserWidth : 1f;
            int damagePerSecond = _titanCoreData != null ? _titanCoreData.LaserDamage : 20;

            Ray ray = new Ray(emitter.position, emitter.forward);
            if (Physics.SphereCast(ray, width * 0.5f, out RaycastHit hit, range)) {
                if (hit.collider.CompareTag("Player") || hit.collider.GetComponent<PlayerHealthController>() != null) {
                    _laserDamageAccumulator += damagePerSecond * dt;
                    if (_laserDamageAccumulator >= 1f) {
                        int dmgToApply = Mathf.FloorToInt(_laserDamageAccumulator);
                        _laserDamageAccumulator -= dmgToApply;
                        EventBroker.Publish(new PlayerTakeDamageEvent { Damage = dmgToApply });
                    }
                }
            }
        }

        public void EmitEightDirectionBullets() {
            int count = _titanCoreData != null ? _titanCoreData.BulletCount : 8;
            float speed = _titanCoreData != null ? _titanCoreData.BulletSpeed : 8f;
            int damage = _titanCoreData != null ? _titanCoreData.BulletDamage : 5;
            GameObject prefab = _titanCoreData != null ? _titanCoreData.BulletPrefab : null;

            if (prefab == null) return;

            for (int i = 0; i < count; i++) {
                float angle = i * (360f / count);
                Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 spawnPos = transform.position + rotation * Vector3.forward * 2f;

                GameObject bulletObj = null;
                if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                    bulletObj = Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(prefab, spawnPos, rotation);
                } else {
                    bulletObj = Instantiate(prefab, spawnPos, rotation);
                }

                if (bulletObj != null) {
                    if (bulletObj.TryGetComponent<EnemyProjectile>(out var ep)) {
                        ep.Initialize(speed, damage);
                    }
                }
            }
        }

        private void HandleBossDeath() {
            EventBroker.Publish(new BossDefeatedEvent());
        }
    }

    public class BossPhase1State : IEnemyState {
        public void Enter(EnemyBase enemy) {
        }

        public void Update(EnemyBase enemy) {
            enemy.UpdateMovement();
        }

        public void Exit(EnemyBase enemy) {
        }
    }

    public class BossPhase2State : IEnemyState {
        private float _stateTimer = 0f;
        private enum LaserSubState { Charging, Firing, Cooldown }
        private LaserSubState _subState = LaserSubState.Cooldown;

        public void Enter(EnemyBase enemy) {
            if (enemy is TitanCoreAI boss) {
                boss.DecoupleShield();
                _stateTimer = 0f;
                _subState = LaserSubState.Charging;
                boss.SetLaserActive(true, false);
            }
        }

        public void Update(EnemyBase enemy) {
            if (enemy is TitanCoreAI boss) {
                float dt = boss.DeltaTimeVal;
                _stateTimer += dt;

                float chargeDur = boss.LaserChargeDuration;
                float fireDur = boss.LaserFireDuration;
                float cooldownDur = boss.LaserInterval;

                if (_subState == LaserSubState.Charging) {
                    boss.UpdateLaserAim(true);
                    boss.RotateTowardsPlayer();

                    if (_stateTimer >= chargeDur) {
                        _subState = LaserSubState.Firing;
                        _stateTimer = 0f;
                        boss.SetLaserActive(true, true);
                    }
                } else if (_subState == LaserSubState.Firing) {
                    boss.UpdateLaserAim(false);
                    boss.RotateTowardsPlayer();
                    boss.PerformLaserRaycast(dt);

                    if (_stateTimer >= fireDur) {
                        _subState = LaserSubState.Cooldown;
                        _stateTimer = 0f;
                        boss.SetLaserActive(false, false);
                    }
                } else if (_subState == LaserSubState.Cooldown) {
                    enemy.UpdateMovement();

                    if (_stateTimer >= cooldownDur) {
                        _subState = LaserSubState.Charging;
                        _stateTimer = 0f;
                        boss.SetLaserActive(true, false);
                    }
                }
            }
        }

        public void Exit(EnemyBase enemy) {
            if (enemy is TitanCoreAI boss) {
                boss.SetLaserActive(false, false);
            }
        }
    }

    public class BossPhase3State : IEnemyState {
        private float _bulletTimer = 0f;

        public void Enter(EnemyBase enemy) {
            if (enemy is TitanCoreAI boss) {
                boss.DecoupleShield();
                boss.SetLaserActive(false, false);
                _bulletTimer = 0f;
            }
        }

        public void Update(EnemyBase enemy) {
            if (enemy is TitanCoreAI boss) {
                float dt = boss.DeltaTimeVal;
                _bulletTimer += dt;

                if (_bulletTimer >= boss.BulletInterval) {
                    _bulletTimer = 0f;
                    boss.EmitEightDirectionBullets();
                }

                enemy.UpdateMovement();
            }
        }

        public void Exit(EnemyBase enemy) {
        }
    }
}
