using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameOverMenuManager gameOverMenuManager;

    public GameOverMenuManager GameOverMenuManager { get => gameOverMenuManager; set => gameOverMenuManager = value; }

    public static GameController controller = null;

    private void Awake() {
        if (controller == null) {
            controller = this;

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }


    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        // TODO Reset player data
    }

    public void NextArena() {
        SceneManager.LoadScene(1);
    }

    public void CallGameOverMenu() {
        gameOverMenuManager.GameOver();
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
