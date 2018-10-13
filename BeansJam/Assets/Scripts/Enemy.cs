using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Standard,
    Big,
    Rotator
}

public class Enemy : MonoBehaviour {

    public GameObject StandardPrefab;
    public GameObject BigPrefab;
    public GameObject RotatorPrefab;
    public Transform SpawningPosition;
    public float RotateSpeed = 20f;

     public EnemyType type;
	// Use this for initialization
	void Start () {
        SpawningPosition = transform.Find("SpawnPosition").transform;
        SpawnProjectile();
	}
	
	// Update is called once per frame
	void Update () {
        switch (type)
        {
            case EnemyType.Standard:
                UpdateStandard();
                break;
            case EnemyType.Big:
                UpdateBig();
                break;
            case EnemyType.Rotator:
                UpdateRotator();
                break;
            default:
                break;
        }
    }

    void UpdateStandard()
    {
        
    }

    void UpdateBig()
    {

    }

    void UpdateRotator()
    {
        transform.RotateAround(GameManager.Instance.Center.position, Vector3.up, RotateSpeed * Time.deltaTime);
        transform.LookAt(GameManager.Instance.Player.transform.position);
    }

    [ContextMenu("Spawn Projectiles")]
    public void SpawnProjectile()
    {
        switch (type)
        {
            case EnemyType.Standard:
                Instantiate(StandardPrefab, SpawningPosition.position, Quaternion.identity);
                break;
            case EnemyType.Big:
                Instantiate(BigPrefab, SpawningPosition.position, Quaternion.identity);
                break;
            case EnemyType.Rotator:
                
                break;
            default:
                break;
        }
    }
}
