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
        // Check for both "Enemy" and "Obstacle" tags/components to integrate the two systems
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.GetComponent<Obstacle>() != null || collision.gameObject.GetComponent<Enemy>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
