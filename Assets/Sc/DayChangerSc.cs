using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChangerSc : MonoBehaviour
{
    [Range(0, 1)]
    public float BeginningOfNight; // �������� ����� 0 � 1, ����������� ������ ����
    public float TimeOfDay;
    public float LengthOfDay; // ����������������� ������� ��� (������� ���� � ����)
    public int Days;

    void FixedUpdate()
    {
        TimeOfDay += Time.fixedDeltaTime / LengthOfDay;
        if (TimeOfDay >= 1)
        {
            TimeOfDay = 0;
            Days++;
        }

        // ���������� ������� ���� (���� ��� ����) � ��������� ���� ��������
        float currentPhaseTime;
        if (TimeOfDay < BeginningOfNight)
        {
            // ������� ����
            float lengthOfDayPhase = BeginningOfNight * LengthOfDay;
            currentPhaseTime = TimeOfDay / BeginningOfNight;
            transform.localRotation = Quaternion.Euler(currentPhaseTime * 180, 0, 0);
        }
        else
        {
            // ������ ����
            float lengthOfNightPhase = (1 - BeginningOfNight) * LengthOfDay;
            currentPhaseTime = (TimeOfDay - BeginningOfNight) / (1 - BeginningOfNight);
            transform.localRotation = Quaternion.Euler(180 + currentPhaseTime * 180, 0, 0);
        }
        Debug.Log(currentPhaseTime);
    }
}

