using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText; // Tham chiếu đến UI Text
    public ScoreDisplay scoreDisplay;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore"); // Lấy điểm số đã lưu
        scoreText.text = finalScore.ToString();
    }
}
