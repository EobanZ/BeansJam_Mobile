using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "scooter")
        {
            GameManager.Instance.AddHealth(1);
            //TODO Particle Effects
            Destroy(gameObject);
        }
    }
}
