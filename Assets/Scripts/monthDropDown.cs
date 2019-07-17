using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class monthDropDown : MonoBehaviour
{
    List<string> month = new List<string>();
    public Dropdown birthMonthDropdown;
    public Text selectedMonth;
    public static int birthMonth;

    public void DropDownIndexChange(int index)
    {
        if (index >= 0 && index < month.Count)
        {
            selectedMonth.text = month[index];
           // Debug.Log(birthMonthDropdown.value);
            birthMonth = birthMonthDropdown.value;
        }
    }

    public void Awake()
    {
        month.Add("Luna nasterii"); month.Add("Ianuarie"); month.Add("Februarie"); month.Add("Martie"); month.Add("Aprilie"); month.Add("Mai"); month.Add("Iunie"); month.Add("Iulie"); month.Add("August"); month.Add("Septembrie"); month.Add("Octombrie"); month.Add("Noiembrie"); month.Add("Decembrie");

        birthMonthDropdown = GetComponent<Dropdown>();
        birthMonthDropdown.ClearOptions();
        birthMonthDropdown.AddOptions(month);
    }
}