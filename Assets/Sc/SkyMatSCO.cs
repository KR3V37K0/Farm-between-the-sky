using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sky Setting", menuName = "ScriptableObjects/Sky Settings", order = 1)]

public class SkyMatSCO : ScriptableObject
{
    public Gradient FogColor;
    public AnimationCurve Exposure, Atmosphere;
    public Gradient Tint, Ground;
    public Gradient SunColor,MoonColor;
    public AnimationCurve SunIntensity, MoonIntensity;
}
