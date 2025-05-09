using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public bool gameOver;
    public string highScoreName;
    public bool mainGameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    /// Initializes the game state by setting the game over flag to false and displaying the high score.
    /// If no high score name is set, defaults the name to "nobody".
    void Start()
    {
        gameOver = false;
        highScoreText.text = $"High Score : {DataManager.Instance.playerHighScoreName} : {DataManager.Instance.playerHighScore}";
        if (DataManager.Instance.playerHighScoreName == "")
        {
            DataManager.Instance.playerHighScoreName = "nobody";
        }
    }



    /// Checks every frame if the game is over by checking the game over flag in MainManager.
    /// If the game is over and the game over flag in this script is false, sets the high score
    /// and sets the game over flag to true.
    void Update()
    {
        mainGameOver = GameObject.Find("MainManager").GetComponent<MainManager>().m_GameOver;
        score = GameObject.Find("MainManager").GetComponent<MainManager>().m_Points;
        if (mainGameOver == true && gameOver == false)
        {
            SetHighScore();
            gameOver = true;
        }
    }

    // ------------  SCORE SETTING  --------------

    /// Updates the high score and high score name if the current score is higher.
    /// The high score name is only updated if the current player's name is different
    /// from the current high score name.
    /// Should be called when the game is over.
    private void SetHighScore()
    {
        if (score > DataManager.Instance.playerHighScore)
        {
            DataManager.Instance.playerHighScore = score;
            highScoreName = DataManager.Instance.playerName;

            if (DataManager.Instance.playerName != DataManager.Instance.playerHighScoreName)
            {
                DataManager.Instance.playerHighScoreName = DataManager.Instance.playerName;
            }

            highScoreText.text = $"Best score : {DataManager.Instance.playerHighScoreName} : {DataManager.Instance.playerHighScore}";
        }
    }
}
