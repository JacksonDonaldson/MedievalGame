using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSun : MonoBehaviour
{
    public Transform sunT;
    public float sunSpeed;
    float rotation;
    // Update is called once per frame
    void Update()
    {
        sunT.Rotate(new Vector3(sunSpeed * Time.deltaTime, 0, 0));
    }
}
