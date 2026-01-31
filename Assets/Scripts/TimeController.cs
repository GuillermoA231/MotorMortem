// using UnityEngine;

// public class TimeController : ITimeController
// {
//     public void PauseTime() => Time.timeScale = 0f;
//     public void ResumeTime() => Time.timeScale = 1f;
// }


using UnityEngine;
using System;
public class TimeController : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    
    public static Action onGamePaused;
    public static Action onGameResumed;
    private bool isPaused;
    private bool canPause = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.CurrentGameState == GameState.GAME)
        {
            togglePause();
        }
    }

    public void PauseTime() => Time.timeScale = 0f;
    public void ResumeTime() => Time.timeScale = 1f;
    public void togglePause()
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
        // Time.timeScale = 0;
        onGamePaused?.Invoke();
    }
    public void ResumeButtonCallback()
    {
        
        // Time.timeScale = 1;
        onGameResumed?.Invoke();
    }
}
