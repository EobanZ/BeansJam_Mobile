using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : Projectile {
    public int damage = 1;

    protected override void OnPlayerCollision(Collider other)
    {
        GameManager.Instance.ApplyDamage(damage);

        Explode();
        //float radius = Random.Range(minImpactRadius, maxImpactRadius);
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        //int i = 0;
        //while (i < hitColliders.Length)
        //{
        //    var player = hitColliders[i].gameObject.GetComponent<Rigidbody>();
        //    if (player)
        //        player.AddExplosionForce(explosionForce, transform.position, radius);

        //    i++;

        //}
    }

    protected override void OnTileCollission(Collider other)
    {
        Explode();

        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, Random.Range(minImpactRadius, maxImpactRadius));
        //int i = 0;

        //while (i < hitColliders.Length)
        //{
        //    var tile = hitColliders[i].gameObject.GetComponent<Tile>();
        //    if (tile)
        //        tile.Remove();
        //    i++;
        //}
        removeProjectile();
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

    void Explode()
    {
        removeProjectile();

        float radius = Random.Range(minImpactRadius, maxImpactRadius);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            var tile = hitColliders[i].gameObject.GetComponent<Tile>();
            var player = hitColliders[i].gameObject.GetComponent<PlayerMovement>();
            if (tile)
                tile.Remove();
            if (player)
            {
                player.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius);
                GameManager.Instance.ApplyDamage(damage);
            }               
            i++;
        }
    }
}
