using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageMain : MonoBehaviour {
    public static StageMain main;
    public GameObject obstacle;
    
    private float t = 0f;
    
    private void Awake() {
        main = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        t += Time.deltaTime;
        if (t > Random.Range(3f, 5f)) {
            t = 0;
            var a = Random.Range(0f, 360f);
            Instantiate(obstacle, new Vector3(10 * Mathf.Sin(a), 10f, 10 * Mathf.Cos(a)), Quaternion.identity);
        }
    }
}
