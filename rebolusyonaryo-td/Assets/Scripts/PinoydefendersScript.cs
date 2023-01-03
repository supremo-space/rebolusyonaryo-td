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
    private int[] pinoyDefenderDamages = { 5, 10, 25, 45, 90 };
    private string[] pinoyDefenderNames =
    {
        "PinoyBolo(Clone)",
        "PinoyTirador(Clone)",
        "PinoySumpit(Clone)",
        "PinoyRevolver(Clone)",
        "PinoyShotgun(Clone)"
    };
    private float[] pinoyDefenderAttackDelay = { 1f, 3f, 3f, 4f, 8f };
    private float nextAttackTime;
    private float delayTime = 0f;
    private int damage = 0;

    // private Vector3 initialBulletPos;

    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject bullet;

    //for revolver
    public GameObject[] bulletSpawnPointForRevolver;

    public GameObject[] bulletForRevolver;

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
        transform.right = enemySoldier.gameObject.transform.position - transform.position;
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
        if (gameObject.name == "PinoyBolo(Clone)")
        {
            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Sway");
            decreaseHealth();
        }
        else if (gameObject.name == "PinoySumpit(Clone)")
        {
            bullet = Instantiate(
                bulletPrefab,
                bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation
            );

            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Smoke");

            bullet.transform.parent = gameObject.transform;
        }
        else if (gameObject.name == "PinoyTirador(Clone)")
        {
            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Trigger");
            bullet = Instantiate(
                bulletPrefab,
                bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation
            );

            bullet.transform.parent = gameObject.transform;
        }
        else if (gameObject.name == "PinoyRevolver(Clone)")
        {
            bulletForRevolver = new GameObject[2];
            bulletForRevolver[0] = Instantiate(
                bulletPrefab,
                bulletSpawnPointForRevolver[0].transform.position,
                bulletSpawnPointForRevolver[0].transform.rotation
            );

            bulletForRevolver[1] = Instantiate(
                bulletPrefab,
                bulletSpawnPointForRevolver[1].transform.position,
                bulletSpawnPointForRevolver[1].transform.rotation
            );

            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Shoot");

            bulletForRevolver[0].transform.parent = gameObject.transform;
            bulletForRevolver[1].transform.parent = gameObject.transform;
        }
        else if (gameObject.name == "PinoyShotgun(Clone)")
        {
            bullet = Instantiate(
                bulletPrefab,
                bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation
            );

            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Shoot");

            bullet.transform.parent = gameObject.transform;
        }
    }

    public void decreaseHealth()
    {
        enemySoldier.gameObject.GetComponent<SoldierScript>().health -= damage;
    }
}
