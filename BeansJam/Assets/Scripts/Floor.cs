using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Floor : MonoBehaviour {

    public GameObject[] floorTiles;
    public int size;  
    public bool ready = false;
    public bool secondFieldReady = false;
    MeshCollider collider;
    Tile[,] tiles;
    Tile[,] tiles2;
    Transform firstField;

    public Tile[,] Tiles
    {
        get
        {
            return tiles;
        }
        
    }

    private void Awake()
    {
        tiles = new Tile[size, size];
        size = GameManager.Instance.floorSize;

        StartCoroutine(BuildField());
    }

    void Start () {
        firstField = transform.GetChild(0).transform;

        //spawn random tiels
        //for (int i = 0; i < size; i++ )
        //{

        //    for (int j = 0; j < size; j++)
        //    {
        //        var go = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(i*2, 0, j*2), Quaternion.identity, transform);

        //        tiles[i,j] = go.GetComponent<Tile>();
        //        if (i == size - 1 && j == size - 1)
        //        {
        //            ready = true;


        //        }


        //    }
        //}
        //CombineMeshes();

  


    }


    public IEnumerator BuildField()
    {
        ready = false;
        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {

                
                if (tiles[i, j])
                {
                    Destroy(tiles[i, j].gameObject);
                }
                tiles = new Tile[size, size];

                var go = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(i * 2, 0, j * 2), Quaternion.identity, transform);

                tiles[i, j] = go.GetComponent<Tile>();
                if (i == size - 1 && j == size - 1)
                {
                    ready = true;


                }


            }
        }
        yield return null;
    }

	



 
  
}

