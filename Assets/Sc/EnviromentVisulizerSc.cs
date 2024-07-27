using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnviromentVisulizerSc : MonoBehaviour
{
    float TimeValue;
    [Header("GRASS")]
    [SerializeField] Material GrassMaterial;
    [SerializeField] GrassMatSCO[] GrassSettings;
    [SerializeField] SkyMatSCO[] SkySettings;
    int idSettings; // 0,2,4,6... is night    1,3,5,7.... is day
    [Header("SKYBOX")]
    Material _skybox;
    [SerializeField] Light Sun,Moon;

    private void Start()
    {
        _skybox= RenderSettings.skybox;
    }
    public void Visualize(float NightBegin, float TimeOfDay)
    {
        if (NightBegin < TimeOfDay)
        {
            TimeValue = (TimeOfDay - NightBegin) / (1 - NightBegin);
            if (idSettings % 2 == 1) idSettings--;
        }
        else 
        { 
            TimeValue = TimeOfDay / NightBegin;
            if (idSettings % 2 == 0) idSettings++;
        }
        Debug.Log(idSettings);


        RenderSettings.fogColor = SkySettings[idSettings].FogColor.Evaluate(TimeValue);

        Sun.color = SkySettings[idSettings].SunColor.Evaluate(TimeValue);
        Sun.intensity = SkySettings[idSettings].SunIntensity.Evaluate(TimeValue);
        Moon.color = SkySettings[idSettings].MoonColor.Evaluate(TimeValue);
        Moon.intensity = SkySettings[idSettings].MoonIntensity.Evaluate(TimeValue);

        //GRASS 
        GrassMaterial.SetColor("_FarColor", GrassSettings[idSettings].FarColor.Evaluate(TimeValue));
        GrassMaterial.SetColor("_NearColor", GrassSettings[idSettings].NearColor.Evaluate(TimeValue));
        GrassMaterial.SetColor("_BottomColor", GrassSettings[idSettings].BottomColor.Evaluate(TimeValue));
        GrassMaterial.SetColor("_ShadowColor", GrassSettings[idSettings].ShadowColor.Evaluate(TimeValue));

        //SKYBOX
        _skybox.SetFloat("_AtmosphereThickness", SkySettings[idSettings].Atmosphere.Evaluate(TimeValue));
        _skybox.SetFloat("_Exposure", SkySettings[idSettings].Exposure.Evaluate(TimeValue));
        _skybox.SetColor("_SkyTint", SkySettings[idSettings].Tint.Evaluate(TimeValue));
        _skybox.SetColor("_Ground", SkySettings[idSettings].Ground.Evaluate(TimeValue));
    }
}
