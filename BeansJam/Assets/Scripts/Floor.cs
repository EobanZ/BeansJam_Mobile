using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Floor : MonoBehaviour {

    public GameObject[] floorTiles;
    public int size = 100;
    bool ready = false;
    MeshCollider collider;


	void Start () {

        //spawn random tiels
        for (int i = 0; i < size; i++)
        {

            for (int j = 0; j < size; j++)
            {
                var go = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector3(i, 0, j), Quaternion.identity, transform);
               

                if (i == size - 1 && j == size - 1)
                {
                    ready = true;
                    
                                 
                }
                    

            }
        }
        //CombineMeshes();
        
       
    


    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CombineMeshes();
        }
	}


    [ContextMenu("Combine Meshes")]
    public void CombineMeshes()
    {
        
        Destroy(gameObject.GetComponent<MeshCollider>());
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            if(i>0)
                meshFilters[i].gameObject.GetComponent<MeshCollider>().enabled = false;
            meshFilters[i].gameObject.active = false;
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.active = true;

        collider = gameObject.AddComponent<MeshCollider>();
        collider.convex = true;



    }
  
}

