using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveningChangeOfDay : MonoBehaviour
{
    [Range (0,1)] 
    public float BeginningOfNight, TimeOfDay;
    public float LengthOfDay;
    public int Days;
    public Light LightSun; 

    void FixedUpdate()
    {
        TimeOfDay += Time.fixedDeltaTime / LengthOfDay;
        if (TimeOfDay >= 1)
        {
            TimeOfDay = 0;
            Days++;
        }
                     
       transform.localRotation = Quaternion.Euler (TimeOfDay *360, 0, 0);       
    }
}
