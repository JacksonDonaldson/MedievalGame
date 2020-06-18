using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float speed = 800f;
    public float offsetY = 2f;
    public float offsetX = 0f;
    public float offsetZ = -3.5f;
    public Rigidbody rb;
    public Transform camT;
    public Transform pT;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set camera position based on where the player is
        //todo(2): enable the player to rotate the camera around the player through mouse movement (probably quite difficult)
        //camT.position = new Vector3(pT.position.x + offsetX, pT.position.y + offsetY, pT.position.z + offsetZ);

        //todo(1): rotate the camera such that it is always looking at the player (not too hard)
        float x = camT.position.x - pT.position.x;
        float y = camT.position.y - pT.position.y;
        float z = camT.position.z - pT.position.z;
        // do some trig to figure out what angle to look at
        pointTo(x, y, z);
        //todo(3): better movement
        //move the player around
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        rb.AddForce(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }
    void pointTo(float x, float y, float z)
    {
        double rotY;
        double rotX;
        float horizontalDistance = (float)Math.Pow((Math.Pow(x,2) + Math.Pow(z,2)),0.5f);
        rotX = Math.Atan(y / horizontalDistance);
        rotX = rotX * 180 / Math.PI;
        rotY = Math.Atan(x/z);
        rotY = rotY * 180 / Math.PI;
        print(rotX);
        if (z <= 0)
        {
            camT.eulerAngles = new Vector3((float)rotX, (float)rotY, camT.eulerAngles.z);
        }
        else
        {
            camT.eulerAngles = new Vector3((float)rotX, (float)rotY + 180, camT.eulerAngles.z);

        }
        


    }
}
