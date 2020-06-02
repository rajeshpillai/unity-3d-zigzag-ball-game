using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI txtCurrentScore;

    public static ScoreManager instance;

    public int score;
    public int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt("score", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Custom function
    void IncrementScore()
    {
        score += 1;
        ScoreChanged();
    }

    public void BonusPoints()
    {
        score += 5;
        ScoreChanged();
    }

    void ScoreChanged()
    {
        txtCurrentScore.text = score.ToString();
    }

    public void StartScore()
    {
        InvokeRepeating("IncrementScore", 0.1f, 0.5f);
    }

    public void StopScore()
    {
        CancelInvoke("IncrementScore");
        PlayerPrefs.SetInt("score", score);

        if (PlayerPrefs.HasKey("highScore"))
        {
            if (score > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", score);
            }
        } else
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }
}
