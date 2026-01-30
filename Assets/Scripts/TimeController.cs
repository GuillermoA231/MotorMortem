using UnityEngine;

public class TimeController : ITimeController
{
    public void PauseTime() => Time.timeScale = 0f;
    public void ResumeTime() => Time.timeScale = 1f;
}