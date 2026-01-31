using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager playerScore;
    [SerializeField] private TMP_Text scoreCounter;
    [SerializeField] private TMP_Text scoreText;

    private void Start() {
        scoreText.text = "";
        scoreCounter.text = "";
    }
    
    private void OnEnable()
    {
        playerScore.OnScoreChanged += UpdateScoreDisplay;
        playerScore.OnCounterChanged += UpdateCounterDisplay;
    }
    
    private void OnDisable()
    {
        playerScore.OnScoreChanged -= UpdateScoreDisplay;
        playerScore.OnCounterChanged -= UpdateCounterDisplay;
    }
    
    private void UpdateScoreDisplay(int score)
    {
        scoreText.text = $"Score: {score}";
    }
    private void UpdateCounterDisplay(int counter)
    {
        scoreCounter.text = $"Time: {counter}";
    }
}
