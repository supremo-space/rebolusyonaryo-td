using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    private float speed = 30f;
    private PointsScript points;
    private int pointsInd;
    private Rigidbody2D rb;
    public int health;
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

    void Start()
    {
        points = GameObject.FindGameObjectWithTag("Points").GetComponent<PointsScript>();
        rb = this.GetComponent<Rigidbody2D>();
        setEnemyStats();
    }

    void Update()
    {
        moveToPoint();
        soldierFacing();
        moveToNextPoint();
        // if (Time.frameCount % 1080 == 0)
        // {
        //     this.health -= 10;
        // }
        // textContainer.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = health.ToString();

        if (this.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void setEnemyStats()
    {
        for (var i = 0; i < americanSoldiersName.Length; i++)
        {
            if (this.gameObject.name == americanSoldiersName[i])
            {
                this.health = americanSoldiersHealth[i];
            }
        }
        GameObject canvas = GameObject.Find("Canvas").gameObject;
        GameObject textContainer = canvas.transform.Find("Text (TMP)").gameObject;
        textContainer.GetComponent<TMPro.TextMeshProUGUI>().text = health.ToString();
    }

    void moveToPoint()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            points.points[pointsInd].position,
            speed * Time.deltaTime
        );
    }

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

    void soldierFacing()
    {
        Vector3 direction = points.points[pointsInd].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
