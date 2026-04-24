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
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
