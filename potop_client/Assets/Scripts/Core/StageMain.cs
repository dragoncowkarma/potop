using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageMain : MonoBehaviour {
    public static StageMain main;
    
    private void Awake() {
        main = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Cursor lock logic moved to Player.cs
    }

    // Update is called once per frame
    void Update() {
        // Spawning logic delegated to Spawner.cs components in the scene
    }
}
