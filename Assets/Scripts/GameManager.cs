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

    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;
    public bool isGamePaused;

    void Start()
    {
        UIManager.instance.UIStartGame();
        crosshair.SetActive(false);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        isGamePaused = false;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UIManager.instance.UIInGame();
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
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            PauseAndResumGame();
        }
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
