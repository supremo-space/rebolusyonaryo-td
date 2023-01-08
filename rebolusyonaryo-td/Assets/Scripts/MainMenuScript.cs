using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject blackBG;

    public GameObject mainMenu;

    public GameObject btn1,
        btn2;

    void Start()
    {
        animateMainMenu();
    }

    void Update() { }

    void animateMainMenu()
    {
        blackBG.GetComponent<Animator>().SetTrigger("Fade");
        StartCoroutine(animateMainMenuBG());
    }

    IEnumerator animateMainMenuBG()
    {
        yield return new WaitForSeconds(2.5f);
        mainMenu.GetComponent<Animator>().SetTrigger("AnimateMM");
    }

    public void play()
    {
        btn1.GetComponent<AudioSource>().Play();
        mainMenu.GetComponent<Animator>().SetTrigger("Play");
        StartCoroutine(goToMapScene());
    }

    IEnumerator goToMapScene()
    {
        yield return new WaitForSeconds(3.1f);
        SceneManager.LoadScene(2);
    }

    public void exit()
    {
        btn2.GetComponent<AudioSource>().Play();
        StartCoroutine(delayQuit());
    }

    IEnumerator delayQuit()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }
}
