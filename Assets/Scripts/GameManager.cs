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
    public GameObject titleScreen;
    public GameObject inforScreen;
    public GameObject gameoverScreen;
    public GameObject pauseScreen;


    private float spawnRate = 1.0f;
    private int score;
    public int lives;
    public bool isGameActive;
    public bool isGamePaused;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    
    void Start()
    {
        inforScreen.gameObject.SetActive(false);
        gameoverScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        isGamePaused = false;
        lives = 3;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.gameObject.SetActive(false);
        inforScreen.gameObject.SetActive(true);
    }
    
    public void GameOver()
    {
        gameoverScreen.gameObject.SetActive(true);
        isGameActive = false;
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
        scoreText.text = "Score: "+ score;
    }

    public void UpdateLives(int liveUpdate)
    {
        lives -= liveUpdate;
        livesText.text = "Lives: " + lives;
       
    }

    public void PauseAndResumGame()
    {

        if (isGamePaused)
        {
            Time.timeScale = 1;
            pauseScreen.gameObject.SetActive(false);
            inforScreen.gameObject.SetActive(true);
            isGamePaused = false;
        }
        else
        {
            pauseScreen.gameObject.SetActive(true);
            inforScreen.gameObject.SetActive(false);
            isGamePaused = true;
            Time.timeScale = 0;            
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
