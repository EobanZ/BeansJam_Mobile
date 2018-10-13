using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : Projectile {

    protected override void OnPlayerCollision(Collider other)
    {
        removeProjectile();
        other.GetComponent<Rigidbody>().AddForce(rb.velocity * 100, ForceMode.Impulse);
    }

    protected override void OnTileCollission(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Random.Range(minImpactRadius, maxImpactRadius));
        int i = 0;
        Debug.Log(hitColliders.Length);
        while (i < hitColliders.Length)
        {
            var tile = hitColliders[i].gameObject.GetComponent<Tile>();
            if (tile)
                tile.Remove();
            i++;
        }
        removeProjectile();
    }

    void shoot()
    {
        
    }

    protected override void Shooting()
    {
        
    }

    // Use this for initialization
    new void Start()
    {
        base.Start();
        rb.isKinematic = true;
        rb.velocity.Set(0, 0, 0);
        rb.isKinematic = false;
        Vector3 v = calcBallisticVelocityVector(transform.position, new Vector3(Random.Range(0, GameManager.Instance.floorSize*2), 0, Random.Range(0, GameManager.Instance.floorSize*2)), 45);
        Debug.Log(v);
        rb.AddForce(v.x,v.y,v.z,ForceMode.VelocityChange);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
    }

    void removeProjectile()
    {
        Destroy(gameObject);
    }

    Vector3 calcBallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }
}
