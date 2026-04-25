using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMain : MonoBehaviour
{
    [Header("UI References")]
    public Button turretModeButton;
    public Button stageModeButton;
    public Button quitButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        if (turretModeButton != null)
            turretModeButton.onClick.AddListener(OnTurretModeClicked);

        if (stageModeButton != null)
            stageModeButton.onClick.AddListener(OnStageModeClicked);

        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitClicked);
    }

    private void OnTurretModeClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnStageModeClicked()
    {
        SceneManager.LoadScene("Stage");
    }

    private void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
