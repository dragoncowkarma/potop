using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public int scoreValue = 100;
    public float attackRange = 2f;

    private Transform target;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 플레이어에 도달하면 데미지
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    private void AttackPlayer()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
