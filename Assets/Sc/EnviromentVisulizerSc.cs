using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnviromentVisulizerSc : MonoBehaviour
{
    float TimeValue;
    //[Header("GLOBAL")]  

    [Header("WEATHER")]
    [SerializeField] Volume _volume;
    ColorAdjustments adj;
    ChannelMixer mix;
    [SerializeField] Color resultColor;

    [SerializeField] float[] forceWind=new float[4];
    [SerializeField] Color[] colorHeat = new Color[4]; 
    [SerializeField] Color[] colorRain = new Color[4];


    [Header("GRASS")]
    [SerializeField] Material GrassMaterial;
    [SerializeField] GrassMatSCO[] GrassSettings;  
    int idSettings; // 0 is night    1 is day


    [Header("SKYBOX")]  
    [SerializeField] SkyMatSCO[] SkySettings;
    [SerializeField] Light Sun,Moon;
    [SerializeField] ParticleSystem RainParticle;
    [SerializeField] Material Clouds;
    Material _skybox;

    private void Start()
    {
        _skybox= RenderSettings.skybox;
        _volume.sharedProfile.TryGet<ColorAdjustments>(out adj);
        _volume.sharedProfile.TryGet<ChannelMixer>(out mix);
    }
    /*private void Update()
    {
         adj.colorFilter.value = c;
    }*/
    public void VisualizeWeather(int wind,int heat,int rain)
    {
        mix.redOutBlueIn.value = heat;
        //resultColor = new Color((colorHeat[heat].r + colorRain[rain].r) / 2, (colorHeat[heat].g + colorRain[rain].g) / 2, (colorHeat[heat].b + colorRain[rain].b) / 2);
        //if(heat)    


        //Wind(wind);
        //grass wind
        //cloud wind      

        //Heat(heat);
        //postprocess heat

        //Rain(rain);
        //postprocess rain
        //particle rain
        //wind rain
    }
    /*void Wind(int wind)
    {
        
    }
    void Heat(int heat)
    {
        adj.colorFilter.value = colorHeat[heat];
    }
    void Rain(int rain)
    {
        adj.colorFilter.value = colorHeat[rain];
    }*/
    public void VisualizeCycle(float NightBegin, float TimeOfDay)
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
        //Debug.Log(idSettings);


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
