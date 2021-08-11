using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }


    [Header("Panels")]

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    [Header("Texts")]
    [SerializeField] private Text levelText;

    public void ActivateGameOverPanel(bool activate) => gameOverPanel.SetActive(activate);
    public void ActivateWinPanel(bool activate) => winPanel.SetActive(activate);

    public void UpdateLevelText(int level) => levelText.text = "LEVEL: " + level.ToString();

}
