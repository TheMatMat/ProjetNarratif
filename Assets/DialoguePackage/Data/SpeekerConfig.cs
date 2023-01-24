using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeekerConfig", menuName = "ScriptableObjects/SpeekerConfiguration", order = 1)]
public class SpeekerConfig : ScriptableObject
{
    public List<Speaker> allSpeekers = new List<Speaker>();
}
