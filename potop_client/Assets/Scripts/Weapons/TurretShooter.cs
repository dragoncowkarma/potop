using UnityEngine;
using UnityEngine.InputSystem; // 새로운 입력 시스템 사용을 위해 반드시 추가해야 합니다.

public class TurretShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        // 수정된 부분: Input.GetButtonDown("Fire1") 대신 Mouse.current.leftButton.wasPressedThisFrame 사용
        // (Mouse.current != null 은 마우스가 연결되어 있는지 확인하는 안전장치입니다.)
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
            Debug.LogWarning("Projectile Prefab is missing!");
        }
    }

    void OnGUI()
    {
        float size = 10f;
        GUI.Box(new Rect(Screen.width / 2f - size / 2f, Screen.height / 2f - size / 2f, size, size), "+");
    }
}