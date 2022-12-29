using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour
{
    public DeployingScript deployingScript;
    public GameObject moneyText;
    public int[] pinoyDefendersCost;
    public int money;

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
        pinoyDefendersCost = new int[] { 10, 20, 30, 40, 50 };
        money = 100;
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
            i++;
        }
    }
}
