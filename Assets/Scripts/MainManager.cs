using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    /// Initializes the game by instantiating bricks in a grid pattern.
    /// The number of lines and points are determined by the LineCount and pointCountArray respectively.
    /// Each brick is assigned a point value and an event listener for when it is destroyed.
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        // Initialize a grid of bricks with increasing point values.
        // The outer loop is for each line, and the inner loop is for each brick in the line.
        // The position of each brick is calculated based on its position in the grid.
        // The point value of each brick is determined by the pointCountArray.
        // We add an event listener to each brick to call the AddPoint function when it is destroyed.
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                // Calculate the position of the brick based on its position in the grid.
                // The x position is a function of the perLine variable, and the y position is a function of the line number.
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);

                // Instantiate the brick at the calculated position.
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);

                // Set the point value of the brick based on the line number.
                brick.PointValue = pointCountArray[i];

                // Add an event listener to the brick to call the AddPoint function when it is destroyed.
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    /// Handles the game start and restart logic.
    /// If the game hasn't started, pressing the space key will launch the ball and start the game.
    /// If the game is over, pressing the space key will restart the current scene.
    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    /// Adds a point to the player's score, and updates the score text.
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }


    /// Sets the game over flag to true and activates the game over text.
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
