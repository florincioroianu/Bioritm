using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class textManager : MonoBehaviour
{ 
    public TextMeshProUGUI rText;
    public TextMeshProUGUI gText;
    public TextMeshProUGUI bText;

    public void changeTextColor(float r, float g, float b)
    {
        rText.text = "R: " + r;
        gText.text = "G: " + g;
        bText.text = "B: " + b;
    }

    public static int Convert(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }
}
