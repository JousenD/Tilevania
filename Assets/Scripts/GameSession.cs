using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    // Use this for initialization

    [SerializeField] int playerLives = 3;
    private int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
            Debug.Log("Singleton Destroyed");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score.ToString();
    }


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = "Lives: " + playerLives.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
