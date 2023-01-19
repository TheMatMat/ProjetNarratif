using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IndiceData
{
    public string name;
    public int sceneID;
    public Sprite sceneSprite;
    public Sprite detailSprite;
    public Sprite inventorySprite;
}

[CreateAssetMenu(fileName = "IndiceDataBase", menuName = "ScriptableObjects/IndiceDataBase")]
public class IndiceDataBase : ScriptableObject
{
    public List<IndiceData> indiceDatas = new List<IndiceData>();
}
