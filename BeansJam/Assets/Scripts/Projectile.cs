using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float maxRadius = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Random.Range(2,maxRadius));
        int i = 0;
        Debug.Log(hitColliders.Length);
        while (i < hitColliders.Length)
        {
            var tile = hitColliders[i].gameObject.GetComponent<Tile>();
            if (tile) 
            tile.Remove();
            i++;
        }
    }
}
