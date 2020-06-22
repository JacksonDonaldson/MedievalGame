using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform camT;
    public Transform pT;
    public CharacterController camCC;
    public float camSpeed = 10;
    public double distance = 5d;
    float x;
    float y;
    float z;
    Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        y = camT.localPosition.y;
        x = camT.localPosition.x;
        z = camT.localPosition.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        //enable the player to rotate the camera around the player through mouse movement
        //moves the camera around the player at a set distance
        //gets how far to move from mouse movements
        float horizontal = Input.GetAxis("Mouse X") * camSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * camSpeed * Time.deltaTime;
        lastPos = new Vector3(x, y, z);
        //first, calculates how far to move vertically, and then figures out how far the camera should be from the player at that height(that's horizontalDistance)
        y = camT.localPosition.y - vertical;
        if (y > distance - 0.5d)
        {
            y = (float)distance - 0.5f;
        }
        if (y < 0)
        {
            y = 0f;
        }
        float horizontalDistance = (float)Math.Pow((Math.Pow(distance, 2d) - Math.Pow((double)y, 2)), 0.5d);

        //find out how far in the x direction to move
        x = camT.localPosition.x - horizontal;

        if (x > horizontalDistance)
        {
            x = horizontalDistance;
        }
        if (x < -horizontalDistance)
        {
            x = -horizontalDistance;
        }
        //set z acording to how far x moved, and how far horizontalDistance is
        z = (float)Math.Pow((Math.Pow(horizontalDistance, 2d) - Math.Pow((double)x, 2)), 0.5d);
        //set new position, but only if there isn't an object in the way
        if (!Physics.Raycast(camT.position, new Vector3(x, y, z) - lastPos, 0.5f))
        {
            camT.localPosition = new Vector3(x, y, z);
        }
        //rotate the camera such that it is always looking at the player (done)
        
        camT.LookAt(pT.position);

        //rotate the player with the camera. The camera is kept constant
        Vector3 camPos = camT.localPosition;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) || Math.Abs(pT.eulerAngles.y - camT.eulerAngles.y)>30)
        {
            pT.eulerAngles = new Vector3(pT.eulerAngles.x, camT.eulerAngles.y + 180, pT.eulerAngles.z);
            camT.localPosition = new Vector3(0, camT.localPosition.y, horizontalDistance);
            camT.LookAt(pT.position);
        }

    }
}
