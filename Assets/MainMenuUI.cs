using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        // current scenes only 0 and 1
        // TODO: change implementation when there are more scenes
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void OnQuitButtonCliked()
    {
        Application.Quit();
    }
}
