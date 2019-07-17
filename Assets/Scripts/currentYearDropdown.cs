using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class currentYearDropdown : MonoBehaviour
{
    List<string> years = new List<string>();
    public Dropdown CurrentYearDropdown;
    public Text selectedYear;
    public static int currentYear;

    public void DropDownIndexChange(int index)
    {
        if (index >= 0 && index < years.Count)
        {
            selectedYear.text = years[index];
           // Debug.Log(years[index]);
            currentYear = textManager.Convert(years[index]);
        }
    }

    private void Awake()
    {
        years.Add("Anul");
        for (int i = 2019; i >= 1900;i--)
        {
            years.Add(i.ToString());
        }
        CurrentYearDropdown = GetComponent<Dropdown>();
        CurrentYearDropdown.ClearOptions();
        CurrentYearDropdown.AddOptions(years);
    }
}
