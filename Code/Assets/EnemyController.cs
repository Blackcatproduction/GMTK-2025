using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    List<Enemy> enemiesList = new List<Enemy>();

    TransitionManager transitionManager;
    public TransitionManager TransitionManager { get => transitionManager; set => transitionManager = value; }

    public static EnemyController controller = null;

    private void Awake() {
        if (controller == null) {
            controller = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void AddEnemy(Enemy enemy) {
        enemiesList.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy) {
        enemiesList.Remove(enemy);

        // Check if there are no more enemies
        if (enemiesList.Count == 0) {
            transitionManager.StartTransition();
        }
    }

    public int EnemyCount() {
        return enemiesList.Count;
    }
}
