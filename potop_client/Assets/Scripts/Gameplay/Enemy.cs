using UnityEngine;

/// <summary>
/// 적의 이동 및 플레이어 공격 로직을 처리하는 클래스입니다.
/// </summary>
public class Enemy : MonoBehaviour {
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _scoreValue = 100;
    [SerializeField] private float _attackRange = 2f;

    private Transform _target;

    /// <summary>
    /// 적이 처치되었을 때 획득할 수 있는 점수입니다.
    /// </summary>
    public int ScoreValue => _scoreValue;

    private void Start() {
        if (GameManager.Instance != null) {
            _target = GameManager.Instance.PlayerTransform;
        }
    }

    private void Update() {
        if (_target != null) {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;

            // 플레이어에 도달하면 데미지
            float distanceToPlayer = Vector3.Distance(transform.position, _target.position);
            if (distanceToPlayer <= _attackRange) {
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer() {
        if (GameManager.Instance != null) {
            GameManager.Instance.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
