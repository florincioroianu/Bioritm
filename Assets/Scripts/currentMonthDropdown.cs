using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class currentMonthDropdown : MonoBehaviour
{
    List<string> month = new List<string>();
    public Dropdown CurrentMonthDropdown;
    public Text selectedMonth;
    public static int currentMonth;

    public void DropDownIndexChange(int index)
    {
        if (index >= 0 && index < month.Count)
        {
            selectedMonth.text = month[index];
           // Debug.Log(CurrentMonthDropdown.value);
            currentMonth = CurrentMonthDropdown.value;
        }
    }

    private void Awake()
    {
        month.Add("Luna"); month.Add("Ianuarie"); month.Add("Februarie"); month.Add("Martie"); month.Add("Aprilie"); month.Add("Mai"); month.Add("Iunie"); month.Add("Iulie"); month.Add("August"); month.Add("Septembrie"); month.Add("Octombrie"); month.Add("Noiembrie"); month.Add("Decembrie");
        CurrentMonthDropdown = GetComponent<Dropdown>();
        CurrentMonthDropdown.ClearOptions();
        CurrentMonthDropdown.AddOptions(month);
    }
}