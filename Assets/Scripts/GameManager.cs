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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    /// Initializes the game state by setting the game over flag to false and displaying the high score.
    /// If no high score name is set, defaults the name to "nobody".
    void Start()
    {
        gameOver = false;
        highScoreText.text = $"High Score: {DataManager.Instance.playerHighScoreName} : {DataManager.Instance.playerHighScore}";
        if (DataManager.Instance.playerHighScoreName == "")
        {
            DataManager.Instance.playerHighScoreName = "nobody";
        }
    }


    /// Handles the game state transitions based on user input.
    /// If the left mouse button is released, sets the game over flag to false and increments the score.
    /// If the right mouse button is released, sets the game over flag to true, updates the high score
    /// If the current score is higher, and resets the current score.
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            gameOver = false;
            UpdateScore(1);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            SetHighScore();
            gameOver = true;
            ResetScore();
        }
    }

    // ------------  SCORE EDITING  ------------
    
    /// Updates the player's score by adding the specified amount.
    /// If the game is not over, increments the score, updates the score display,
    /// and sets the player's current score in the DataManager.
    private void UpdateScore(int scoreToAdd)
    {
        if (gameOver == false)
        {
            score += scoreToAdd;
            scoreText.text = $"Score: {score}";
            DataManager.Instance.playerScore = score;
        }
    }

    /// Resets the current score to 0 and updates the score display.
    private void ResetScore()
    {
        score = 0;
        scoreText.text = $"Score: {score}";
    }

    // ------------  SCORE SETTING  --------------

    /// Updates the high score and high score name if the player's current score exceeds the current high score.
    /// If the player's name is not the same as the current high score name, updates the high score name.
    /// If the player's current score is higher than the current high score, updates the high score and
    /// displays the updated high score and name.
    private void SetHighScore()
    {
        if (score > DataManager.Instance.playerHighScore)
        {
            highScore = score;
            highScoreName = DataManager.Instance.playerName;

            if (DataManager.Instance.playerName != DataManager.Instance.playerHighScoreName)
            {
                DataManager.Instance.playerHighScoreName = DataManager.Instance.playerName;
            }

            if (DataManager.Instance.playerScore > DataManager.Instance.playerHighScore)
            {
                DataManager.Instance.playerHighScore = highScore;
            }

            highScoreText.text = $"Best score : {DataManager.Instance.playerHighScoreName} : {DataManager.Instance.playerHighScore}";
        }
    }
}
