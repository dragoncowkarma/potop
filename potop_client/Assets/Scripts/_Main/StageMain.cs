using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageMain : MonoBehaviour
{
    public static StageMain main;
    public GameObject obstacle;

    private float t = 0f;
    private float survivalTime = 0f;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            return;

        survivalTime += Time.deltaTime;

        // 생존 시간을 점수로 환산
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score = Mathf.FloorToInt(survivalTime * 10);
            // 직접 이벤트 호출은 하지 않고 HUD에서 Update로 처리
        }

        t += Time.deltaTime;
        // 시간이 지날수록 스폰 간격이 줄어듦 (난이도 상승)
        float spawnInterval = Mathf.Max(1f, 4f - survivalTime * 0.05f);
        if (t > spawnInterval)
        {
            t = 0;
            var a = Random.Range(0f, 360f);
            Instantiate(obstacle, new Vector3(10 * Mathf.Sin(a), 10f, 10 * Mathf.Cos(a)), Quaternion.identity);
        }
    }
}
