using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNoteProjectile : Projectile {
    Vector3 destinationPoint;
    public float speed = 0.125f;
    public float dropAfterSeconds = 2;
    float timer;
  
    protected override void OnPlayerCollision(Collider other)
    {
        removeProjectile();
        other.GetComponent<Rigidbody>().AddForce(rb.velocity * 100, ForceMode.Impulse);
    }

    protected override void OnTileCollission(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Random.Range(minImpactRadius, maxImpactRadius));
        int i = 0;
        while (i < hitColliders.Length)
        {
            var tile = hitColliders[i].gameObject.GetComponent<Tile>();
            if (tile)
                tile.Remove();
            i++;
        }
        removeProjectile();
    }

    protected override void Shooting()
    {

        transform.position = Vector3.Lerp(transform.position, destinationPoint, Time.deltaTime * speed);
        if(Time.time - timer >= dropAfterSeconds)
        {
            rb.velocity.Set(0, 0, 0);
            rb.isKinematic = false;
            
        }
    }

    // Use this for initialization
    new void Start () {
        base.Start();
        destinationPoint = new Vector3(Random.Range(0, GameManager.Instance.floorSize * 2), 20, Random.Range(0, GameManager.Instance.floorSize * 2));
        rb.isKinematic = true;
        timer = Time.time;

	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    void removeProjectile()
    {
        Destroy(gameObject);
    }
}
