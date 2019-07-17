using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class dayDropDown : MonoBehaviour
{
    List<string> days = new List<string>();
    public Dropdown birthDayDropdown;
    public Text selectedDay;
    public static int birthDay;

    public void DropDownIndexChange(int index)
    {
        if (index >= 0 && index < days.Count)
        {
            selectedDay.text = days[index];
           // Debug.Log(birthDayDropdown.value);
            birthDay = textManager.Convert(days[index]);
        }
    }

    public void Awake()
    {
        days.Add("Ziua nasterii");
        for (int i = 1; i <= 31; i++)
            days.Add(i.ToString());
        birthDayDropdown = GetComponent<Dropdown>();
        birthDayDropdown.ClearOptions();
        birthDayDropdown.AddOptions(days);
    }
}