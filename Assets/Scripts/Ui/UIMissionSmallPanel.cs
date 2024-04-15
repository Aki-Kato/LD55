using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMissionSmallPanel : MonoBehaviour
{
    public TMP_Text TopText, BottomText;
    
    public void SetText(int workLeft, int totalWork, float TimeLeft){
        TopText.text = $"{workLeft}/{totalWork}";
        BottomText.text = $"{FormatTime(TimeLeft)}";
    }
    
    private string FormatTime(float seconds)
    {
        int totalSeconds = (int)seconds;
        int minutes = totalSeconds / 60;
        int remainingSeconds = totalSeconds % 60;
        
        return string.Format("{0:D2}:{1:D2}", minutes, remainingSeconds);
    }
}
