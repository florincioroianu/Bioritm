using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class yearDropDown : MonoBehaviour
{
    List<string> years = new List<string>();
    public Dropdown birthYearDropDown;
    public Text selectedYear;
    public static int birthYear;

    public void DropDownIndexChange(int index)
    {
        if (index >= 0 && index < years.Count)
        {
            selectedYear.text = years[index];
            birthYear = textManager.Convert(years[index]);
           // Debug.Log(birthYear);
        }
    }

    private void Awake()
    {
        years.Add("Anul nasterii");
        for (int i = 2019; i >= 1900; i--)
        {
            years.Add(i.ToString());
        }
        birthYearDropDown = GetComponent<Dropdown>();
        birthYearDropDown.ClearOptions();
        birthYearDropDown.AddOptions(years);
    }
}
