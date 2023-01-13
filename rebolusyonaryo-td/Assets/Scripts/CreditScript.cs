using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    public GameObject blackBG;

    void Start()
    {
        animateBlackBG();
    }

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
            blackBG.GetComponent<Animator>().SetTrigger("FadeOut");
            StartCoroutine(delayBackToMap());
        }
    }

    void animateBlackBG()
    {
        blackBG.GetComponent<Animator>().SetTrigger("Fade");
    }

    IEnumerator delayBackToMap()
    {
        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene(1);
    }
}
