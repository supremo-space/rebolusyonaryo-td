using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierScript : MonoBehaviour
{
    private float speed = 50f;
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
    private int[] americanSoldiersHealth = { 10, 20, 30, 40, 50, 60, 70 };
    public GameObject healthBar;
    private Animator anim;
    public int defenderDamage;

    void Start()
    {
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
        Debug.Log(defenderDamage);
    }

    // void OnCollisionEnter(Collision bullet)
    // {
    //     if (bullet.gameObject.tag == "Bullet")
    //     {
    //         Destroy(bullet.gameObject);
    //         Debug.Log("sadasda");
    //         this.health -= defenderDamage;
    //     }
    // }


    //setting enemy stats
    void setEnemyStats()
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
        Destroy(gameObject, 0.5f);
    }
}
