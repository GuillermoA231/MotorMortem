using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;

    public static Action onGamePaused;
    public static Action onGameResumed;

    private bool isPaused;
    private bool canPause = true;

    private GameInputAction input;

    private void Awake()
    {
        input = new GameInputAction();

        // Subscribe to Pause action
        Debug.Log(GameManager.CurrentGameState);
        input.Player.Pause.performed += OnPause;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void OnDestroy()
    {
        input.Player.Pause.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (GameManager.CurrentGameState == GameState.GAME && canPause)
        {
            TogglePause();
        }
        if (GameManager.CurrentGameState == GameState.PAUSE && canPause)
        {
            TogglePause();
        }
    }

    public void PauseTime() => Time.timeScale = 0f;

    public void ResumeTime() => Time.timeScale = 1f;

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            uIManager.GamePausedCallback();
            PauseButtonCallback();
        }
        else
        {
            Time.timeScale = 1;
            uIManager.GameResumedCallback();
            ResumeButtonCallback();
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void CanPause(bool check)
    {
        canPause = check;
    }

    public void PauseButtonCallback()
    {
        onGamePaused?.Invoke();
    }

    public void ResumeButtonCallback()
    {
        onGameResumed?.Invoke();
    }
}
