using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinoydefendersScript : MonoBehaviour
{
    private bool isShown;
    public GameObject[] pinoySoldiers;
    GameObject child;

    void Start()
    {
        this.child = transform.GetChild(1).gameObject;
        isShown = false;
    }

    void Update()
    {
        // pinoySoldiers = GameObject.FindGameObjectsWithTag("PinoySoldiers");
    }

    void OnMouseDown()
    {
        if (isShown)
        {
            this.child.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            isShown = !isShown;
        }
        else
        {
            // foreach (var pinoy in pinoySoldiers)
            // {
            //     if (
            //         pinoy.GetComponentInChildren<SpriteRenderer>()
            //         != this.child.GetComponentInChildren<SpriteRenderer>()
            //     )
            //     {
            //         pinoy.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            //     }
            // }
            this.child.GetComponentInChildren<SpriteRenderer>().color = new Color(
                255,
                0,
                0,
                0.2747f
            );
            isShown = !isShown;
        }
    }
}
