using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] private TMP_Text countBall;

    public static UIManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    public void OnclickStart()
    {
        Time.timeScale = 1;
    }
    public void OpenGamePanel()
    {
        startPanel.SetActive(true);
    }
    public void CloseGamePanel()
    {
        startPanel.SetActive(false);
    }
    public void OpenGame1Panel()
    {
        startPanel.SetActive(true);
    }
    public void CloseGame1Panel()
    {
        startPanel.SetActive(false);
    }

    public void UpdateBallValue(float ballCount)
    {
        countBall.text = ballCount.ToString();
    }

}
