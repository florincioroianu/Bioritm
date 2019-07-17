using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CurrentDayDropdown : MonoBehaviour
{
    List<string> days = new List<string>();
    public Dropdown currentDayDropdown;
    public Text selectedDay;
    public static int currentDay;
    
    public void DropDownIndexChange(int index)
    {
        if (index >= 0 && index < days.Count)
        {
            selectedDay.text = days[index];
           // Debug.Log(days[index]);
            currentDay = textManager.Convert(days[index]);
        }
    }

    private void Awake()
    {
        days.Add("Ziua");
        for (int i = 1; i <= 31 ; i++)
            days.Add(i.ToString());
        currentDayDropdown = GetComponent<Dropdown>();
        currentDayDropdown.ClearOptions();
        currentDayDropdown.AddOptions(days);
    }
    
}