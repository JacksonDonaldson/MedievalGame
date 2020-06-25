using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    Transform pT;
    public float sensitivity = 1f;
    public float distance = 5f;
    public Transform camT;
    public float safety = 1f;
    Vector3 ogAngle;
    Vector3 camAngle = new Vector3(0,0.5f,-1);
    Vector3 oldAngle;
    
    void Start()
    {
        pT = GetComponent<Transform>();
        ogAngle = camAngle;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // stores a direction for the camera to go, sends a raycast in that direction, and puts the camera at the closest available spot
        //edit rotation
        if (!pauseMenu.paused)
        {


            oldAngle = camAngle;
            camAngle = Vector3.RotateTowards(camAngle, pT.right, sensitivity * Input.GetAxis("Mouse X"), 0);
            oldAngle = oldAngle - camAngle;
            camAngle = Vector3.RotateTowards(camAngle, pT.up, -sensitivity * Input.GetAxis("Mouse Y"), 0);
            if (camAngle.y > 0.95f)
            {
                camAngle.y = 0.95f;
            }
            //check if we've gone too far(not counting y direction), and if we have rotate the player to match the camera
            //to do: rotate the player more smoothly(only a bit)
            pT.eulerAngles = new Vector3(pT.eulerAngles.x, camT.eulerAngles.y, pT.eulerAngles.z);
            //if (Vector3.Angle(camAngle, new Vector3(ogAngle.x, camAngle.y, ogAngle.z)) > 30)
            //{
            //    pT.eulerAngles = new Vector3(pT.eulerAngles.x, camT.eulerAngles.y, pT.eulerAngles.z);
            //     
            //
            //}
            //if(Vector3.Angle(camAngle, pT.forward * -1) < 10)
            //{
            //    ogAngle = camAngle;
            //}

            //send a raycast the right way, and put the camera just before where it hits, so we don't glitch through walls
            RaycastHit hit;
            if (Physics.Raycast(pT.position, camAngle, out hit, distance))
            {
                camT.position = pT.position + camAngle * (hit.distance - safety);

            }
            else
            {
                camT.position = pT.position + camAngle * (distance - safety);
            }
            //rotate so that we're looking at the player
            camT.LookAt(pT.position);
        }
    }
}
