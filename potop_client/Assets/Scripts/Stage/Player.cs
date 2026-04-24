using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private const float MOUSE_SENSITIVITY = 200f;
    
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * MOUSE_SENSITIVITY;
        mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * MOUSE_SENSITIVITY;
        
        xRotation -= mouseY;
        yRotation += mouseX;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
