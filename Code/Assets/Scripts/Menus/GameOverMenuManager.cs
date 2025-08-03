using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenuManager : MonoBehaviour
{
    [SerializeField]
    float playDelay = 2.5f;

    [SerializeField]
    ImageFadeEffect backFadeEffect;

    [SerializeField]
    ImageFadeEffect transitionFadeEffect;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    TextMeshProUGUI menuMessage;

    void Start() {
        GameController.controller.GameOverMenuManager = this;
        enabled = false;
    }

    public void GameOver(bool winSituation = false) {
        if (winSituation) {
            menuMessage.text = "You win!";

            enabled = true;
            backFadeEffect.enabled = true;
            menu.SetActive(true);

            // Pause game
            Time.timeScale = 0;
        } else {
            menuMessage.text = "Game Over";

            enabled = true;
            backFadeEffect.enabled = true;
            menu.SetActive(true);

            // Pause game
            Time.timeScale = 0;
        }
    }

    public void Play() {
        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        transitionFadeEffect.FadeSpeed = 1f / playDelay;
        transitionFadeEffect.TargetAlpha = 1f;
        transitionFadeEffect.UnscaledTime = true;

        StartCoroutine(WaitAndStartGame());
    }

    public void MainMenu() {
        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        transitionFadeEffect.FadeSpeed = 1f / playDelay;
        transitionFadeEffect.TargetAlpha = 1f;
        transitionFadeEffect.UnscaledTime = true;

        StartCoroutine(WaitAndGoToMainMenu());
    }

    IEnumerator WaitAndStartGame() {
        yield return new WaitForSecondsRealtime(playDelay);

        GameController.controller.StartGame();
    }

    IEnumerator WaitAndGoToMainMenu() {
        yield return new WaitForSecondsRealtime(playDelay);

        GameController.controller.ReturnToMainMenu();
    }
}
