using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float speed = 800f;
    public Rigidbody rb;
    public Transform camT;
    public Transform pT;
    public float camSpeed = 10;
    public double distance = 4d;
    bool mode = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //set camera position based on where the player is
        //todo(2): enable the player to rotate the camera around the player through mouse movement (probably quite difficult)

        //camT.RotateAround(pT.position, Vector3.up, 50 * Time.deltaTime);
        //todo(1): rotate the camera such that it is always looking at the player (done)
        float horizontal = Input.GetAxis("Mouse X") * camSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * camSpeed * Time.deltaTime;
        
        float x = camT.localPosition.x - horizontal;

        if (x > 4)
        {
            x = 4;
        }
        if (x < -4)
        {
            x = -4;
        }
        
        float z = (float)Math.Pow((Math.Pow(distance, 2d) - Math.Pow((double)x, 2)), 0.5d); 
        camT.localPosition = new Vector3(x, 0, z);
        camT.LookAt(pT.position);

        //todo(3): better movement
        //move the player around
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        rb.AddForce(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }
    
}
