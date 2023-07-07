using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] private TMP_Text countBall;
    [SerializeField] GameObject restartPanel;
    [SerializeField] GameObject settingsPanel;

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
    public void OpenRestartPanel()
    {
       restartPanel.SetActive(true);
    }
    public void CloseRestartPanel()
    {
        restartPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
