using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 45;
    public float acceleration = 2;
    public float jumpForce = 10;
    public float boostForce = 35;
    public float jumpDelay = 2;
    public float boostDelay = 5;

    Rigidbody rb;
    bool isOnGround;
    float lastBoosted;

	void Start ()
	{
        rb = GetComponent<Rigidbody>();
        //rb.maxDepenetrationVelocity = maxSpeed;
	}

    void ClampPlayer()
    {
        transform.position = new Vector3( Mathf.Clamp(transform.position.x,.5f,197),transform.position.y,Mathf.Clamp(transform.position.z, .5f, 197));
    }
	
	
	void Update () {



        if (transform.position.y <= -2)
        {
            GameManager.Instance.GameOver = true;
        }

        //if (rb.velocity.magnitude >= maxSpeed)
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        //TODO: Gravity normal wirken lassen
        //rb.AddForce(new Vector3(0, 0, -10));
        isOnGround = Physics.Raycast(transform.position, new Vector3(0, -1, 0), 0.5f);
        if (isOnGround)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            transform.position.Set(transform.position.x, Mathf.Clamp01(transform.position.y), transform.position.z);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if(!isOnGround)
        {
            rb.AddForce(new Vector3(0, -10, 0), ForceMode.Acceleration);
        }

	    Vector3 tilt = Quaternion.Euler(90, 0, 0) * Input.acceleration;
	    rb.AddForce(new Vector3(tilt.x, 0, tilt.z) * acceleration);
	    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

	    Vector3 dir = Vector3.zero;
	    dir.x = -tilt.x;
        dir.z = -tilt.z;
	    if (dir.sqrMagnitude > 1)
	        dir.Normalize();

	    transform.forward = dir;

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
        if(Input.GetKeyDown(KeyCode.LeftShift)&&isOnGround)
        {
            if ((Time.time - lastBoosted) >= boostDelay)
            {
                lastBoosted = Time.time;
                boost();
            }
      
        }
        //rotate orientation with gyroscope

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

    public float GetBoostDelay()
    {
        return boostDelay;
    }

    public float GetJumpDelay()
    {
        return jumpDelay;
    }
}
