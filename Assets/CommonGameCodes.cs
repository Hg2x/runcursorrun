using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonGameCodes : MonoBehaviour
{
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
