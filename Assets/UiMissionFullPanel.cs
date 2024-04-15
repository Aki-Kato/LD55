using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMissionFullPanel : MonoBehaviour
{
    public TMP_Text JobText, Reward, Progress, Time;
    
    public void SetText(string description, int workLeft, int totalWork, int reward, float TimeLeft)
    {
        JobText.text = description;
        Reward.text = $"{reward}";
        Progress.text = $"{workLeft}/{totalWork}";
        Time.text = $"{FormatTime(TimeLeft)}";
    }
    
    private string FormatTime(float seconds)
    {
        int totalSeconds = (int)seconds;  // Convert float to int to drop any fractional part
        int minutes = totalSeconds / 60;  // Get the number of minutes
        int remainingSeconds = totalSeconds % 60;  // Get the remaining seconds

        // Format the time as MM:SS
        return string.Format("{0:D2}:{1:D2}", minutes, remainingSeconds);
    }
}
