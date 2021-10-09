using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    bool menuOn = false;
    public GameObject quitButton;
    public GameObject restartButton;

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu()
    {
        if (menuOn)
        {
            quitButton.SetActive(false);
        }
    }
}
