using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;

public class PinoydefendersScript : MonoBehaviour
{
    public bool isShown;
    public bool isInsideTheRange;
    public GameObject[] instantiatedEnemySoldiers;
    public GameObject[] pinoySoldiers;
    private GameObject child;
    public GameObject enemySoldier;
    private int[] pinoyDefenderDamages = { 5, 10, 25, 45, 90, 5 };
    private string[] pinoyDefenderNames =
    {
        "PinoyBolo(Clone)",
        "PinoyTirador(Clone)",
        "PinoySumpit(Clone)",
        "PinoyRevolver(Clone)",
        "PinoyShotgun(Clone)",
        "PinoyBomber(Clone)"
    };
    private float[] pinoyDefenderAttackDelay = { 1f, 2f, 3f, 4f, 3f, 2f };
    private float nextAttackTime;
    private float delayTime = 0f;
    public int damage = 0;

    // private Vector3 initialBulletPos;

    public GameObject bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject bullet;

    //for revolver
    public GameObject[] bulletSpawnPointForRevolver;

    public GameObject[] bulletForRevolver;

    public Image sellImageButton,
        sellCoin;
    public Button sellButton;
    public TextMeshProUGUI sellText;

    public Image upgradeImageButton,
        upgradeCoin;
    public Button upgradeButton;
    public TextMeshProUGUI upgradeText;
    private int upgradeCost;
    public int roundCount;
    private int sellCost;
    public SpriteRenderer circle;
    public Vector3Int tilePosition;
    public Tilemap tilemap;
    public AudioClip upgrade;
    public bool scale = false;

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<CircleCollider2D>().enabled = false;
        initializeVariables();
        setDefenderStats();
        displayCost();
    }

    void Update()
    {
        sellButtonState();
        storeEnemySoldier();
        lookAtTheEnemyAndAttack();
        displayCost();
    }

    void initializeVariables()
    {
        this.child = transform.GetChild(1).gameObject;
        isShown = false;
        isInsideTheRange = false;
        roundCount = 1;
    }

    // showing the range
    void OnMouseDown()
    {
        if (isShown)
        {
            this.child.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            isShown = !isShown;
            sellImageButton.enabled = false;
            sellButton.enabled = false;
            sellText.enabled = false;
            upgradeImageButton.enabled = false;
            upgradeButton.enabled = false;
            upgradeText.enabled = false;
            sellCoin.enabled = false;
            upgradeCoin.enabled = false;
        }
        else
        {
            this.child.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.2747f);
            isShown = !isShown;
            sellImageButton.enabled = true;
            sellButton.enabled = true;
            sellText.enabled = true;
            upgradeImageButton.enabled = true;
            upgradeButton.enabled = true;
            upgradeText.enabled = true;
            sellCoin.enabled = true;
            upgradeCoin.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "EnemySoldiers")
        {
            enemySoldier = enemy.gameObject;
            isInsideTheRange = !isInsideTheRange;
        }
    }

    //collider trigger when enemy exits the range
    void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.name == "SpanishMagellan(Clone)")
        {
            enemySoldier = null;
        }
        var parentScript = transform.parent.gameObject.GetComponent<PinoydefendersScript>();
        if (enemy.gameObject.tag == "EnemySoldiers")
        {
            parentScript.isInsideTheRange = !parentScript.isInsideTheRange;
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
                upgradeCost = (int)(MoneyScript.pinoyDefendersCost[i] * 0.7);
                sellCost = MoneyScript.pinoyDefendersCost[i];
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
            gameObject.GetComponent<AudioSource>().Play();
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
            gameObject.GetComponent<AudioSource>().Play();

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
            gameObject.GetComponent<AudioSource>().Play();

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
            gameObject.GetComponent<AudioSource>().Play();

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
            gameObject.GetComponent<AudioSource>().Play();

            bullet.transform.parent = gameObject.transform;
        }
        else if (gameObject.name == "PinoyBomber(Clone)")
        {
            bullet = Instantiate(
                bulletPrefab,
                bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation
            );

            Animator anim = gameObject.GetComponent<Animator>();
            anim.SetTrigger("Throw");
            gameObject.GetComponent<AudioSource>().Play();

            bullet.transform.parent = gameObject.transform;
        }
    }

    public void decreaseHealth()
    {
        enemySoldier.gameObject.GetComponent<SoldierScript>().health -= damage;
    }

    public void sellDefender()
    {
        DeployingScript.sellPos = transform.position;
        Destroy(gameObject);
        MoneyScript.money += (sellCost / 2);
    }

    void sellButtonState()
    {
        if (SpawnEnemyScipt.isReadyToPlay)
        {
            sellButton.interactable = false;
            upgradeButton.interactable = false;
        }
        else
        {
            sellButton.interactable = true;
            if (this.upgradeCost < MoneyScript.money)
            {
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeButton.interactable = false;
            }
        }
    }

    public void upgradeDefender()
    {
        var timeScale = Time.timeScale;
        Time.timeScale = 1;
        gameObject.GetComponent<AudioSource>().PlayOneShot(upgrade, 0.7f);
        gameObject.GetComponent<Animator>().SetTrigger("Upgrade");
        this.damage *= 2;
        if (this.upgradeCost < MoneyScript.money)
        {
            MoneyScript.money -= this.upgradeCost;
            upgradeButton.gameObject.SetActive(false);
        }
        StartCoroutine(delayTimeScale(timeScale));
    }

    IEnumerator delayTimeScale(float param)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = param;
    }

    void displayCost()
    {
        upgradeText.text = "Upgrade " + upgradeCost.ToString();
        sellText.text = "Sell " + (sellCost / 2).ToString();
    }
}
