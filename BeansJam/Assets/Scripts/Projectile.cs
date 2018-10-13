using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour {
    public float minImpactRadius = 2f;
    public float maxImpactRadius = 5f;
    protected Rigidbody rb;

	// Use this for initialization
	protected virtual void Start () {
        rb = GetComponent<Rigidbody>();
	}
  
	
	// Update is called once per frame
	protected virtual void Update () {
        Shooting();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tile")
        {
            
            OnTileCollission();
        }
        if(other.tag == "scooter")
        {
            OnPlayerCollision(other);
        }
       
    }

 

    protected abstract void OnPlayerCollision(Collider other);


    protected abstract void OnTileCollission();

    protected abstract void Shooting();
    
}
