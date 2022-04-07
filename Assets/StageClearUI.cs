using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    public void OnQuitButtonClicked() // inspector referenced
    {
        SceneManager.LoadScene(0); // currently main menu
    }
}
