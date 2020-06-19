using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    //player base speed
    public float speed = 500f;
    //maximum speed, used for air movement only
    public double maxSpeed = 10d;
    //gravity multiplier when falling, also used to prevent sticking to ceilings
    public float gravMult = 4f;
    public float gravity = -9.8f;
    //how many jumps the player currently has
    int jumps = 1;
    //max number of jumps (doesn't work rn, just in case of future implementation
    int jumpMax = 1;
    //divisor for how well the player can be controlled in the air
    public float airControl = 25f;
    public float jumpForce = 9f;
    public float airFriction = 10f;
    //used to slow to a stop (a bit odd, ask Jackson if it doesn't work as intended)
    public float friction = 75f;
    //various other 1 use variables
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 yVelocity = new Vector3(0, 0, 0);
    Vector3 currentVelocity = new Vector3(0, 0, 0);
    public bool isGrounded;
    float lastTime = 0f;
    float smallDistance = 0.5f;
    float smallTime = 0.1f;
    Vector3 shouldBe;
    CharacterController cc;
    Transform pT;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        pT = GetComponent<Transform>();
        shouldBe = pT.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move the player around based on rotation
        //using the players rotation, calculate the porportions of velocity
        velocity = new Vector3(0, 0, 0);
        velocity += (pT.right * Input.GetAxis("Horizontal") * -1f * 999f);
        velocity += pT.forward * Input.GetAxis("Vertical") * -1f * 999f;
        

        //if velocity is not 0 (if a button is being pressed)
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            

            //make it so that velocity represents how much of each direction to go in
            velocity = new Vector3(velocity.x / (Math.Abs(velocity.x) + Math.Abs(velocity.z)), 0, velocity.z / (Math.Abs(velocity.x) + Math.Abs(velocity.z)));
            //multiply by speed
            if (Input.GetButton("Run"))
            {
                velocity = velocity * speed * Time.deltaTime;
            }
            else
            {
                velocity = velocity * speed * Time.deltaTime / 2;
            }
            

            //if we're grounded, just use the velocity
            if (isGrounded)
            {
                currentVelocity = velocity;
            }
            else
            {
                //otherwise, try to influence the previous velocity by the intended amount
                velocity = velocity / airControl;
                if (!Input.GetButton("Run"))
                {
                    velocity = velocity / 2;
                }
                currentVelocity = Vector3.MoveTowards(currentVelocity, new Vector3(0f, 0f, 0f), airFriction * Time.deltaTime);
                //not exceeding maxSpeed
                if (Math.Pow(Math.Pow((currentVelocity + velocity).x, 2) + Math.Pow((currentVelocity + velocity).z, 2), 0.5d) < maxSpeed)
                {
                    currentVelocity += velocity;
                }
            }
            


        }
        else
        {
            //static or turning friction
            if (isGrounded)
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, new Vector3(0f, 0f, 0f), friction * Time.deltaTime);
            }
        
        }

        cc.Move(currentVelocity*Time.deltaTime);


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
                        jumps = jumpMax;
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
            //do that if we're falling, if we should have moved up further than we did(we hit a ceiling)
            if (yVelocity.y <= 0 || shouldBe.y > pT.position.y )
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
