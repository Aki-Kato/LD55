using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Final");
    }
}
