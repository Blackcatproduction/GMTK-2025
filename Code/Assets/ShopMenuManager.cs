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


    [Header("Shopkeeper")]
    [SerializeField]
    SpriteRenderer shopkeeper;

    [SerializeField]
    TextMeshProUGUI shopkeeperDialog;
    [SerializeField]
    TextMeshProUGUI shopkeeperNameComponent;

    ShopkeeperSpeechSO shopkeeperSpeeches;


    void Start() {
        menu.SetActive(true);

        // Load current shopkeeper and speeches
        currentLoop = GameController.controller.PlayerData.loopIndex/3;
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
        shopkeeperNameComponent.text = shopkeeperName;

        shopkeeperSpeeches = Resources.Load<ShopkeeperSpeechSO>("Scriptable Objects/" + shopkeeperName);

        // Set speeches
        shopkeeperDialog.text = shopkeeperSpeeches.entranceSpeeches[Mathf.Min(currentLoop, 7)];

        // Set shopkeeper appearance
        shopkeeper.sprite = shopkeeperSpeeches.sprite;
    }

    public void Leave() {

        StartCoroutine(WaitAndStartArena());
    }

    IEnumerator WaitAndStartArena() {
        // Show leaving speech
        shopkeeperDialog.text = shopkeeperSpeeches.exitSpeeches[Mathf.Min(currentLoop, 7)];

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
