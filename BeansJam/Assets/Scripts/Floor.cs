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
    Transform secondField;
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
        secondField = transform.GetChild(1).transform;

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

        StartCoroutine(BuildSecondField());


    }

    public IEnumerator BuildSecondField()
    {
        secondFieldReady = false;
        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {


                tiles2 = new Tile[size, size];

                var go = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(i * 2 + secondField.position.x, 0, j * 2 + secondField.position.z), Quaternion.identity, secondField);
                go.GetComponent<BoxCollider>().enabled = false;

                tiles2[i, j] = go.GetComponent<Tile>();
                if (i == size - 1 && j == size - 1)
                {
                    secondFieldReady = true;


                }


            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void ReplaceField()
    {
       

    

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

                var go = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(i * 2, 0, j * 2), Quaternion.identity, firstField);

                tiles[i, j] = go.GetComponent<Tile>();
                if (i == size - 1 && j == size - 1)
                {
                    ready = true;


                }


            }
        }
        yield return null;
    }

	
	void Update () {
       
	}


    [ContextMenu("Combine Meshes")]
    IEnumerator CombineMeshes()
    {
        Destroy(gameObject.GetComponent<MeshCollider>());
        
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        Debug.Log(meshFilters.Length);
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            if (i > 0)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

                meshFilters[i].gameObject.GetComponent<BoxCollider>().enabled = false;
                meshFilters[i].gameObject.active = false;
            }
                
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.active = true;

        collider = gameObject.AddComponent<MeshCollider>();
        //collider.convex = true;
        yield return null;


    }

 
  
}

