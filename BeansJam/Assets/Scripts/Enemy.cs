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
    [HideInInspector] public Transform SpawningPosition;
    public float RotateSpeed = 20f;
    public float Shootintervall = 3;
    public float MinIntervall = 0.5f;
    public float rangeAroundIntervall = 2;
    float randomShootinterval;

     public EnemyType type;
	// Use this for initialization
	void Start () {
        SpawningPosition = transform.Find("SpawnPosition").transform;


        //SpawnProjectile();
        NextRandomIntervall();
        randomShootinterval /= Random.Range(1,3);
    }
    
    void NextRandomIntervall()
    {
        randomShootinterval = Random.Range(Shootintervall - rangeAroundIntervall, Shootintervall + rangeAroundIntervall);
        randomShootinterval = randomShootinterval * (1/GameManager.Instance.Multiplier);
        if (randomShootinterval < MinIntervall)
            randomShootinterval = MinIntervall;

    }
	
	// Update is called once per frame
	void Update () {
        randomShootinterval = randomShootinterval - Time.deltaTime;
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
        if (!(randomShootinterval <= 0))
            return;

        NextRandomIntervall();
        SpawnProjectile();

    }

    void UpdateBig()
    {
        if (!(randomShootinterval <= 0))
            return;

        NextRandomIntervall();
        SpawnProjectile();
    }

    void UpdateRotator()
    {
        transform.RotateAround(GameManager.Instance.Center.position, Vector3.up, RotateSpeed * Time.deltaTime);
        transform.LookAt(GameManager.Instance.Player.transform.position);

        if (!(randomShootinterval <= 0))
            return;

        NextRandomIntervall();
        SpawnProjectile();
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
                Instantiate(RotatorPrefab, SpawningPosition.position, Quaternion.identity);
                break;
            default:
                break;
        }
    }
}
