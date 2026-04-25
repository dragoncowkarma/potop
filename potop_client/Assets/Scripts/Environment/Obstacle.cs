using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        rb.AddForce(-transform.position);
    }
}
