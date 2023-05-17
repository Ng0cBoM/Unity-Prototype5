using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public static GameManager instance;
    public GameObject crosshair;

    public bool isGameActive;
    public bool isGamePaused;
    public bool isBonus;

    private float spawnRate = 1.0f;
    private int score;
    public float timeLeft;
    public int bulletsLeft;

    public bool isPowerUp;
    public int onTargetCount;

    void Start()
    {
        UIManager.instance.UIStartGame();
        crosshair.SetActive(false);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        isGamePaused = false;
        isBonus = false;
        isPowerUp = false;
        score = 0;
        timeLeft = 40;
        onTargetCount = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        BulletReload();

        UIManager.instance.UIInGame();
        UIManager.instance.SetFillBulletAmount(6);
        crosshair.SetActive(true);
    }
    
    public void GameOver()
    {
        UIManager.instance.UIGameOver();
        isGameActive = false;
        crosshair.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UIManager.instance.UISetScoreText(score);
    }

    public void UpdateBullet()
    {
        bulletsLeft -= 1;
        UIManager.instance.SetFillBulletAmount(bulletsLeft);
        UIManager.instance.UISetBulletText(bulletsLeft);
    }
    public void BulletReload()
    {
        bulletsLeft = 6;
        UIManager.instance.UISetBulletText(bulletsLeft);
        UIManager.instance.SetFillBulletAmount(bulletsLeft);
    }

    public void PauseAndResumGame()
    {

        if (isGamePaused)
        {
            Time.timeScale = 1;
            UIManager.instance.UIGameResume();
            isGamePaused = false;
            crosshair.SetActive(true);
        }
        else
        {
            UIManager.instance.UIGamePause();
            isGamePaused = true;
            Time.timeScale = 0;
            crosshair.SetActive(false);
        }
    }
    private void Update()
    { 
        
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            UIManager.instance.UISetTimeText(Mathf.Round(timeLeft));
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PauseAndResumGame();
            }
            if (timeLeft <= 0) GameOver();
        } 
    }

    public void StartBonus()
    {
        UIManager.instance.UISetBulletBonus();
        isBonus = true;
        timeLeft += 5;
        UIManager.instance.bonusSlider.gameObject.SetActive(true);
        StartCoroutine(TimeBonus());
        
    }

    public void EndBonus()
    {
        isBonus = false;
        BulletReload();
        UIManager.instance.UISetBulletText(bulletsLeft);
        UIManager.instance.bonusSlider.gameObject.SetActive(false);
    }
    IEnumerator TimeBonus()
    {
        float timeBonusLeft = 3f;

        while (timeBonusLeft >= 0)
        {
            timeBonusLeft -= Time.deltaTime;
            UIManager.instance.UISetBonusSlider(timeBonusLeft);
            yield return null;
        }
        EndBonus();
    }

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
}
