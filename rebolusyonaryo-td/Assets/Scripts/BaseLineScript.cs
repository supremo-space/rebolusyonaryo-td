using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseLineScript : MonoBehaviour
{
    public GameObject roundText;
    public Button[] defenderButtons;

    void Start() { }

    void Update() { }

    void OnTriggerEnter2D(Collider2D soldier)
    {
        if (soldier.gameObject.tag == "EnemySoldiers")
        {
            foreach (var btn in defenderButtons)
            {
                btn.interactable = false;
            }
            roundText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "defeat!!!!!!";
        }
    }
}
