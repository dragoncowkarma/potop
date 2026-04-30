using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 점수 추가
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null && GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(enemy.scoreValue);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
