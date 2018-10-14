using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "scooter")
        {
            GameManager.Instance.AddRepairItem();
            //Particle Effect
            Destroy(gameObject);
        }
    }
}
