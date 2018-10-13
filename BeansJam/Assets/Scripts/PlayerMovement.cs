using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float maxSpeed = 10;
    public float acceleration = 1000;

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.maxDepenetrationVelocity = maxSpeed;
	}
	
	
	void Update () {

        if (rb.velocity.magnitude >= maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        Debug.Log(rb.velocity);
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
        //rotate orientation with gyroscope

    }
}
