using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused = false;
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
                Time.timeScale = 0;
                paused = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                panel.SetActive(true);
            }
            else
            {
                paused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                panel.SetActive(false);
            }
        }
    }
}
