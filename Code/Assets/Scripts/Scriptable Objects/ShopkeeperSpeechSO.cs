using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShopkeeperSpeechScriptableObject")]
public class ShopkeeperSpeechSO : ScriptableObject
{
    public Sprite sprite;
    public List<string> entranceSpeeches;
    public List<string> exitSpeeches;

    private void OnValidate() {
        foreach (string speech in entranceSpeeches) {
            speech.Trim();
        }
        foreach (string speech in exitSpeeches) {
            speech.Trim();
        }
    }
}
