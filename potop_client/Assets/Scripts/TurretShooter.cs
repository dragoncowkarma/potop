using UnityEngine;
using UnityEngine.InputSystem;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float sensitivity = 5.0f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        // 회전 (마우스 X축 입력)
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(0, mouseX, 0);

        // 발사
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
#if UNITY_EDITOR
            if (projectilePrefab == null)
                Debug.LogWarning("Projectile Prefab is missing!");
            if (firePoint == null)
                Debug.LogWarning("FirePoint is missing!");
#endif
        }
    }
}