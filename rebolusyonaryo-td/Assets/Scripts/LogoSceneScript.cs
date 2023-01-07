using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneScript : MonoBehaviour
{
    public GameObject lightLogo;

    void Start()
    {
        StartCoroutine(delayLogo());
    }

    void Update() { }

    IEnumerator delayLogo()
    {
        yield return new WaitForSeconds(3);
        lightLogo.GetComponent<Animator>().SetTrigger("ShowLogo");
    }
}
