using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TimeController timeController;
    [SerializeField] private UIManager UIManager;

    [Header("Actions")]
    public static GameState CurrentGameState { get; private set; }

    private ScoreManager scoreManagerInstance;
    // private ITimeController timeController;
    private IDeatheable deathPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        deathPlayer = playerHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;

        Menu();
    }
    private void OnEnable()
    {
        deathPlayer.OnDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        deathPlayer.OnDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        Lose();
        scoreManager.ShowScore();
    }

    public void RestartGame()
    {
        playerHealth.ResetHealth();
        scoreManager.SetScore(false, 0);
        Menu();
    }


    //// My own stuff
    /// public static GameManager instance;


    public static GameManager instance;


    public void StartGame()
    {
        SetGameState(GameState.GAME);
    }
    public void PauseGame() => SetGameState(GameState.PAUSE);
    public void Menu()
    {
        SetGameState(GameState.MENU);
    }
    public void Lose() => SetGameState(GameState.GAMEOVER);
    public void SetGameState(GameState gameState)
    {
        CurrentGameState = gameState;

        switch (gameState)
        {
            case GameState.GAME:
                Time.timeScale = 1f;
                break;

            case GameState.PAUSE:
            case GameState.MENU:
            case GameState.GAMEOVER:
                Time.timeScale = 0f;
                break;
        }

        IEnumerable<IGameStateListener> gameStateListeners =
        FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
        .OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.GameStateChangedCallback(gameState);

    }

}



public interface IGameStateListener
{
    void GameStateChangedCallback(GameState gameState);
}