using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed);
        rb.AddForce(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }
}
