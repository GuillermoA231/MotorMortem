using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TimeController timeController;
    // private ITimeController timeController;
    private IDeatheable deathPlayer;

    private void Awake()
    {
        timeController = new TimeController();
        deathPlayer = playerHealth;
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
        timeController.PauseTime();
        scoreManager.ShowScore();
    }

    public void RestartGame()
    {
        playerHealth.ResetHealth();
        timeController.ResumeTime();
        scoreManager.SetScore(false,0);
        SceneManager.LoadScene("SampleScene");
    }


    //// My own stuff
    /// public static GameManager instance;


    public static GameManager instance;
    
    [Header("Actions")]
    public static GameState CurrentGameState { get; private set; }

    private ScoreManager scoreManagerInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);
    }

    public void StartGame() => SetGameState(GameState.GAME);
    public void Menu() => SetGameState(GameState.MENU);
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