using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public abstract class Projectile : MonoBehaviour {
    public float minImpactRadius = 2f;
    public float maxImpactRadius = 5f;
    public float explosionForce = 0;
    public GameObject ExplosionPrefab;
    protected Rigidbody rb;

	// Use this for initialization
	protected virtual void Start () {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(GameManager.Instance.Player.transform);
	}
  
	
	// Update is called once per frame
	protected virtual void Update () {
        Shooting();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tile")
        {
            
            OnTileCollission(other);
        }
        if(other.tag == "scooter")
        {
            OnPlayerCollision(other);
        }
       
    }

 

    protected abstract void OnPlayerCollision(Collider other);


    protected abstract void OnTileCollission(Collider other);

    protected abstract void Shooting();
    
}
