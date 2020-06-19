using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float speed = 800f;
    CharacterController cc;
    public Transform pT;
    public float jumpForce = 250f;
    public float friction;
    public float gravity = -9.8f;
    public double maxSpeed = 10d;
    public float gravMult = 4f;
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 yVelocity = new Vector3(0, 0, 0);
    Vector3 currentVelocity = new Vector3(0, 0, 0);
    double lastVel;
    public bool isGrounded;
    float lastTime = 0f;
    float smallDistance = 0.5f;
    float smallTime = 0.1f;
    Vector3 shouldBe;
    int jumps = 1;
    int jumpCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        shouldBe = pT.position;
    }

    // Update is called once per frame
    void Update()
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
        //move the appropriate amount
        cc.Move(currentVelocity *Time.deltaTime);


        //jump/gravity controls
        //mostly done - known bug: movement down slopes is jittery. being caused by teleporting into the ground I think
        
        //raycast to find the ground from where the player is
        RaycastHit hit;
        if (Physics.CapsuleCast(pT.position, pT.position, cc.radius, Vector3.down, out hit, smallDistance))
        {
            
            //check if it's been a little while since we snapped to the ground (prevent it happening right after jumping)
            if (Time.time - lastTime > smallTime)
            {
                
                //check to make sure its a surface we can walk on (needs work) 
                if (hit.normal.y > 0f)
                {
                    
                    //if we weren't grounded before, snap to the ground
                    if (!isGrounded)
                    {
                        cc.Move(Vector3.down * hit.distance + Vector3.down * cc.height * .5f);
                        jumps = jumpCount;
                        yVelocity.y = 0f;
                        lastTime = Time.time;
                    }
                    
                }


            }
            //set grounded to true if it hits at all
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        // if we want to jump, and can, jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            if (jumps > 0)
            {
                yVelocity.y += jumpForce;
                isGrounded = false;
                jumps -= 1;
            }
        }
        // if we aren't grounded, check to see if we should increase gravity
        if (!isGrounded)
        {
            //do that if we're falling, if we should have moved up further than we did(we hit a ceiling), or the player let go of jump
            if (yVelocity.y <= 0 || shouldBe.y > pT.position.y || !Input.GetButton("Jump") )
            {
                yVelocity.y += gravity * Time.deltaTime * gravMult;
            }
            else
            {
                yVelocity.y += gravity * Time.deltaTime;
            }
            
        }
        //reset where we should be, and add the amount we should move to the player
        shouldBe = pT.position + yVelocity * Time.deltaTime;
        cc.Move(yVelocity * Time.deltaTime);
    }
    
}
