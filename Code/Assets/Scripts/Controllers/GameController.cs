using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
    [SerializeField]
    PlayerDataSO playerData;
    public PlayerDataSO PlayerData { get => playerData; set => playerData = value; }

    GameOverMenuManager gameOverMenuManager;

    public GameOverMenuManager GameOverMenuManager { get => gameOverMenuManager; set => gameOverMenuManager = value; }

    public static GameController controller = null;

    private void Awake() {
        if (controller == null) {
            controller = this;

            SceneManager.sceneLoaded += OnSceneLoaded;

            ResetPlayerData();

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }


    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

        // Reset player data to start game
        ResetPlayerData();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { 
        switch (scene.buildIndex) {
            case 0:
                MusicController.controller.PlaySong(MusicController.MENU_SONG);
                break;
        }
    }

    public void NextArena() {
        SceneManager.LoadScene(2);
    }

    public void NextShop() {
        SceneManager.LoadScene(1);
        playerData.loopIndex += 1;
    }

    public void CallGameOverMenu() {
        gameOverMenuManager.GameOver();
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void ResetPlayerData() {
        PlayerDataSO baseData = Resources.Load<PlayerDataSO>("Scriptable Objects/BasePlayerData");

        playerData.CopyValues(baseData);
    }
}
