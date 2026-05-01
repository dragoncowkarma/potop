using UnityEngine;
using UnityEngine.InputSystem;

public class TurretShooter : MonoBehaviour
{
    [Header("Input Settings")]
    public InputActionReference attackAction;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float sensitivity = 5.0f;
    private float nextFireTime = 0f;

    // ⚠️ InputAction을 직접 스크립트에서 참조할 때는 반드시 활성화/비활성화 과정이 필요합니다.
    private void OnEnable()
    {
        if (attackAction != null) attackAction.action.Enable();
    }

    private void OnDisable()
    {
        if (attackAction != null) attackAction.action.Disable();
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        // 회전 (마우스 X축 입력)
        float mouseX = Mouse.current.delta.x.ReadValue() * sensitivity;
        transform.Rotate(0, mouseX, 0);

        // 발사
        if (attackAction.action.IsPressed() && Time.time >= nextFireTime)
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
