using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherSc : MonoBehaviour
{
    public int wind, heat, rain; 
    public float Stage_1, Stage_2, Stage_3;
    public bool CenerateWeather;
    private void Update() { if (CenerateWeather) WeatherChanger(); }

    public void WeatherChanger() 
    {
        wind = CenrateStageWether();
        heat = CenrateStageWether();
        rain = CenrateStageWether();
        Debug.LogWarning(wind + " " + heat + " " + rain);
        CenerateWeather = false;
    }
    private int CenrateStageWether() 
    {
        float value;
        value = Random.Range(0f, 1f);
        Debug.LogWarning(value);
             if (value <= Stage_1) return 0;
        else if (value <= Stage_2) return 1;
        else if (value <= Stage_3) return 2;
        else return 3 ;
        
    }
}
