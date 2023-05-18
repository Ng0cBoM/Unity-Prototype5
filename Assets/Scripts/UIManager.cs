using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject titleScreen;
    public GameObject inforScreen;
    public GameObject gameoverScreen;
    public GameObject pauseScreen;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI highScoreText;
    public Button restartButton;
    public Slider bonusSlider;

    public Image bulletAmountFill;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UIStartGame(int highScore)
    {
        inforScreen.gameObject.SetActive(false);
        gameoverScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
        UISetHighScoreText(highScore);
        highScoreText.gameObject.SetActive(true);
        bonusSlider.gameObject.SetActive(false);
    }

    public void UIInGame()
    {
        titleScreen.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        inforScreen.gameObject.SetActive(true);
    }

    public void UIGameOver() 
    {
        inforScreen.gameObject.SetActive(false);
        gameoverScreen.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
    }

    public void UIGamePause()
    {
        pauseScreen.gameObject.SetActive(true);
        inforScreen.gameObject .SetActive(false);
    }

    public void UIGameResume()
    {
        pauseScreen.gameObject.SetActive(false);
        inforScreen.gameObject .SetActive(true);
    }
    public void UISetScoreText(int score) 
    {
        scoreText.text = "Score: " + score;
    }

    public void UISetBulletText(int bulletsLeft)
    {
        bulletText.text = ""+bulletsLeft;
    }
    public void UISetBulletBonus()
    {
        bulletText.text = "∞";
    }

    public void UISetBonusSlider(float value)
    {
        bonusSlider.value = value;
    }

    public void UISetTimeText(float time)
    {
        timeText.text = "Time: "+time;
    }
    public void UISetHighScoreText(int highScore)
    {
        highScoreText.text = "HIGH SCORE: " + highScore;
    }

    public void UIBlinkingTimeTex()
    {
        StartCoroutine(BlinkText());
    }
    IEnumerator BlinkText()
    {
        while (GameManager.instance.isPowerUp)
        {
            timeText.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);

            timeText.color = Color.green;
            yield return new WaitForSeconds(0.1f);
        }
        timeText.color = Color.white;
    }

    public void SetFillBulletAmount(int bulletsLeft)
    {
        bulletAmountFill.fillAmount = ((float)bulletsLeft / 6f);
    }
}
