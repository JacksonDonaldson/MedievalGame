using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class save : MonoBehaviour
{
    Transform pT;
    public void saveGame()
    {
        //saves all relevant info to PlayerPrefs. this is a bad way to do this, and should probably be improved in the future
        pT = GameObject.Find("Player").GetComponent<Transform>();
        if(pT == null)
        {
            Debug.Log("save failed: player object not found");
        }
        PlayerPrefs.SetFloat("playerX", pT.position.x);
        PlayerPrefs.SetFloat("playerY", pT.position.y);
        PlayerPrefs.SetFloat("playerZ", pT.position.z);

        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        //todo: save health, armor, ect

    }
    public void loadGame()
    {
        //load the position from playerPref, and put player there
        float x = PlayerPrefs.GetFloat("playerX");
        float y = PlayerPrefs.GetFloat("playerY");
        float z = PlayerPrefs.GetFloat("playerZ");
        Movement.startPos = new Vector3(x, y, z);
        SceneManager.LoadScene("Player");
        SceneManager.LoadScene(PlayerPrefs.GetString("Scene"), LoadSceneMode.Additive);
    }
}
