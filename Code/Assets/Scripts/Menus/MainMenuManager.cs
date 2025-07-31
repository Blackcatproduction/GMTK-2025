using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    float playDelay = 2.5f;

    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject credits;
    [SerializeField]
    GameObject settings;
    [SerializeField]
    GameObject howToPlay;

    [SerializeField]
    ImageFadeEffect fadeEffect;

    // Start is called before the first frame update
    void Start() {
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
        howToPlay.SetActive(false);
    }

    public void Play() {

        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        fadeEffect.FadeSpeed = 1f / playDelay;
        fadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndStartGame());
    }

    IEnumerator WaitAndStartGame() {
        yield return new WaitForSeconds(playDelay);

        GameController.controller.StartGame();
    }

    public void Credits() {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Settings() {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void HowToPlay() {
        menu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void Back() {
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
        howToPlay.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }
}
