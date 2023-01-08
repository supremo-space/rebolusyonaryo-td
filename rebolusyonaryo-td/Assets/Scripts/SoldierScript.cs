using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoldierScript : MonoBehaviour
{
    public static float speed = 60f;
    private PointsScript points;
    private int pointsInd;
    private Rigidbody2D rb;
    public int health;
    public int initialHealth;
    private string[] americanSoldiersName =
    {
        "AmericanPistol(Clone)",
        "AmericanRifle(Clone)",
        "AmericanSniper(Clone)",
        "AmericanMachineGun(Clone)",
        "AmericanBazooka(Clone)",
        "AmericanJeep(Clone)",
        "AmericanTank(Clone)"
    };

    private string[] japaneseSoldiersName =
    {
        "JapaneseKnife(Clone)",
        "JapaneseKatana(Clone)",
        "JapaneseRevolver(Clone)",
        "JapaneseRifleKnife(Clone)",
        "JapaneseDoubleRifle(Clone)",
        "JapaneseShotgun(Clone)"
    };

    private string[] spanishSoldiersName =
    {
        "SpanishAxe(Clone)",
        "SpanishSpear(Clone)",
        "SpanishSwordShield(Clone)",
        "SpanishBow(Clone)",
        "SpanishMagellan(Clone)",
    };
    private int[] americanSoldiersHealth = { 10, 20, 30, 70, 90, 130, 180 };
    private int[] japaneseSoldiersHealth = { 10, 20, 25, 35, 45, 50 };
    private int[] spanishSoldiersHealth = { 15, 25, 50, 150, 2000 };
    public GameObject healthBar;
    private Animator anim;
    public int defenderDamage;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        GetComponent<AudioSource>().Play();
        points = GameObject.FindGameObjectWithTag("Points").GetComponent<PointsScript>();
        rb = this.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        setEnemyStats();
    }

    void Update()
    {
        moveToPoint();
        soldierFacing();
        moveToNextPoint();
        updateHealthBar();
        soldierWillDie();
    }

    //setting enemy stats
    void setEnemyStats()
    {
        if (scene.name == "JapanWarScene")
        {
            for (var i = 0; i < japaneseSoldiersName.Length; i++)
            {
                if (this.gameObject.name == japaneseSoldiersName[i])
                {
                    this.health = japaneseSoldiersHealth[i];
                    this.initialHealth = japaneseSoldiersHealth[i];
                }
            }
        }
        else if (scene.name == "AmericanWarScene")
        {
            for (var i = 0; i < americanSoldiersName.Length; i++)
            {
                if (this.gameObject.name == americanSoldiersName[i])
                {
                    this.health = americanSoldiersHealth[i];
                    this.initialHealth = americanSoldiersHealth[i];
                }
            }
        }
        else if (scene.name == "SpanishWarScene")
        {
            for (var i = 0; i < spanishSoldiersName.Length; i++)
            {
                if (this.gameObject.name == spanishSoldiersName[i])
                {
                    this.health = spanishSoldiersHealth[i];
                    this.initialHealth = spanishSoldiersHealth[i];
                }
            }
        }
    }

    //update health every attacked
    void updateHealthBar()
    {
        healthBar.gameObject.GetComponent<Image>().fillAmount =
            (float)health / (float)initialHealth;
    }

    //enemy moves
    void moveToPoint()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            points.points[pointsInd].position,
            speed * Time.deltaTime
        );
    }

    //enemy moves to next point
    void moveToNextPoint()
    {
        if (Vector2.Distance(transform.position, points.points[pointsInd].position) < 0.1f)
        {
            if (pointsInd < points.points.Length - 1)
            {
                pointsInd++;
            }
        }
    }

    //facing to the point

    void soldierFacing()
    {
        Vector3 direction = points.points[pointsInd].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    //if enemy health is 0
    void soldierWillDie()
    {
        if (this.health <= 0)
        {
            anim.SetTrigger("Die");
            destroyEnemySoldier();
        }
    }

    //destroy enemy gameobject
    void destroyEnemySoldier()
    {
        StartCoroutine(dieCostNDestroy());
    }

    IEnumerator dieCostNDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        hasReward();
    }

    void hasReward()
    {
        bool[] rewardArr;
        if (scene.name == "JapanWarScene")
        {
            rewardArr = new[] { true, false };
        }
        else
        {
            rewardArr = new[] { true, false, false };
        }

        var randomBool = Random.Range(0, 3);
        if (rewardArr[randomBool])
        {
            MoneyScript.money += 1;
        }
    }
}
