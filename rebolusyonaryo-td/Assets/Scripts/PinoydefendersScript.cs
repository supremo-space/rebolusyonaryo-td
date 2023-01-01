using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PinoydefendersScript : MonoBehaviour
{
    private bool isShown;
    private bool isInsideTheRange;
    public GameObject[] instantiatedEnemySoldiers;
    public GameObject[] pinoySoldiers;
    private GameObject child;
    public GameObject enemySoldier;
    private int[] pinoyDefenderDamages = { 20, 20, 30, 50, 100 };
    private string[] pinoyDefenderNames =
    {
        "PinoyBolo(Clone)",
        "PinoyTirador(Clone)",
        "PinoySumpit(Clone)",
        "PinoyRevolver(Clone)",
        "PinoyShotgun(Clone)"
    };
    private float[] pinoyDefenderAttackDelay = { 3f, 1f, 2f, 4f, 8f };
    private float nextAttackTime;
    private float delayTime = 0f;
    private int damage = 0;

    // private Vector3 initialBulletPos;

    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject bullet;

    // public bool isReadyToDecrease = false;

    void Start()
    {
        initializeVariables();
        setDefenderStats();
    }

    void Update()
    {
        storeEnemySoldier();
        lookAtTheEnemyAndAttack();
    }

    void initializeVariables()
    {
        this.child = transform.GetChild(1).gameObject;
        isShown = false;
        isInsideTheRange = false;
    }

    //showing the range
    void OnMouseDown()
    {
        if (isShown)
        {
            this.child.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            isShown = !isShown;
        }
        else
        {
            this.child.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.2747f);
            isShown = !isShown;
        }
    }

    //collider trigger when enemy enters the range
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "EnemySoldiers")
        {
            this.enemySoldier = enemy.gameObject;
            isInsideTheRange = !isInsideTheRange;
        }
    }

    //collider trigger when enemy exits the range
    void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "EnemySoldiers")
        {
            isInsideTheRange = !isInsideTheRange;
        }
    }

    //setting delaytime and attack damage
    void setDefenderStats()
    {
        for (var i = 0; i < pinoyDefenderNames.Length; i++)
        {
            if (gameObject.name == pinoyDefenderNames[i])
            {
                delayTime = pinoyDefenderAttackDelay[i];
                damage = pinoyDefenderDamages[i];
            }
        }
    }

    //looking at the close enemy that is inside the range
    void lookAtTheEnemyAndAttack()
    {
        // if (isInsideTheRange)
        // {
        //     transform.right = enemySoldier.transform.position - transform.position;
        //     AttackEnemy();
        // }
        // else
        // {
        //     return;
        // }

        transform.right = enemySoldier.transform.position - transform.position;
        AttackEnemy();
    }

    //attacking the enemy
    void AttackEnemy()
    {
        if (Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + delayTime;
            fireBullet();
        }
    }

    //store in an array
    void storeEnemySoldier()
    {
        instantiatedEnemySoldiers = GameObject.FindGameObjectsWithTag("EnemySoldiers");
    }

    void fireBullet()
    {
        if (gameObject.name == "PinoySumpit(Clone)")
        {
            bullet = Instantiate(
                bulletPrefab,
                bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation
            );

            bullet.transform.parent = gameObject.transform;
        }
    }

    public void decreaseHealth()
    {
        enemySoldier.gameObject.GetComponent<SoldierScript>().health -= damage;
    }
}
