using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject blackBG;

    public GameObject mainMenu;

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
        Application.Quit();
    }
}
