using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Serialization;

/// <summary>
/// 메인 메뉴 씬의 UI를 관리하는 컨트롤러입니다.
/// </summary>
public class StartMain : MonoBehaviour {
    [Header("UI References")]
    [SerializeField, FormerlySerializedAs("turretModeButton")] private Button _turretModeButton;
    [SerializeField, FormerlySerializedAs("stageModeButton")] private Button _stageModeButton;
    [SerializeField, FormerlySerializedAs("quitButton")] private Button _quitButton;

    private const float NORMAL_TIME_SCALE = 1f;
    private const string TURRET_MODE_SCENE = "MainScene";
    private const string STAGE_MODE_SCENE = "Stage";

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = NORMAL_TIME_SCALE;

        if (_turretModeButton != null) {
            _turretModeButton.onClick.AddListener(OnTurretModeClicked);
        }

        if (_stageModeButton != null) {
            _stageModeButton.onClick.AddListener(OnStageModeClicked);
        }

        if (_quitButton != null) {
            _quitButton.onClick.AddListener(OnQuitClicked);
        }
    }

    private void OnTurretModeClicked() {
        SceneManager.LoadScene(TURRET_MODE_SCENE);
    }

    private void OnStageModeClicked() {
        SceneManager.LoadScene(STAGE_MODE_SCENE);
    }

    private void OnQuitClicked() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
