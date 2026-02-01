using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{

    private int score = 0; 
    public float timeInterval = 0.5f;
    public float timer = 0f;
    public bool isTimerActive = true;
    
    public event System.Action<int> OnScoreChanged;
    public event System.Action<int> OnCounterChanged;


    private void Update() {
        if(isTimerActive)
        {
            timer += Time.deltaTime;

            if(timer > timeInterval)
            {
                score += 1;
                timer = 0f;
            }
        }
        OnCounterChanged?.Invoke(score);
    }

    public void ShowScore()
    {
        OnScoreChanged?.Invoke(score);
    }

    public void SetScore(bool isAdding,int score)
    {
        if(isAdding)
            this.score += score;
        else
            this.score = score;
    }
}
