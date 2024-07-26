using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grass Setting", menuName = "ScriptableObjects/Grass Settings", order = 1)]
public class GrassMatSCO : ScriptableObject
{
    public Gradient FarColor, NearColor, BottomColor, ShadowColor;
}
