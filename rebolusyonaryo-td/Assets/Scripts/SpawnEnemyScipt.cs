using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScipt : MonoBehaviour
{
    public GameObject[] enemySoldiers;
    public GameObject[] instantiatedEnemySoldiers;
    public GameObject spawnLocation;

    void Start() { }

    void Update()
    {
        delaySpawn();
        storeEnemySoldier();
    }

    //spawing enemy soldiers
    void spawnEnemySoldiers()
    {
        var randomNum = Random.Range(0, 6);
        Instantiate(
            enemySoldiers[randomNum],
            spawnLocation.transform.position,
            spawnLocation.transform.rotation
        );
    }

    //delay spawn

    void delaySpawn()
    {
        if (Time.frameCount % 1080 == 0)
        {
            spawnEnemySoldiers();
        }
    }

    //store enemy on array
    void storeEnemySoldier()
    {
        instantiatedEnemySoldiers = GameObject.FindGameObjectsWithTag("EnemySoldiers");
    }
}
