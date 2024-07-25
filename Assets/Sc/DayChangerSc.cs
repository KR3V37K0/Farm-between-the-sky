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

    [Header("Visual")]
    [SerializeField] Light Sun;
    [SerializeField] Light Moon;
    [SerializeField] ParticleSystem Stars;

    [SerializeField] Gradient DirectionalColor;
    [SerializeField] Gradient FogColor;
    [SerializeField] AnimationCurve ReflectionIntensity;
    [SerializeField] float FogDensity; //Default 0.01f

    void FixedUpdate()
    {
        TimeOfDay += Time.fixedDeltaTime / LengthOfDay;
        if (TimeOfDay >= 1)
        {
            TimeOfDay = 0;
            Days++;
        }

        float currentPhaseTime;
        if (TimeOfDay < BeginningOfNight)
        {
            // Дневная фаза
            Stars.Stop();
            Stars.Clear();
            RenderSettings.sun=Sun;
            float lengthOfDayPhase = BeginningOfNight * LengthOfDay;
            currentPhaseTime = TimeOfDay / BeginningOfNight;
            transform.localRotation = Quaternion.Euler(currentPhaseTime * 180, 0, 0);
        }
        else
        {
            Stars.Play();
            // Ночная фаза
            RenderSettings.sun=Moon;
            float lengthOfNightPhase = (1 - BeginningOfNight) * LengthOfDay;
            currentPhaseTime = (TimeOfDay - BeginningOfNight) / (1 - BeginningOfNight);
            transform.localRotation = Quaternion.Euler(180 + currentPhaseTime * 180, 0, 0);
        }
        Visualize();
    }
    void Visualize()
    {
        RenderSettings.fogColor = FogColor.Evaluate(TimeOfDay);
        RenderSettings.fogDensity = FogDensity;

        Sun.color = DirectionalColor.Evaluate(TimeOfDay);
        RenderSettings.reflectionIntensity=ReflectionIntensity.Evaluate(TimeOfDay);
    }
}

