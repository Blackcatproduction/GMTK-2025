using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameOverMenuManager gameOverMenu;

    public GameOverMenuManager GameOverMenu { get => gameOverMenu; set => gameOverMenu = value; }

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
        SceneManager.LoadScene(1);
    }

    public void NextArena() {

    }

    public void CallGameOverMenu() {
        gameOverMenu.GameOver();
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
