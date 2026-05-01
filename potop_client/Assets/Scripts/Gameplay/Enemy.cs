using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 적의 이동 및 플레이어 공격 로직을 처리하는 클래스입니다.
/// </summary>
public class Enemy : MonoBehaviour {
    [FormerlySerializedAs("_speed")]
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private int health = 10;

    [FormerlySerializedAs("_damage")]
    [SerializeField] private int _damage = 10;

    [FormerlySerializedAs("_scoreValue")]
    [SerializeField] private int _scoreValue = 100;

    [FormerlySerializedAs("_attackRange")]
    [SerializeField] private float _attackRange = 2f;

    private Transform _target;

    /// <summary>
    /// 적이 처치되었을 때 획득할 수 있는 점수입니다.
    /// </summary>
    public int ScoreValue => _scoreValue;

    private void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            _target = player.transform;
        }
    }

    private void Update() {
        if (_target != null) {
            transform.LookAt(_target);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

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

    /// <summary>
    /// 적이 데미지를 입었을 때 호출되는 메서드입니다.
    /// </summary>
    /// <param name="damage">입을 데미지 수치입니다.</param>
    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
