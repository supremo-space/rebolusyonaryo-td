using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemyScipt : MonoBehaviour
{
    public GameObject[] enemySoldiers;
    public GameObject[] instantiatedEnemySoldiers;
    public GameObject spawnLocation;
    public Button[] defenderButtons;
    private bool isReadyToPlay = false;
    private bool isSpawnAvailable = true;
    private int roundNum = 0;

    private int instantiatedEnemySoldiersCount = 0;

    public GameObject roundText;

    void Start() { }

    void Update()
    {
        changeRoundText();
        readyToSpawn();
        storeEnemySoldier();
        ifEnemyZero();
    }

    public void playButton()
    {
        if (!isReadyToPlay)
        {
            roundNum++;
            isReadyToPlay = !isReadyToPlay;
            foreach (var btn in defenderButtons)
            {
                btn.interactable = false;
            }
        }
    }

    void readyToSpawn()
    {
        if (isReadyToPlay)
        {
            if (isSpawnAvailable)
            {
                delaySpawn();
                storeEnemySoldier();
            }
        }
    }

    //spawing enemy soldiers
    void spawnEnemySoldiers()
    {
        if (roundNum == 1)
        {
            var randomNum = Random.Range(0, 2);
            Instantiate(
                enemySoldiers[randomNum],
                spawnLocation.transform.position,
                spawnLocation.transform.rotation
            );

            instantiatedEnemySoldiersCount++;

            if (instantiatedEnemySoldiersCount == 10)
            {
                isSpawnAvailable = !isSpawnAvailable;
            }
        }
        else if (roundNum == 2)
        {
            var randomNum = Random.Range(0, 3);
            Instantiate(
                enemySoldiers[randomNum],
                spawnLocation.transform.position,
                spawnLocation.transform.rotation
            );

            instantiatedEnemySoldiersCount++;

            if (instantiatedEnemySoldiersCount == 20)
            {
                isSpawnAvailable = !isSpawnAvailable;
            }
        }
    }

    //delay spawn

    void delaySpawn()
    {
        if (Time.frameCount % 60 == 0)
        {
            spawnEnemySoldiers();
        }
    }

    //store enemy on array
    void storeEnemySoldier()
    {
        instantiatedEnemySoldiers = GameObject.FindGameObjectsWithTag("EnemySoldiers");
    }

    void ifEnemyZero()
    {
        if (!isSpawnAvailable)
        {
            if (isReadyToPlay)
            {
                if (instantiatedEnemySoldiers.Length == 0)
                {
                    isReadyToPlay = !isReadyToPlay;
                    foreach (var btn in defenderButtons)
                    {
                        btn.interactable = true;
                    }
                    isSpawnAvailable = !isSpawnAvailable;
                    instantiatedEnemySoldiersCount = 0;
                }
            }
        }
    }

    void changeRoundText()
    {
        roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = roundNum.ToString();
    }
}
