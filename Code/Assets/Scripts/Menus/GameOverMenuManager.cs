using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start() {
        GameController.controller.GameOverMenu = this;
        enabled = false;
    }

    public void GameOver() {
        enabled = true;
        backFadeEffect.enabled = true;
        menu.SetActive(true);
    }

    public void Play() {
        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        transitionFadeEffect.FadeSpeed = 1f / playDelay;
        transitionFadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndStartGame());
    }

    public void MainMenu() {
        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        transitionFadeEffect.FadeSpeed = 1f / playDelay;
        transitionFadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndGoToMainMenu());
    }

    IEnumerator WaitAndStartGame() {
        yield return new WaitForSeconds(playDelay);

        GameController.controller.StartGame();
    }

    IEnumerator WaitAndGoToMainMenu() {
        yield return new WaitForSeconds(playDelay);

        GameController.controller.ReturnToMainMenu();
    }
}
