using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Potop.Client.UI {
    /// <summary>
    /// 메인 메뉴 씬의 UI를 관리하는 컨트롤러입니다.
    /// </summary>
    public class StartMenuController : MonoBehaviour {
        [Header("UI References")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;

        /// <summary>
        /// 게임 시작 버튼입니다.
        /// </summary>
        public Button StartButton => _startButton;

        /// <summary>
        /// 게임 종료 버튼입니다.
        /// </summary>
        public Button QuitButton => _quitButton;

        private const float NORMAL_TIME_SCALE = 1f;
        private const string MAIN_SCENE = "MainScene";

        private void Start() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = NORMAL_TIME_SCALE;

            if (_startButton != null) {
                _startButton.onClick.AddListener(OnStartClicked);
            }

            if (_quitButton != null) {
                _quitButton.onClick.AddListener(OnQuitClicked);
            }
        }

        private void OnStartClicked() {
            SceneManager.LoadScene(MAIN_SCENE);
        }

        private void OnQuitClicked() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
