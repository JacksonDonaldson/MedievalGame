using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 loadPosition;
    public static Vector3 loadPos;
    public string sceneToLoad;
    void OnTriggerEnter(Collider col)
    {
        if(col.name == "Player")
        {
            loadPos = loadPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
