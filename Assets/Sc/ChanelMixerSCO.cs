using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChanelMixer Setting", menuName = "ScriptableObjects/ChanelMixer Setting", order = 1)]

public class ChanelMixerSCO : ScriptableObject
{
    [Header("RED")]
    [SerializeField, Range(-200, 200)] int rr;
    [SerializeField, Range(-200, 200)] int rg, rb;

    [Header("GREEN")]
    [SerializeField, Range(-200, 200)] int gr;
    [SerializeField, Range(-200, 200)] int gg, gb;

    [Header("BLUE")]
    [SerializeField, Range(-200, 200)] int br;
    [SerializeField, Range(-200, 200)] int bg, bb;
}

