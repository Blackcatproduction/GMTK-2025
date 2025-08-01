using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuManager : MonoBehaviour
{
    [SerializeField]
    float playDelay = 2.5f;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    ImageFadeEffect fadeEffect;

    // Start is called before the first frame update
    void Start() {
        menu.SetActive(true);
    }

    public void Leave() {

        // Disappear buttons
        menu.SetActive(false);

        // Start fade effect
        fadeEffect.FadeSpeed = 1f / playDelay;
        fadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndStartArena());
    }

    IEnumerator WaitAndStartArena() {
        yield return new WaitForSeconds(playDelay);

        GameController.controller.NextArena();
    }
}
