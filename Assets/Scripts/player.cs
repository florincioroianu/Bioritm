using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    private Vector3 clickPos;
    private bool mouseDown = false;
    float speed = 1.2f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI scoreDeadText;
    public TextMeshProUGUI recordText;
    public GameObject newRecordText;
    public int score = 0;
    public GameObject GameOverMenuUI;
    public GameObject GameOnUI;
    public GameObject Player;
    public static bool GameIsOver = false;

    private void Start()
    {
        Player.transform.position=new Vector3(0f, .8f, 0f);
        Time.timeScale = 1f;
        GameIsOver = false;
        GameOverMenuUI.SetActive(false);
        GameOnUI.SetActive(true);
        newRecordText.SetActive(false);
        recordText.color = Color.white;
        
    }

    void Update()
    {
        if (GameIsOver)
        {
            GameOver();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Input.mousePosition;
            mouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        scoreDeadText.text = "Scor: " + score.ToString();
        scoreText.text = score.ToString();
    }

    private void FixedUpdate()
    {
        if (mouseDown)
        {
            Vector3 currentMPos = Input.mousePosition;
            float move = clickPos.x - currentMPos.x;

            transform.RotateAround(Vector3.zero, Vector3.forward, move * Time.deltaTime / speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "die")
        {
            GameIsOver = true;
            if (score > PlayerPrefs.GetInt("Highscore"))
                PlayerPrefs.SetInt("Highscore", score);
        }
        if (col.gameObject.tag == "score")
        {
            score++;
            if (score > PlayerPrefs.GetInt("Highscore") && PlayerPrefs.GetInt("Highscore") > 0)
            {
                newRecordText.SetActive(true);
            }
        }
    }

    public void GameOver()
    {
        GameOverMenuUI.SetActive(true);
        bestScoreText.text = "Record: " + PlayerPrefs.GetInt("Highscore").ToString();
        GameOnUI.SetActive(false);
        Time.timeScale = 0f;
    }

     public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Back()
    {
        gameManager.ok1 = true;
        GameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
