using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayChangerSc : MonoBehaviour
{
    [Header("Time")]
    [Range(0, 1)][SerializeField] float BeginningOfNight; // Значение между 0 и 1, указывающее начало ночи
    public float TimeOfDay;
    [SerializeField] float LengthOfDay; // Продолжительность полного дня (включая день и ночь)
    public int Days;
    float currentPhaseTime;

    [Header("Visual")]
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

    [Header("GRASS (в отдельный скрипт?)")]
    [SerializeField] Material GrassMaterial;
    [SerializeField] GrassMatSCO GrassSettings;
    float timeChange = 0f;

    void FixedUpdate()
    {
        TimeOfDay += Time.fixedDeltaTime / LengthOfDay;
        if (TimeOfDay >= 1)
        {
            TimeOfDay = 0;
            Days++;
        }

        
        if (TimeOfDay < BeginningOfNight)
        {
            // Дневная фаза
            Stars.Stop();
            Stars.Clear();
            RenderSettings.sun=Sun;
            float lengthOfDayPhase = BeginningOfNight * LengthOfDay;
            currentPhaseTime = TimeOfDay / BeginningOfNight;
            transform.localRotation = Quaternion.Euler(currentPhaseTime * 180, 0, 0);
            Visualize(0);
        }
        else
        {// Ночная фаза
            Stars.Play();
            RenderSettings.sun=Moon;
            float lengthOfNightPhase = (1 - BeginningOfNight) * LengthOfDay;
            currentPhaseTime = (TimeOfDay - BeginningOfNight) / (1 - BeginningOfNight);
            transform.localRotation = Quaternion.Euler(180 + currentPhaseTime * 180, 0, 0);
            Visualize(1);
        }
        

    }
    void Visualize(int phase)
    {
        RenderSettings.fogColor = FogColor.Evaluate(TimeOfDay);
        RenderSettings.fogDensity = FogDensity;

        Sun.color = DirectionalColor.Evaluate(TimeOfDay);
        RenderSettings.sun.intensity = SunIntensity.Evaluate(TimeOfDay);

        //RenderSettings.reflectionIntensity=ReflectionIntensity.Evaluate(TimeOfDay);

        //GRASS 
        /*
        GrassMaterial.SetColor("_FarColor", Color.Lerp(GrassSettings[otherphase].FarColor, GrassSettings[phase].FarColor, timeChange));
        GrassMaterial.SetColor("_NearColor", Color.Lerp(GrassSettings[otherphase].NearColor, GrassSettings[phase].NearColor, timeChange));
        GrassMaterial.SetColor("_BottomColor", Color.Lerp(GrassSettings[otherphase].BottomColor, GrassSettings[phase].BottomColor, timeChange));
        GrassMaterial.SetColor("_ShadowColor", Color.Lerp(GrassSettings[otherphase].ShadowColor, GrassSettings[phase].ShadowColor, timeChange));
        timeChange += Time.deltaTime / 10f;
        if (timeChange > 1f) timeChange = 0f;*/
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

