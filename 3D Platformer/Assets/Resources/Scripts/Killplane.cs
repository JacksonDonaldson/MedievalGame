using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Killplane : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        print('1');
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
