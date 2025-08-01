using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour 
{
    [SerializeField]
    float playDelay = 1f;

    [SerializeField]
    ImageFadeEffect transitionFadeEffect;

    void Start() {
        EnemyController.controller.TransitionManager = this;
        gameObject.SetActive(false);
    }

    public void StartTransition() {
        gameObject.SetActive(true);
        // Start fade effect
        transitionFadeEffect.FadeSpeed = 1f / playDelay;
        transitionFadeEffect.TargetAlpha = 1f;

        StartCoroutine(WaitAndGoToShop());
    }


    IEnumerator WaitAndGoToShop() {
        yield return new WaitForSeconds(playDelay);

        GameController.controller.NextShop();
    }
}
