using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class HighScoreManager :MonoBehaviour
{
    public static HighScoreManager instance;

    public int ReadHighScore()
    {
        string filePath = Path.Combine(Application.dataPath, "highscore.txt");
        if (File.Exists(filePath))
        {
            string highScoreString = File.ReadAllText(filePath);
            return int.Parse(highScoreString);
        }
        return 0;
    }
    public void UpdateHighScore(int currentScore, int highScore)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
        SaveHighScore(highScore);
    }
    void SaveHighScore(int highScore)
    {
        string filePath = Path.Combine(Application.dataPath, "highscore.txt");
        File.WriteAllText(filePath, highScore.ToString());
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
