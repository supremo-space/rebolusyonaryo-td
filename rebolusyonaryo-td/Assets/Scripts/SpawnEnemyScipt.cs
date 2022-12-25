using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScipt : MonoBehaviour
{
    public GameObject enemySoldier;
    public GameObject spawnLocation;

    void Start() { }

    void Update()
    {
        if (Time.frameCount % 1080 == 0)
        {
            spawnEnemySoldiers();
        }
    }

    //spawing enemy soldiers
    void spawnEnemySoldiers()
    {
        Instantiate(
            enemySoldier,
            spawnLocation.transform.position,
            spawnLocation.transform.rotation
        );
    }
}
