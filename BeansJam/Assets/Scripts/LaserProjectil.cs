
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : Projectile
{

    public int LaserDamage = 2;
    protected override void OnPlayerCollision(Collider other)
    {
        GameManager.Instance.ApplyDamage(LaserDamage);
    }

    protected override void OnTileCollission(Collider other)
    {
        other.GetComponent<Tile>().Remove();
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
}
