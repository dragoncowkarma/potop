using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody rb;
    public int damage = 20;

    void Start()
    {

    }

    void Update()
    {
        rb.AddForce(-transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
