using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;

    public static Action onGamePaused;
    public static Action onGameResumed;

    private GameInputAction input;

    private bool isPaused = false;
    private bool canPause = true;

    private void Awake()
    {
        Debug.Log("TimeController Awake: " + gameObject.name);
        input = new GameInputAction();
    }

    private void OnEnable()
    {
        // Enable input and subscribe
        input.Enable();
        input.Player.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        // Unsubscribe and disable to prevent stacking
        input.Player.Pause.performed -= OnPause;
        input.Disable();
    }

    private void OnDestroy()
    {
        // Extra safety (optional, but good practice)
        if (input != null)
        {
            input.Player.Pause.performed -= OnPause;
            input.Disable();
        }
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!canPause)
            return;

        if (GameManager.CurrentGameState == GameState.GAME ||
            GameManager.CurrentGameState == GameState.PAUSE && canPause)
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            canPause = false;
            GameManager.instance.PauseGame();
            PauseButtonCallback();
        }
        else
        {
            canPause = true;
            GameManager.instance.StartGame();
            ResumeButtonCallback();
        }
    }


    public bool IsPaused()
    {
        if (GameManager.CurrentGameState == GameState.PAUSE ||
            GameManager.CurrentGameState == GameState.GAMEOVER ||
            GameManager.CurrentGameState == GameState.MENU)
        {
            canPause = false;
            return isPaused;
        }

        // Force unpaused in other states
        isPaused = false;
        return false;
    }

    public void CanPause(bool check)
    {
        canPause = check;
    }

    private void PauseButtonCallback()
    {
        onGamePaused?.Invoke();
    }

    private void ResumeButtonCallback()
    {
        onGameResumed?.Invoke();
    }
}
