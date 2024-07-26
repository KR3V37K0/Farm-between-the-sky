using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeatherSc : MonoBehaviour
{
    public int Wind { get => Wind; private set => Wind = value; }
    public int Heat { get => Heat; private set => Heat = value; }
    public int Rain { get => Rain; private set => Rain = value; }

    [SerializeField] float stage_1, stage_2, stage_3;
    public bool CenerateWeather;
   
    private void Update() { if (CenerateWeather) WeatherChanger(); }

    public void WeatherChanger() 
    {
        Wind = CenrateStageWether();
        Heat = CenrateStageWether();
        Rain = CenrateStageWether();
        Debug.LogWarning(Wind + " " + Heat + " " + Rain);
        CenerateWeather = false;
    }
    private int CenrateStageWether() 
    {
        float value;
        value = Random.Range(0f, 1f);
        Debug.LogWarning(value);
             if (value <= stage_1) return 0;
        else if (value <= stage_2) return 1;
        else if (value <= stage_3) return 2;
        else return 3 ;        
    }
}
