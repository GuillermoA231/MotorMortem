using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    private List<GameObject> panels = new List<GameObject>();

    private void Awake()
    {
        panels.AddRange(new GameObject[]
        {
            menuPanel,
            gamePanel,
            gameOverPanel,
            pausePanel
        });

        
        TimeController.onGamePaused += GamePausedCallback;
        TimeController.onGameResumed += GameResumedCallback;

    }
    private void OnDestroy()
    {

        TimeController.onGamePaused -= GamePausedCallback;
        TimeController.onGameResumed -= GameResumedCallback;
    }
    public void GamePausedCallback()
    {
        pausePanel.SetActive(true);
    }
    public void GameResumedCallback()
    {
        pausePanel.SetActive(false);
    }

    private void ShowPanel(GameObject panel, bool hidePreviousPanels = true)
    {
        if (hidePreviousPanels)
        {
            foreach (GameObject p in panels)
                p.SetActive(p == panel);
        }
        else
        {
            panel.SetActive(true);
        }

    }


    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameOverPanel);
                break;
            case GameState.PAUSE:
                ShowPanel(pausePanel);
                break;
        }
    }
}
