using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI panelBtnText;
    public int scale = 1;

    public bool isPanelOpen = true;

    public Button panelBtn;

    public GameObject panel;

    public AudioSource clickAS;

    void Start()
    {
        text.text = scale.ToString() + "x";
    }

    void Update() { }

    public void speedUp()
    {
        clickAS.Play();
        if (scale == 1)
        {
            Time.timeScale = 2;
            scale = 2;
            text.text = scale.ToString() + "x";
        }
        else if (scale == 2)
        {
            Time.timeScale = 4;
            scale = 4;
            text.text = scale.ToString() + "x";
        }
        else if (scale == 4)
        {
            Time.timeScale = 8;
            scale = 8;
            text.text = scale.ToString() + "x";
        }
        else if (scale == 8)
        {
            Time.timeScale = 1;
            scale = 1;
            text.text = scale.ToString() + "x";
        }
    }

    public void panelFucntion()
    {
        clickAS.Play();
        if (isPanelOpen)
        {
            panel.GetComponent<Animator>().SetTrigger("Close");
            isPanelOpen = !isPanelOpen;
            panelBtnText.text = "Open";
        }
        else
        {
            panel.GetComponent<Animator>().SetTrigger("Open");
            isPanelOpen = !isPanelOpen;
            panelBtnText.text = "Close";
        }
    }

    public void exit()
    {
        SceneManager.LoadScene(1);
    }
}
