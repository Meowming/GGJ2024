using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic.GameLaunch {
    public class GameLaunch : MonoBehaviour {
        public GameObject debugConsole;
        
        private void Awake() {
            DontDestroyOnLoad(this);

            InitDebugConsole();
            GameStartUp();
        }

        private void InitDebugConsole() {
            if (!Application.isEditor && !Debug.isDebugBuild) return;

            Instantiate(debugConsole);
        }

        private void GameStartUp() {
            Application.targetFrameRate = 60;

            SceneManager.LoadSceneAsync("TestLevel");
        }
    }
}