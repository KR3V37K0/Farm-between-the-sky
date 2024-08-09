using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherSc : MonoBehaviour
{
    [SerializeField] EnviromentVisulizerSc Visualizer;
    [SerializeField] int Wind,Heat,Rain;


    [SerializeField] float stage_1, stage_2, stage_3;
    public bool GenerateWeather;
   
    private void Update() { if (GenerateWeather) WeatherChanger(); }

    public void WeatherChanger() 
    {
        Wind = GenerateStageWeather();
        Heat = GenerateStageWeather();
        Rain = GenerateStageWeather();
        GenerateWeather = false;

        Visualizer.VisualizeWeather(Wind, Heat, Rain);
    }
    private int GenerateStageWeather() 
    {
        float value;
        value = Random.Range(0f, 1f);
             if (value < stage_1) return 0;
        else if (value < stage_2) return 1;
        else if (value < stage_3) return 2;
        else return 3 ;        
    }
}
