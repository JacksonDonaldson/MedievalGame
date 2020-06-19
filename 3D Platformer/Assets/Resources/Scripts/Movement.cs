using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float speed = 800f;
    CharacterController cc;
    public Transform pT;
    public int jumpCount =1;
    public float jumpForce = 250f;
    public int jumps = 1;
    public float friction;
    public float gravity = -9.8f;
    public double maxSpeed = 10d;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 yVelocity = new Vector3(0, 0, 0);
    Vector3 currentVelocity = new Vector3(0, 0, 0);
    double lastVel;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the player around based on rotation
        //using the players rotation, calculate the porportions of velocity
        velocity = new Vector3(0, 0, 0);
        velocity += (pT.right * Input.GetAxis("Horizontal") * -1f);
        velocity += pT.forward * Input.GetAxis("Vertical") * -1f;
        
        //if velocity is not 0 (if a button is being pressed)
        if (velocity.x != 0 && velocity.z != 0)
        {
            //make it so that velocity represents how much of each direction to go in
            velocity = new Vector3(velocity.x / (Math.Abs(velocity.x) + Math.Abs(velocity.z)), 0, velocity.z / (Math.Abs(velocity.x) + Math.Abs(velocity.z)));
            //multiply by speed
            velocity = velocity * speed * Time.deltaTime;
            //if we aren't already at max speed, add the velocity to the current velocity
            if(Math.Pow(Math.Pow(currentVelocity.x,2)+Math.Pow(currentVelocity.z, 2),0.5d) < maxSpeed){
                currentVelocity += velocity;
            }
            
            
        }
        //if we're slowing down (or not speeding up), use friction
        if (lastVel>= Math.Pow(Math.Pow(currentVelocity.x, 2) + Math.Pow(currentVelocity.z, 2), 0.5d))
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, new Vector3(0f, 0f, 0f), friction * Time.deltaTime);
        }
        lastVel = Math.Pow(Math.Pow(currentVelocity.x, 2) + Math.Pow(currentVelocity.z, 2), 0.5d);
        cc.Move(currentVelocity *Time.deltaTime);


        //jump/gravity controls
        //in progress
        if (cc.isGrounded)
        {
            jumps = jumpCount;
        }
        if (cc.isGrounded && yVelocity.y < 0)
        {
            yVelocity.y = 0f;
        }
        if (Input.GetButton("Jump"))
        {
            if (jumps > 0)
            {
                yVelocity.y += jumpForce;
                jumps -= 1;
            }
        }
        
        yVelocity.y += gravity * Time.deltaTime;
        cc.Move(yVelocity * Time.deltaTime);
    }
    
}
