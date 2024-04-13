using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDisableOnStart : MonoBehaviour
{
    public void Awake(){
        gameObject.SetActive(false);
    }
}
