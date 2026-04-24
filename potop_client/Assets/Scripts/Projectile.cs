using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
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
