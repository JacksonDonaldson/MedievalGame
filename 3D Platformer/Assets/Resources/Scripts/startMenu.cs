using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    public void playButton()
    {
        SceneManager.LoadScene("Player");
        SceneManager.LoadScene("Room1", LoadSceneMode.Additive);
    }

    public void quitButton()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
