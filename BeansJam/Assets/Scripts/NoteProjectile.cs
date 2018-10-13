using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteProjectile : Projectile {

    protected override void OnPlayerCollision(Collider other)
    {
        removeProjectile();
        other.GetComponent<Rigidbody>().AddForce(rb.velocity * 100, ForceMode.Impulse);
    }

    protected override void OnTileCollission()
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

    protected override void Shooting()
    {
        
    }

    // Use this for initialization
    new void Start()
    {
        base.Start();
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
}
