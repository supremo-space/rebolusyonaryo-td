using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSceneScript : MonoBehaviour
{
    public GameObject stageInfoBox;

    public TMP_Text countryName;
    public TMP_Text history;

    private string[] stageCountryName = { "Japan", "America", "Spain" };
    private string[] stageHistory =
    {
        "The Japanese Army first landed in the Philippines on December 8, 1941. The Japanese invasion force landed on the northeastern coast of the main Philippine island of Luzon, at the Lingayen Gulf. The invasion was part of a larger Japanese plan to occupy and control a number of territories in Southeast Asia and the Pacific, including the Philippines.",
        "American forces first landed in the Philippines during the Spanish-American War in 1898.The United States was allied with the Philippine revolutionaries, who were fighting against Spanish colonial rule. The American forces landed on the eastern coast of the main Philippine island of Luzon, near the town of Cavite.",
        "Spanish explorers first landed in the Philippines on March 17, 1521. The crew landed on the island of Homonhon, which is located in the eastern part of the Philippines. On April 27, 1521, the Battle of Mactan occurred as indigenous ruler Lapulapu resisted Spanish domination"
    };

    public GameObject blackBG;
    public GameObject stageInfo;

    void Start()
    {
        animateMainMenu();
    }

    void Update() { }

    void animateMainMenu()
    {
        blackBG.GetComponent<Animator>().SetTrigger("Fade");
    }

    void openStageInfoBox()
    {
        stageInfo.GetComponent<Animator>().SetTrigger("ShowBox");
    }

    public void cancel()
    {
        stageInfo.GetComponent<Animator>().SetTrigger("CloseBox");
    }

    private void setStageInfo(int num)
    {
        countryName.text = stageCountryName[num];
        history.text = stageHistory[num];
    }

    public void openStage1()
    {
        setStageInfo(0);
        openStageInfoBox();
    }

    public void openStage2()
    {
        setStageInfo(1);
        openStageInfoBox();
    }

    public void openStage3()
    {
        setStageInfo(2);
        openStageInfoBox();
    }

    public void playStage()
    {
        if (countryName.text == "Japan")
        {
            SceneManager.LoadScene(3);
        }
        else if (countryName.text == "America")
        {
            SceneManager.LoadScene(3);
        }
        else if (countryName.text == "Spanish")
        {
            SceneManager.LoadScene(3);
        }
    }
}
