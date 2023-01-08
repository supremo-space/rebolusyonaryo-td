using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    void Start() { }

    void Update()
    {
        clickEsc();
    }

    void clickEsc()
    {
        if (
            Input.GetKeyUp(KeyCode.Escape)
            || Input.GetKeyUp(KeyCode.Space)
            || Input.GetKeyUp(KeyCode.KeypadEnter)
        )
        {
            SceneManager.LoadScene(1);
        }
    }
}
