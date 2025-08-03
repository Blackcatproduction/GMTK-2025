using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [Header("Scenario")]
    [SerializeField]
    Sprite pastScenario;
    [SerializeField]
    Sprite presentScenario;
    [SerializeField]
    Sprite futureScenario;

    [Header("Enemies")]
    [SerializeField]
    Sprite pastEnemy;
    [SerializeField]
    Sprite presentEnemy;
    [SerializeField]
    Sprite futureEnemy;

    [Header("Sprite Renderers")]
    [SerializeField]
    List<SpriteRenderer> enemies;
    [SerializeField]
    SpriteRenderer scenario;

    void Start()
    {
        int currentPeriod = GameController.controller.PlayerData.loopIndex % 3;

        switch (currentPeriod) {
            case 0:
                foreach (SpriteRenderer enemy in enemies) {
                    enemy.sprite = pastEnemy;
                }
                scenario.sprite = pastScenario;
                break;

            case 1:
                foreach (SpriteRenderer enemy in enemies) {
                    enemy.sprite = presentEnemy;
                }
                scenario.sprite = presentScenario;

                break;

            case 2:
                foreach (SpriteRenderer enemy in enemies) {
                    enemy.sprite = futureEnemy;
                }
                scenario.sprite = futureScenario;

                break;
        }
    }

}
