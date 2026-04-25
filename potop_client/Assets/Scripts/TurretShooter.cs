using UnityEngine;
using UnityEngine.InputSystem;

public class TurretShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Projectile Prefab is missing!");
#endif
        }
    }
}