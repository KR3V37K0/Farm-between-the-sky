using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChangerSc : MonoBehaviour
{
    [Range(0, 1)]
    public float BeginningOfNight; // Значение между 0 и 1, указывающее начало ночи
    public float TimeOfDay;
    public float LengthOfDay; // Продолжительность полного дня (включая день и ночь)
    public int Days;

    void FixedUpdate()
    {
        TimeOfDay += Time.fixedDeltaTime / LengthOfDay;
        if (TimeOfDay >= 1)
        {
            TimeOfDay = 0;
            Days++;
        }

        // Определяем текущую фазу (день или ночь) и вычисляем угол поворота
        float currentPhaseTime;
        if (TimeOfDay < BeginningOfNight)
        {
            // Дневная фаза
            float lengthOfDayPhase = BeginningOfNight * LengthOfDay;
            currentPhaseTime = TimeOfDay / BeginningOfNight;
            transform.localRotation = Quaternion.Euler(currentPhaseTime * 180, 0, 0);
        }
        else
        {
            // Ночная фаза
            float lengthOfNightPhase = (1 - BeginningOfNight) * LengthOfDay;
            currentPhaseTime = (TimeOfDay - BeginningOfNight) / (1 - BeginningOfNight);
            transform.localRotation = Quaternion.Euler(180 + currentPhaseTime * 180, 0, 0);
        }
        Debug.Log(currentPhaseTime);
    }
}

