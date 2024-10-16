using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreTextUI;

    public static int score = 0;

    public string returnScore() => score.ToString();

    private void Awake()
    {
        // Giữ đối tượng này khi chuyển scene
        DontDestroyOnLoad(gameObject);
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreTextUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void UpdateScoreTextUI()
    {
        string scoreText = string.Format("{0:000000}" , score);
        scoreTextUI.text = scoreText;
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreTextUI();
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.Save();
    }
}
