using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private ITimeController timeController;
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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        playerHealth.ResetHealth();
        timeController.ResumeTime();
    }
}