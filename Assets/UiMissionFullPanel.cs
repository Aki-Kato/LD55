using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMissionFullPanel : MonoBehaviour
{
    public TMP_Text LeftText, RightText;
    
    public void SetText(int workLeft, int totalWork, int reward, float TimeLeft){
        LeftText.text = $"Work: {workLeft}/{totalWork}\n${reward}";
        RightText.text = $"Time: {TimeLeft}";
    }
}
