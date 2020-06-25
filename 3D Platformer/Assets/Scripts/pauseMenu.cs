using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetButtonDown("Cancel")){
            if(paused == false)
            {
                pause();
            }
            else
            {
                resume();
            }
        }

    }
    public void pause()
    {
        Time.timeScale = 0;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panel.SetActive(true);
    }
    public void resume()
    {
        paused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panel.SetActive(false);
    }
    public void toMenu()
    {
        paused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
