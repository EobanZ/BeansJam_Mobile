using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager> {

    public int floorSize;
    public GameObject PlayerPrefab;
    public Tile[,] tiles;

	void Start () {
        Instantiate(PlayerPrefab, new Vector3(floorSize / 2, 1 , floorSize / 2), Quaternion.identity);
	}
	
	
	void Update () {
		
	}

    void positionCamera()
    {
        
    }
}
