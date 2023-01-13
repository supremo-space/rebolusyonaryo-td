using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public DeployingScript deployingScript;
    public GameObject moneyText;
    public static int[] pinoyDefendersCost;
    public static int money;

    void Start()
    {
        setinitialMoney();
    }

    void Update()
    {
        changeMoneyText();
        availablePinoyDefenders();
    }

    void setinitialMoney()
    {
        pinoyDefendersCost = new int[] { 15, 20, 30, 40, 50, 15 };
        money = 500;
    }

    //changing money text
    void changeMoneyText()
    {
        moneyText.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = money.ToString();
    }

    //availability of defenders and its cost, if money is lower than the price that defender is not available
    void availablePinoyDefenders()
    {
        var i = 0;
        foreach (var defender in deployingScript.pinoyDefendersButtons)
        {
            if (money < pinoyDefendersCost[i])
            {
                defender.interactable = false;
            }
            else
            {
                if (!SpawnEnemyScipt.isReadyToPlay)
                {
                    defender.interactable = true;
                }
            }
            i++;
        }
    }
}
