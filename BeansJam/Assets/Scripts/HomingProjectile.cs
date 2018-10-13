using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile {
    public Transform target;

    public float damping = 1.0f;
    public float flySpeed = 15;
    public float lifeTime = 6f;
    public int damage = 3;

    protected override void OnPlayerCollision(Collider other)
    {
        GameManager.Instance.ApplyDamage(damage);
        Explode();
    }

    protected override void OnTileCollission(Collider other)
    {
        Explode();
    }

    protected override void Shooting()
    {
       
    }

    // Use this for initialization
    new void Start () {
        base.Start();

        target = GameManager.Instance.Player.transform;
        
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();

        

        transform.Translate(Vector3.forward * Time.deltaTime * flySpeed);
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Explode();
        }
	}

    void Explode()
    {
        Destroy(gameObject);
        //TODO play particle Effect

        float radius = Random.Range(minImpactRadius, maxImpactRadius);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius );
        int i = 0;
        while (i < hitColliders.Length)
        {
            var tile = hitColliders[i].gameObject.GetComponent<Tile>();
            var player = hitColliders[i].gameObject.GetComponent<Rigidbody>();
            if (tile)
                tile.Remove();
            if (player)
                player.AddExplosionForce(explosionForce, transform.position, radius);
            
            i++;
        }
    }
}
