using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using TMPro;


public class gameManager : MonoBehaviour
{
    public GameObject dateCanvas;
    public GameObject rezultatCanvas;
    public GameObject menuCanvas;
    public GameObject documentatieCanvas;
    public GameObject metodaCalculCanvas;
    public GameObject bioritmInfoCanvas;
    public GameObject introducereCanvas;
    public GameObject ceSuntBioritmurileCanvas;
    public GameObject cele3BioritmuriCanvas;
    public GameObject ciclurileBioritmuluiCanvas;
    public GameObject luminaCanvas;
    public GameObject albedoCanvas;
    public GameObject psihologiaCulorilorCanvas;
    public GameObject concluzieCanvas;
    public GameObject errorText;
    public GameObject gamesCanvas;
    public textManager RGBtext;
    public Image img;
    public static bool ok = false;
    public static bool ok1 = false;
    public static Color color;
    Color color2 = color;
    public int r, g, b;
    int bd, bm, by, cd, cm, cy;
    long t;
    bool isFirstTime = true;
    float emotional, physical, intellectual;
    DateTime birthDate;
    DateTime currentDate;
    List<int> monthDays = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private void Start()
    {
        if (ok||ok1)
        {
            Remind();
        }
        else
        {
            menuCanvas.SetActive(true);
            dateCanvas.SetActive(false);
            rezultatCanvas.SetActive(false);
            errorText.SetActive(false);
            documentatieCanvas.SetActive(false);
            metodaCalculCanvas.SetActive(false);
            bioritmInfoCanvas.SetActive(false);
            introducereCanvas.SetActive(false);
            ceSuntBioritmurileCanvas.SetActive(false);
            cele3BioritmuriCanvas.SetActive(false);
            ciclurileBioritmuluiCanvas.SetActive(false);
            luminaCanvas.SetActive(false);
            albedoCanvas.SetActive(false);
            concluzieCanvas.SetActive(false);
            gamesCanvas.SetActive(false);
            psihologiaCulorilorCanvas.SetActive(false);
            if (isFirstTime)
            {
                color = new Color(UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
                isFirstTime = false;
            }
        }
    }

    public void Remind()
    {
        menuCanvas.SetActive(false);
        dateCanvas.SetActive(false);
        rezultatCanvas.SetActive(false);
        errorText.SetActive(false);
        documentatieCanvas.SetActive(false);
        metodaCalculCanvas.SetActive(false);
        bioritmInfoCanvas.SetActive(false);
        introducereCanvas.SetActive(false);
        ceSuntBioritmurileCanvas.SetActive(false);
        cele3BioritmuriCanvas.SetActive(false);
        ciclurileBioritmuluiCanvas.SetActive(false);
        luminaCanvas.SetActive(false);
        albedoCanvas.SetActive(false);
        concluzieCanvas.SetActive(false);
        gamesCanvas.SetActive(true);
        psihologiaCulorilorCanvas.SetActive(false);
        //RGBtext.changeTextColor(color2.r * 255f, color2.g * 255f, color2.b * 255f);
        //img.GetComponent<Image>().color = color2;
        color = color2;
        ok = false;
        ok1 = false;
        //SceneManager.UnloadScene("Game");
        //SceneManager.UnloadScene("Snake");
    }
    
    void getValues()
    {
        bd = dayDropDown.birthDay;
        bm = monthDropDown.birthMonth;
        by = yearDropDown.birthYear;
        cd = CurrentDayDropdown.currentDay;
        cm = currentMonthDropdown.currentMonth;
        cy = currentYearDropdown.currentYear;
        if (CheckDates() == 0)
        {
            errorText.SetActive(true);
            StartCoroutine(DisableError());
        }
        else
        {
            birthDate = new DateTime(by, bm, bd);
            currentDate = new DateTime(cy, cm, cd);
            Calculeaza();
        }
    }

    int CheckDates()
    {
        if (bd == 0 || bm == 0 || by == 0 || cd == 0 || cm == 0 || cy == 0 || (by % 4 == 0 && bm == 2 && bd > 29) || (by % 4 != 0 && bm == 2 && bd > 28) || (cy % 4 == 0 && cm == 2 && cd > 29) || (cy % 4 != 0 && cm == 2 && cd > 28) || (bm != 2 && bd > monthDays[bm - 1]) || (cm != 2 && cd > monthDays[cm - 1]))
            return 0;
        if (cy > by)
        {
            return 1;
        }
        else if (cy == by)
        {
            if (cm > bm)
            {
                return 1;
            }
            else if (cm == bm)
            {
                if (cd > bd)
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    void Calculeaza()
    {
       t=(long)(currentDate-birthDate).TotalDays;
        emotional = Mathf.Sin((360 * t) / 28) * 100;
        r = (int)Mathf.Floor(emotional);
        if (r < 0)
            r = -r;
        physical = Mathf.Sin((360 * t) / 23) * 100;
        g = (int)Mathf.Floor(physical);
        if (g < 0)
            g = -g;
        intellectual = Mathf.Sin((360 * t) / 33) * 100;
        b = (int)Mathf.Floor(intellectual);
        if (b < 0)
            b = -b;
        Debug.Log(r/255);
        Debug.Log(g/255);
        Debug.Log(b/255);

        color = new Color(r/255f, g/255f, b/255f);
        img.GetComponent<Image>().color = color;
        dateCanvas.SetActive(false);
        rezultatCanvas.SetActive(true);
        RGBtext.changeTextColor(r, g, b);
    }

    public void Restart()
    {
        dateCanvas.SetActive(true);
        rezultatCanvas.SetActive(false);
    }

    void Start(Color color)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartButton()
    {
        menuCanvas.SetActive(false);
        gamesCanvas.SetActive(true);
        rezultatCanvas.SetActive(false);
        //Start(color);
    }

    public void Menu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartHexagon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void StartSanke()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    IEnumerator DisableError()
    {

        yield return new WaitForSeconds(3);

        errorText.SetActive(false);
    }

    public void Calculator()
    {
        menuCanvas.SetActive(false);
        dateCanvas.SetActive(true);
    }

    public void Back()
    {
        menuCanvas.SetActive(true);
        dateCanvas.SetActive(false);
    }

    public void Documentatie()
    {
        menuCanvas.SetActive(false);
        documentatieCanvas.SetActive(true);
    }

    public void Back2()
    {
        documentatieCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void MetodaCalcul()
    {
        documentatieCanvas.SetActive(false);
        metodaCalculCanvas.SetActive(true);
    }

    public void BioritmInfo()
    {
        documentatieCanvas.SetActive(false);
        bioritmInfoCanvas.SetActive(true);
    }

    public void BackCalc()
    {
        metodaCalculCanvas.SetActive(false);
        documentatieCanvas.SetActive(true);
    }

    public void BackInfo()
    {
        bioritmInfoCanvas.SetActive(false);
        documentatieCanvas.SetActive(true);
    }

    public void Introducere()
    {
        bioritmInfoCanvas.SetActive(false);
        introducereCanvas.SetActive(true);
    }

    public void BackIntroducere()
    {
        bioritmInfoCanvas.SetActive(true);
        introducereCanvas.SetActive(false);
    }

    public void CeSuntBioritmurile()
    {
        bioritmInfoCanvas.SetActive(false);
        ceSuntBioritmurileCanvas.SetActive(true);
    }

    public void BackCeSuntBioritmurile()
    {
        bioritmInfoCanvas.SetActive(true);
        ceSuntBioritmurileCanvas.SetActive(false);
    }

    public void Cele3Bioritmuri()
    {
        ceSuntBioritmurileCanvas.SetActive(false);
        cele3BioritmuriCanvas.SetActive(true);
    }

    public void BackCele3Bioritmuri()
    {
        ceSuntBioritmurileCanvas.SetActive(true);
        cele3BioritmuriCanvas.SetActive(false);
    }

    public void CiclurileBioritmului()
    {
        cele3BioritmuriCanvas.SetActive(false);
        ciclurileBioritmuluiCanvas.SetActive(true);
    }

    public void BackCicluriBioritm()
    {
        cele3BioritmuriCanvas.SetActive(true);
        ciclurileBioritmuluiCanvas.SetActive(false);
    }

    public void Lumina()
    {
        psihologiaCulorilorCanvas.SetActive(false);
        luminaCanvas.SetActive(true);
    }

    public void BackLumina()
    {
        psihologiaCulorilorCanvas.SetActive(true);
        luminaCanvas.SetActive(false);
    }

    public void Albedo()
    {
        bioritmInfoCanvas.SetActive(false);
        albedoCanvas.SetActive(true);
    }

    public void BackAlbedo()
    {
        bioritmInfoCanvas.SetActive(true);
        albedoCanvas.SetActive(false);
    }

    public void PsihologiaCulorilor()
    {
        albedoCanvas.SetActive(false);
        psihologiaCulorilorCanvas.SetActive(true);
    }

    public void BackPsihologiaCulorilor()
    {
        albedoCanvas.SetActive(true);
        psihologiaCulorilorCanvas.SetActive(false);
    }

    public void Concluzie()
    {
        concluzieCanvas.SetActive(true);
        rezultatCanvas.SetActive(false);
    }

    public void BackConcluzie()
    {
        concluzieCanvas.SetActive(false);
        rezultatCanvas.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
