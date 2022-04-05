using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public void OnResumeButtonClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnQuitButtonClicked() // inspector referenced
    {
        SceneManager.LoadScene(0); // currently main menu
    }
}
