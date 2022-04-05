using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryUI : MonoBehaviour
{
    protected int currentScore;
    protected int highScore;
    public void OnRetryButtonClicked()
    {
        ReloadScene();
    }

    public void OnQuitButtonClicked() // inspector referenced
    {
        SceneManager.LoadScene(0); // currently main menu
    }

    private void ReloadScene()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
