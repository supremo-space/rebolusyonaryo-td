using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int scale = 1;

    void Start()
    {
        text.text = scale.ToString() + "x";
    }

    void Update() { }

    public void speedUp()
    {
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
}
