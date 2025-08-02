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
    TextMeshProUGUI shopkeeperDialog;

    ShopkeeperSpeechSO shopkeeperSpeeches;

    int currentLoop;

    [SerializeField]
    Image shopkeeper;


    void Start() {
        menu.SetActive(true);

        // Load current shopkeeper and speeches
        currentLoop = (GameController.controller.PlayerData.loopIndex/3)+1;
        int currentShopkeeper = GameController.controller.PlayerData.loopIndex % 3;

        string shopkeeperName;
        switch (currentShopkeeper) {
            case 0:
                shopkeeperName = "Qinling";
                break;

            case 1:
                shopkeeperName = "Meiling";
                break;

            case 2:
                shopkeeperName = "Ahab";
                break;

            default:
                Debug.LogError("Shopkeeper not found!");
                shopkeeperName = "Qinling";
                break;

        }
        shopkeeperSpeeches = Resources.Load<ShopkeeperSpeechSO>("Scriptable Objects/" + shopkeeperName);

        // Set speeches
        shopkeeperDialog.text = shopkeeperSpeeches.entranceSpeeches[Mathf.Min(currentLoop, 7)];

        // Set shopkeeper appearance
        shopkeeper.sprite = shopkeeper.sprite;
    }

    public void Leave() {
        // Show leaving speech
        shopkeeperDialog.text = shopkeeperSpeeches.entranceSpeeches[Mathf.Min(currentLoop, 7)];

        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        fadeEffect.FadeSpeed = 1f / playDelay;
        fadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndStartArena());
    }

    private void OnDestroy() {
        Resources.UnloadAsset(shopkeeperSpeeches);
    }

    IEnumerator WaitAndStartArena() {
        yield return new WaitForSeconds(playDelay);

        GameController.controller.NextArena();
    }
}
