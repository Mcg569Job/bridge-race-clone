using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus { Null, Playing, GameOver, Win }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public GameStatus GameStat;
    public Player Player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        Play();
    }

    public void Play()
    {
        GameStat = GameStatus.Playing;
         Player.transform.position = LevelManager.Instance.currentLevelSettings.SpawnPoint.position;
         Player.ResetPlayer();
    }

    public bool IsPlaying() { return GameStat == GameStatus.Playing; }

    public void GameOver()
    {
        if (GameStat == GameStatus.GameOver)
            return;

        GameStat = GameStatus.GameOver;
        UIManager.Instance.ActivateGameOverPanel(true);
    }
    public void Win()
    {
        if (GameStat == GameStatus.Win)
            return;

        GameStat = GameStatus.Win;
        UIManager.Instance.ActivateWinPanel(true);
    }

    public void NextLevel()
    {
        UIManager.Instance.ActivateWinPanel(false);
        LevelManager.Instance.NextLevel();       
        Play();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

}
