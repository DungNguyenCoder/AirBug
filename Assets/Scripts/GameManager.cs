using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _score = 0;
    private float _time = 0f;
    private bool _isGameRunning = true;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI inGameScore;
    [SerializeField] private TextMeshProUGUI playTime;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject scoreGamePanel;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1f;
        _time = 0f;
        _isGameRunning = true;
    }
    public void AddScore(int amount)
    {
        _score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + _score;
            inGameScore.text = "Score: " + _score;
        }
    }

    public void ShowEndGame()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        scoreGamePanel.SetActive(false);
        Debug.Log("het game " + _score);
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    private void Update()
    {
        if (!_isGameRunning)
        {
            return;
        }
        Debug.Log("Real time: " + Time.time + " | Level Time: " + Time.timeSinceLevelLoad + " | TimeScale: " + Time.timeScale);
        float seconds = Time.timeSinceLevelLoad;

        if (playTime != null)
        {
            playTime.text = $"Time: {seconds:F1}s";
        }
        
    }
}
