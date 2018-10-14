using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItem : MonoBehaviour {

    public int Points = 100;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "scooter")
        {
            GameManager.Instance.AddToScore(Points);
        }
    }
}
