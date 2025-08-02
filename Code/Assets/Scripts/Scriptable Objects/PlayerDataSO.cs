using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerDataScriptableObject")]
public class PlayerDataSO : ScriptableObject
{
    public int maxHealth;
    public int health;
    public int score;
    public int loopIndex;

    public void CopyValues(PlayerDataSO so) {
        this.maxHealth = so.maxHealth;
        this.health = so.health;
        this.score = so.score;
        this.loopIndex = loopIndex;
    }
}
