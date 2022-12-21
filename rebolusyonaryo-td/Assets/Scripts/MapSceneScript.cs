using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapSceneScript : MonoBehaviour
{
    public GameObject stageInfoBox;
    private bool isStageInfoBoxOpen = false;

    public TMP_Text countryName;
    public TMP_Text history;

    private string[] stageCountryName = { "Spain", "America", "Japan" };
    private string[] stageHistory =
    {
        "Sinakop tayo ng almost 300 years hehe.",
        "Ewan parang binili ata nila tayo sa Spain",
        "Semi chinchong wahhaaha"
    };

    void Start()
    {
        stageInfoBox.SetActive(false);
    }

    void Update() { }

    private void openStageInfoBox()
    {
        if (!isStageInfoBoxOpen)
        {
            stageInfoBox.SetActive(true);
            isStageInfoBoxOpen = true;
        }
        else
        {
            stageInfoBox.SetActive(false);
            isStageInfoBoxOpen = false;
        }
    }

    private void setStageInfo(int num)
    {
        countryName.text = stageCountryName[num];
        history.text = stageHistory[num];
    }

    public void openStage1()
    {
        setStageInfo(2);
        openStageInfoBox();
    }

    public void openStage2()
    {
        setStageInfo(1);
        openStageInfoBox();
    }

    public void openStage3()
    {
        setStageInfo(0);
        openStageInfoBox();
    }
}
