using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainMenuPanel, InGamePanel, EndGamePanel;
    public Text levelText,endGameLevelText;
    public Button startButton;

    private void Start()
    {
        Debug.Log(LevelManager.Instance.CurrentLevel);
        levelText.text = "LEVEL " + (PlayerPrefs.GetInt("Level") + 1) .ToString();
        endGameLevelText.text = "LEVEL " + (PlayerPrefs.GetInt("Level") + 1) .ToString();
    }

    #region Button Functions
    public void StartButton()
    {
        StateManager.Instance.state = State.InGame;
    }
    public void RestartButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.CurrentLevel);
    }
    #endregion

    #region Events 
    public void InGameEvent()
    {
        mainMenuPanel.SetActive(false);
        InGamePanel.SetActive(true);

    }
    public void EndGameEvent()
    {
        StartCoroutine(EndGame());
    }
    #endregion

    public IEnumerator EndGame()
    {
        
        InGamePanel.SetActive(false);
        EndGamePanel.SetActive(true);
        yield return new WaitForSeconds(1.5F);
        LevelManager.Instance.ChangeLevel("LEVEL " + (LevelManager.Instance.CurrentLevel + 1));
    }
}
