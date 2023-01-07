using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSceneScript : MonoBehaviour
{
    public GameObject lightLogo,
        RTD;

    void Start()
    {
        StartCoroutine(delayLogo());
    }

    void Update() { }

    IEnumerator delayLogo()
    {
        yield return new WaitForSeconds(3);
        lightLogo.GetComponent<Animator>().SetTrigger("ShowLogo");
        StartCoroutine(delayMainMenuScene());
    }

    // IEnumerator showRTD()
    // {
    //     yield return new WaitForSeconds(5.3f);
    //     RTD.GetComponent<Animator>().SetTrigger("ShowRTD");
    //     StartCoroutine(delayMainMenuScene());
    // }

    IEnumerator delayMainMenuScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}
