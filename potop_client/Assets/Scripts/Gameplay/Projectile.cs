using UnityEngine;

/// <summary>
/// 발사체의 이동, 수명, 그리고 적과의 충돌 처리를 담당하는 클래스입니다.
/// </summary>
public class Projectile : MonoBehaviour {
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifeTime = 3f;

    private const string ENEMY_TAG = "Enemy";

    private void Start() {
        Destroy(gameObject, _lifeTime);
    }

    private void Update() {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(ENEMY_TAG)) {
            // 점수 추가
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && GameManager.Instance != null) {
                GameManager.Instance.AddScore(enemy.ScoreValue);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
