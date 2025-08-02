using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ShopkeeperSpeechScriptableObject")]
public class ShopkeeperSpeechSO : ScriptableObject
{
    public Sprite sprite;
    public List<string> entranceSpeeches;
    public List<string> exitSpeeches;
}
