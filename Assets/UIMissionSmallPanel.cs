using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMissionSmallPanel : MonoBehaviour
{
    public TMP_Text TopText, BottomText;
    
    public void SetText(int workLeft, int totalWork, float TimeLeft){
        TopText.text = $"Work: {workLeft}/{totalWork}";
        BottomText.text = $"Time: {TimeLeft}";
    }
}
