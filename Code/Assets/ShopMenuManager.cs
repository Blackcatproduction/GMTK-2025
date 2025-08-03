using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour
{
    [SerializeField]
    float playDelay = 2.5f;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    ImageFadeEffect fadeEffect;

    [SerializeField]
    Button leaveButton;

    [SerializeField]
    int currentLoop;

    AudioSource audioSource;
    [SerializeField]
    AudioClip buttonSound;


    [Header("Shopkeeper")]
    [SerializeField]
    SpriteRenderer shopkeeper;

    [SerializeField]
    TextMeshProUGUI shopkeeperDialog;
    [SerializeField]
    TextMeshProUGUI shopkeeperNameComponent;

    ShopkeeperSpeechSO shopkeeperSpeeches;

    [SerializeField]
    bool isSpeaking = false;
    string currentSpeech;

    public bool IsSpeaking { get => isSpeaking; set {
            isSpeaking = value;

            if (value) {
                audioSource.Play();
            } else {
                audioSource.Stop();
            }
        }
    }

    void Start() {
        menu.SetActive(true);

        audioSource = GetComponent<AudioSource>();

        // Load current shopkeeper and speeches
        currentLoop = GameController.controller.PlayerData.loopIndex/3;
        int currentShopkeeper = GameController.controller.PlayerData.loopIndex % 3;

        string shopkeeperName;
        switch (currentShopkeeper) {
            case 0:
                shopkeeperName = "Qinling";
                MusicController.controller.PlaySong(MusicController.PAST_SONG);
                break;

            case 1:
                shopkeeperName = "Meiling";
                MusicController.controller.PlaySong(MusicController.PRESENT_SONG);
                break;

            case 2:
                shopkeeperName = "Ahab";
                MusicController.controller.PlaySong(MusicController.FUTURE_SONG);
                break;

            default:
                Debug.LogError("Shopkeeper not found!");
                shopkeeperName = "Qinling";
                MusicController.controller.PlaySong(MusicController.PAST_SONG);
                break;

        }
        shopkeeperNameComponent.text = shopkeeperName;

        shopkeeperSpeeches = Resources.Load<ShopkeeperSpeechSO>("Scriptable Objects/" + shopkeeperName);

        // Set speeches
        Speak(shopkeeperSpeeches.entranceSpeeches[Mathf.Min(currentLoop, 7)]);

        // Set shopkeeper appearance
        shopkeeper.sprite = shopkeeperSpeeches.sprite;
    }

    public void Speak(string speech) {
        IsSpeaking = true;
        currentSpeech = speech;
        shopkeeperDialog.text = "";
        StartCoroutine(ShowSpeechByChar());
    }

    IEnumerator ShowSpeechByChar() {
        while (IsSpeaking) {
            if (shopkeeperDialog.text.Length == currentSpeech.Length) {
                IsSpeaking = false;
            } else {
                // Check dialog text size
                int speechSize = shopkeeperDialog.text.Length;

                // Get next valid character on current speech
                while (speechSize < currentSpeech.Length && currentSpeech[speechSize] == ' ') {
                    speechSize++;
                }

                if (speechSize >= currentSpeech.Length) {
                    // End speech
                    IsSpeaking = false;
                }
                else {
                    shopkeeperDialog.text = currentSpeech.Substring(0, speechSize + 1);
                }
            }

            yield return new WaitForSeconds(0.025f);
        }
    }

    public void Leave() {
        audioSource.PlayOneShot(buttonSound);

        if (IsSpeaking) {
            IsSpeaking = false;
            StopAllCoroutines();
        }

        StartCoroutine(WaitAndStartArena());
    }

    IEnumerator WaitAndStartArena() {
        // Show leaving speech
        Speak(shopkeeperSpeeches.exitSpeeches[Mathf.Min(currentLoop, 7)]);

        // Disappear buttons
        leaveButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(shopkeeperDialog.text.Length * 0.03f);

        // Start fade effect
        fadeEffect.FadeSpeed = 1f / playDelay;
        fadeEffect.TargetAlpha = 1f;
        yield return new WaitForSeconds(playDelay);

        GameController.controller.NextArena();
    }
}
