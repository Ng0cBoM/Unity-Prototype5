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
    public TextMeshProUGUI livesText;
    public Button restartButton;

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

    public void UIStartGame()
    {
        inforScreen.gameObject.SetActive(false);
        gameoverScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
    }

    public void UIInGame()
    {
        titleScreen.gameObject.SetActive(false);
        inforScreen.gameObject.SetActive(true);
    }

    public void UIGameOver() 
    {
        gameoverScreen.gameObject.SetActive(true);
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
}
