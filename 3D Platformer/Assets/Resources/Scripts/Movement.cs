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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        
        //enable the player to rotate the camera around the player through mouse movement
        //moves the camera around the player at a set distance
        //gets how far to move from mouse movements
        float horizontal = Input.GetAxis("Mouse X") * camSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * camSpeed * Time.deltaTime;

        //first, calculates how far to move vertically, and then figures out how far the camera should be from the player at that height(that's horizontalDistance)
        float y = camT.localPosition.y - vertical;
        if (y > distance-0.01d)
        {
            y = (float)distance-0.01f;
        }
        if (y < 0)
        {
            y = 0f;
        }
        float horizontalDistance = (float)Math.Pow((Math.Pow(distance, 2d) - Math.Pow((double)y, 2)), 0.5d);
        
        //find out how far in the x direction to move
        float x = camT.localPosition.x - horizontal;

        if (x > horizontalDistance)
        {
            x = horizontalDistance;
        }
        if (x < -horizontalDistance)
        {
            x = -horizontalDistance;
        }
        //set z acording to how far x moved, and how far horizontalDistance is
        float z = (float)Math.Pow((Math.Pow(horizontalDistance, 2d) - Math.Pow((double)x, 2)), 0.5d); 
        //set new position
        camT.localPosition = new Vector3(x, y, z);

        //rotate the camera such that it is always looking at the player (done)
        camT.LookAt(pT.position);

        //rotate the player so that when a direction is pressed, it looks in that direction and the camera is kept constant
        Vector3 camPos = camT.localPosition;
        if(Input.GetAxis("Horizontal")!=0f || Input.GetAxis("Vertical")!=0f)
        {
            pT.eulerAngles = new Vector3(pT.eulerAngles.x, camT.eulerAngles.y+180, pT.eulerAngles.z);
            camT.localPosition = new Vector3(0, camT.localPosition.y, horizontalDistance);
            camT.LookAt(pT.position);
        }

        //move the player around based on rotation
        //using the players rotation, calculate the porportions of velocity
        Vector3 velocity = (pT.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed * -1f);
        velocity += pT.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed * -1f;
        velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        rb.velocity = velocity;
        //rb.AddForce(pT.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed * -1f);
        //rb.AddForce(pT.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed * -1f);
    }
    
}
