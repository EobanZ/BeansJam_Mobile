using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float maxSpeed = 10;
    public float acceleration = 1000;
    public float jumpForce = 10;
    public float boostForce = 35;
    public float jumpDelay = 2;
    public float boostDelay = 5;

    Rigidbody rb;
    bool isOnGround;
    float lastJumped;
    float lastBoosted;

	void Start () {
        rb = GetComponent<Rigidbody>();
        //rb.maxDepenetrationVelocity = maxSpeed;
	}
	
	
	void Update () {

        //if (rb.velocity.magnitude >= maxSpeed)
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        //TODO: Gravity normal wirken lassen
        //rb.AddForce(new Vector3(0, 0, -10));
        isOnGround = Physics.Raycast(transform.position, new Vector3(0, -1, 0), 0.5f);

        if(!isOnGround)
        {
            rb.AddForce(new Vector3(0, -10, 0), ForceMode.Acceleration);
        }


        if (Input.GetKey(KeyCode.W))
        {
       
            rb.AddForce(new Vector3(0, 0, 1)* acceleration * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
               
        }
        if (Input.GetKey(KeyCode.D))
        {

            rb.AddForce(new Vector3(1, 0, 0) * acceleration * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {

            rb.AddForce(new Vector3(-1, 0, 0) * acceleration * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {

            rb.AddForce(new Vector3(0, 0, -1) * acceleration * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&isOnGround)
        {
            if((Time.time - lastJumped) >= jumpDelay)
            {
                lastJumped = Time.time;
                jump();
            }
            
            
            
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)&&isOnGround)
        {
            if ((Time.time - lastBoosted) >= boostDelay)
            {
                lastJumped = Time.time;
                boost();
            }
      
        }
        //rotate orientation with gyroscope

    }

    void jump()
    {
        rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Acceleration);
    }

    void boost()
    {
        var direction = transform.rotation;
        Vector3 dir = direction * -Vector3.forward;
        rb.isKinematic = true;
        rb.velocity.Set(0, 0, 0);
        rb.isKinematic = false;
        rb.AddForce(dir * boostForce, ForceMode.Impulse);
    }
}
