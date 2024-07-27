using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayChangerSc : MonoBehaviour
{
    [Header("Time")]
    [Range(0, 1)][SerializeField] float BeginningOfNight; 
    public float TimeOfDay;
    [SerializeField] float LengthOfDay; 
    public int Days;
    float currentPhaseTime;

    [Header("Visual")]
    [SerializeField] EnviromentVisulizerSc VisualSC;
    [SerializeField] Light Sun;
    [SerializeField] Light Moon;
    [SerializeField] ParticleSystem Stars;

    [SerializeField] Gradient DirectionalColor;
    [SerializeField] Gradient FogColor;
    [SerializeField] AnimationCurve ReflectionIntensity,SunIntensity;
    [SerializeField] float FogDensity; //Default 0.01f

    [Header("Skybox")]
    [SerializeField] Material _skybox;
    [SerializeField] AnimationCurve Exposure, Atmosphere;
    [SerializeField] Gradient Tint, Ground;

    [Header("GRASS (?)")]
    [SerializeField] Material GrassMaterial;
    [SerializeField] GrassMatSCO GrassSettings;

    void FixedUpdate()
    {
        TimeOfDay += Time.fixedDeltaTime / LengthOfDay;
        if (TimeOfDay >= 1)
        {
            TimeOfDay = 0;
            Days++;
        }

        
        if (TimeOfDay < BeginningOfNight)
        {// DAY
            Stars.Stop();
            RenderSettings.sun=Sun;
            float lengthOfDayPhase = BeginningOfNight * LengthOfDay;
            currentPhaseTime = TimeOfDay / BeginningOfNight;
            transform.localRotation = Quaternion.Euler(currentPhaseTime * 180, 0, 0);
            //Visualize(0);
        }
        else
        {// NIGHT
            Stars.Play();
            RenderSettings.sun=Moon;
            float lengthOfNightPhase = (1 - BeginningOfNight) * LengthOfDay;
            currentPhaseTime = (TimeOfDay - BeginningOfNight) / (1 - BeginningOfNight);
            transform.localRotation = Quaternion.Euler(180 + currentPhaseTime * 180, 0, 0);
            //Visualize(1);
            
        }
        VisualSC.Visualize(BeginningOfNight, TimeOfDay);


    }
    void Visualize(int phase)
    {
        RenderSettings.fogColor = FogColor.Evaluate(TimeOfDay);
        RenderSettings.fogDensity = FogDensity;

        Sun.color = DirectionalColor.Evaluate(TimeOfDay);
        RenderSettings.sun.intensity = SunIntensity.Evaluate(TimeOfDay);

        //GRASS 
        GrassMaterial.SetColor("_FarColor", GrassSettings.FarColor.Evaluate(TimeOfDay));
        GrassMaterial.SetColor("_NearColor", GrassSettings.NearColor.Evaluate(TimeOfDay));
        GrassMaterial.SetColor("_BottomColor", GrassSettings.BottomColor.Evaluate(TimeOfDay));
        GrassMaterial.SetColor("_ShadowColor", GrassSettings.ShadowColor.Evaluate(TimeOfDay));

        //SKYBOX
        _skybox.SetFloat("_AtmosphereThickness", Atmosphere.Evaluate(TimeOfDay));
        _skybox.SetFloat("_Exposure", Exposure.Evaluate(TimeOfDay));
        _skybox.SetColor("_SkyTint", Tint.Evaluate(TimeOfDay));
        _skybox.SetColor("_Ground", Ground.Evaluate(TimeOfDay));
    }
}

