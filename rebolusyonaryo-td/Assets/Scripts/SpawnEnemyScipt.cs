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
    private bool isSpawnAvailable = true,
        isSettingsOpen = false;
    public GameObject settings;
    private int roundNum = 0;
    private int instantiatedEnemySoldiersCount = 0;
    public GameObject roundText;
    private Scene scene;
    private float time;
    private float timeDelay = 2f;
    private GameObject[] instantiatedPinoy;
    public AudioSource playAS;
    public GameObject clearedText;
    public AudioSource victoryAS;
    public GameObject victory;
    public TextMeshProUGUI victoryText;
    public AudioClip victoryWinAS;
    private float times;

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
        playAS.Play();
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
        if (scene.name == "JapanWarScene")
        {
            ifJapaneseScene();
        }
        else if (scene.name == "AmericanWarScene")
        {
            ifAmericanScene();
        }
        else if (scene.name == "SpanishWarScene")
        {
            ifSpanishScene();
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
                    times = Time.timeScale;
                    Time.timeScale = 1;
                    victoryAS.Play();
                    StartCoroutine(backTimeScale());
                    clearedText.GetComponent<Animator>().SetTrigger("Show");
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

    IEnumerator backTimeScale()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = times;
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
        if (scene.name == "JapanWarScene")
        {
            if (roundNum == 1)
            {
                if (instantiatedEnemySoldiers.Length == 0)
                {
                    if (!isReadyToPlay)
                    {
                        foreach (var btn in defenderButtons)
                        {
                            btn.interactable = false;
                        }
                        Time.timeScale = 1;
                        if (!victoryAS.isPlaying)
                        {
                            roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                            victoryText.text = "Victory!";
                            victoryAS.clip = victoryWinAS;
                            victoryAS.Play();
                            victory.GetComponent<Animator>().SetTrigger("ShowVictory");
                            MapSceneScript.america = !MapSceneScript.america;
                        }
                    }
                }
            }
        }
        else if (scene.name == "AmericanWarScene")
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
                        Time.timeScale = 1;
                        if (!victoryAS.isPlaying)
                        {
                            roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                            victoryText.text = "Victory!";
                            victoryAS.clip = victoryWinAS;
                            victoryAS.Play();
                            victory.GetComponent<Animator>().SetTrigger("ShowVictory");
                            MapSceneScript.spanish = !MapSceneScript.spanish;
                        }
                    }
                }
            }
        }
        else if (scene.name == "SpanishWarScene")
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
                        Time.timeScale = 1;
                        if (!victoryAS.isPlaying)
                        {
                            roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                            victoryText.text = "Victory!";
                            victoryAS.clip = victoryWinAS;
                            victoryAS.Play();
                            victory.GetComponent<Animator>().SetTrigger("ShowVictory");
                        }
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

    void ifJapaneseScene()
    {
        if (roundNum == 1)
        {
            spawningCount(0, 2, 10);
        }
        else if (roundNum == 2)
        {
            spawningCount(1, 3, 25);
        }
        else if (roundNum == 3)
        {
            spawningCount(1, 4, 35);
        }
        else if (roundNum == 4)
        {
            spawningCount(2, 5, 50);
            SoldierScript.speed = 80f;
        }
        else if (roundNum == 5)
        {
            spawningCount(4, 6, 55);
        }
    }

    void ifSpanishScene()
    {
        if (roundNum == 1)
        {
            spawningCount(0, 1, 10);
        }
        else if (roundNum == 2)
        {
            spawningCount(0, 1, 25);
        }
        else if (roundNum == 3)
        {
            spawningCount(0, 2, 35);
        }
        else if (roundNum == 4)
        {
            spawningCount(0, 2, 50);
            SoldierScript.speed = 80f;
        }
        else if (roundNum == 5)
        {
            spawningCount(1, 3, 55);
        }
        else if (roundNum == 6)
        {
            spawningCount(1, 3, 70);
        }
        else if (roundNum == 7)
        {
            spawningCount(1, 4, 80);
        }
        else if (roundNum == 8)
        {
            SoldierScript.speed = 100f;
            spawningCount(2, 4, 85);
        }
        else if (roundNum == 9)
        {
            spawningCount(2, 4, 95);
        }
        else if (roundNum == 10)
        {
            SoldierScript.speed = 50f;
            spawningCount(4, 5, 1);
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

    public void openSettings()
    {
        if (isSettingsOpen)
        {
            settings.SetActive(false);
            isSettingsOpen = !isSettingsOpen;
        }
        else
        {
            settings.SetActive(true);
            isSettingsOpen = !isSettingsOpen;
        }
    }

    public void exitToMainmenu()
    {
        SceneManager.LoadScene(1);
    }

    public void retry()
    {
        isReadyToPlay = false;
        isSpawnAvailable = true;
        isSettingsOpen = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevel()
    {
        // if (scene.name == "AmericanWarScene")
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }
    }
}
