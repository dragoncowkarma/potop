using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    public float mouseSensitivity = 200f;
    private float xRotation = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (Mouse.current == null) return;

        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Assuming Player has a child Camera.
        // We handle vertical rotation on Camera, and horizontal rotation on the Player itself.
        Camera mainCamera = Camera.main;
        if (mainCamera != null) {
            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        transform.Rotate(Vector3.up * mouseX);
    }
}
