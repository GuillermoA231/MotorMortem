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
        timeController = new TimeController();
        deathPlayer = playerHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        timeController.PauseTime();
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
        timeController.PauseTime();
        scoreManager.ShowScore();
    }

    public void RestartGame()
    {
        playerHealth.ResetHealth();
        timeController.ResumeTime();
        scoreManager.SetScore(false, 0);
        Menu();
    }


    //// My own stuff
    /// public static GameManager instance;


    public static GameManager instance;


    public void StartGame() {
        SetGameState(GameState.GAME);
        Debug.Log("startingame");
        timeController.ResumeTime();
    }
    public void PauseGame() => SetGameState(GameState.PAUSE);
    public void Menu() => SetGameState(GameState.MENU);
    public void Lose() => SetGameState(GameState.GAMEOVER);
    public void SetGameState(GameState gameState)
    {
        CurrentGameState = gameState;

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