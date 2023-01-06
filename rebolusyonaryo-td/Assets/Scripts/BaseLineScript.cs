using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseLineScript : MonoBehaviour
{
    public GameObject roundText;
    public Button[] defenderButtons;
    public GameObject defeatText;
    public bool ifEnemyIn = false;

    // public AudioSource defeatAS;

    void Start() { }

    void Update() { }

    void OnTriggerEnter2D(Collider2D soldier)
    {
        if (!ifEnemyIn)
        {
            gameObject.GetComponent<AudioSource>().Play();
            if (soldier.gameObject.tag == "EnemySoldiers")
            {
                foreach (var btn in defenderButtons)
                {
                    btn.interactable = false;
                }
            }
            Time.timeScale = 1;
            defeatText.GetComponent<Animator>().SetTrigger("ShowVictory");
            ifEnemyIn = !ifEnemyIn;
        }
    }
}
