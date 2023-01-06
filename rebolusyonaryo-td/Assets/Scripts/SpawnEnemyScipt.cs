using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnEnemyScipt : MonoBehaviour
{
    public GameObject[] enemySoldiers;
    public GameObject[] instantiatedEnemySoldiers;
    public GameObject spawnLocation;
    public Button[] defenderButtons;
    public static bool isReadyToPlay = false;
    private bool isSpawnAvailable = true;
    private int roundNum = 0;
    private int instantiatedEnemySoldiersCount = 0;
    public GameObject roundText;
    private Scene scene;
    private float time;
    private float timeDelay = 2f;
    private GameObject[] instantiatedPinoy;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        changeRoundText();
        readyToSpawn();
        storeEnemySoldier();
        storePinoySoldier();
        ifEnemyZero();
        checkVictory();
    }

    public void playButton()
    {
        time = 0f;
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
            disableCircleCollider(true);
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
        if (scene.name == "AmericanWarScene")
        {
            ifAmericanScene();
        }
    }

    //delay spawn

    void delaySpawn()
    {
        time = time + 1f * Time.deltaTime;
        if (time >= timeDelay)
        {
            spawnEnemySoldiers();
            time = 0f;
        }
    }

    //store enemy on array
    void storeEnemySoldier()
    {
        instantiatedEnemySoldiers = GameObject.FindGameObjectsWithTag("EnemySoldiers");
    }

    void storePinoySoldier()
    {
        instantiatedPinoy = GameObject.FindGameObjectsWithTag("PinoySoldiers");
    }

    void disableCircleCollider(bool boolean)
    {
        foreach (var defender in instantiatedPinoy)
        {
            defender.gameObject.GetComponent<CircleCollider2D>().enabled = boolean;
        }
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
                    disableCircleCollider(false);
                }
            }
        }
    }

    void changeRoundText()
    {
        if (!isReadyToPlay)
        {
            roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
                "Next round: " + (roundNum + 1).ToString();
        }
        else
        {
            roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
                "Round " + roundNum.ToString();
        }
    }

    void checkVictory()
    {
        if (scene.name == "AmericanWarScene")
        {
            if (roundNum == 10)
            {
                if (instantiatedEnemySoldiers.Length == 0)
                {
                    if (!isReadyToPlay)
                    {
                        foreach (var btn in defenderButtons)
                        {
                            btn.interactable = false;
                        }
                        roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =
                            "victory!!!!!!";
                    }
                }
            }
        }
    }

    void ifAmericanScene()
    {
        if (roundNum == 1)
        {
            spawningCount(0, 2, 10);
        }
        else if (roundNum == 2)
        {
            spawningCount(0, 3, 25);
        }
        else if (roundNum == 3)
        {
            spawningCount(1, 3, 35);
        }
        else if (roundNum == 4)
        {
            spawningCount(1, 3, 50);
            SoldierScript.speed = 80f;
        }
        else if (roundNum == 5)
        {
            spawningCount(2, 4, 55);
        }
        else if (roundNum == 6)
        {
            spawningCount(2, 5, 70);
        }
        else if (roundNum == 7)
        {
            spawningCount(3, 5, 80);
        }
        else if (roundNum == 8)
        {
            SoldierScript.speed = 100f;
            spawningCount(4, 6, 85);
        }
        else if (roundNum == 9)
        {
            spawningCount(4, 6, 95);
        }
        else if (roundNum == 10)
        {
            spawningCount(5, 7, 110);
        }
    }

    void spawningCount(int min, int max, int count)
    {
        var randomNum = Random.Range(min, max);
        Instantiate(
            enemySoldiers[randomNum],
            spawnLocation.transform.position,
            spawnLocation.transform.rotation
        );

        instantiatedEnemySoldiersCount++;

        if (instantiatedEnemySoldiersCount == count)
        {
            isSpawnAvailable = !isSpawnAvailable;
        }
    }
}
