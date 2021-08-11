using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance = null;

    [Header("Levels")]
    [SerializeField] private LevelSettings[] levels;
    [HideInInspector] public int currentLevel;
    [HideInInspector] public LevelSettings currentLevelSettings;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        ReloadLevel();
    }

    public void ActiveCurrentLevel()
    {
        foreach (LevelSettings lev in levels)
            if (lev.gameObject.activeSelf)
                lev.gameObject.SetActive(false);

        int levelIndex = currentLevel;
        if (levelIndex > levels.Length - 1)
            levelIndex = currentLevel % (levels.Length);


        LevelSettings level = levels[levelIndex];
        level.gameObject.SetActive(true);

        currentLevelSettings = level;
        level.GetObjects();

        UIManager.Instance.UpdateLevelText(currentLevel + 1);
    }

    public void ReloadLevel()
    {
        currentLevel = PlayerPrefs.GetInt("level");
        ActiveCurrentLevel();
    }

    public void NextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        ActiveCurrentLevel();
    }

}
