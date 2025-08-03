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

    AudioSource audioSource;
    [SerializeField]
    AudioClip buttonSound;

    // Start is called before the first frame update
    void Start() {
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
        howToPlay.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    public void Play() {
        audioSource.PlayOneShot(buttonSound);

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
        audioSource.PlayOneShot(buttonSound);
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void Settings() {
        audioSource.PlayOneShot(buttonSound);
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void HowToPlay() {
        audioSource.PlayOneShot(buttonSound);
        menu.SetActive(false);
        howToPlay.SetActive(true);
    }

    public void Back() {
        audioSource.PlayOneShot(buttonSound);
        menu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
        howToPlay.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }
}
