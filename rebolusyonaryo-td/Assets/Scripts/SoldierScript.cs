using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    private float speed = 40f;
    private PointsScript points;
    private int pointsInd;
    private Rigidbody2D rb;

    void Start()
    {
        points = GameObject.FindGameObjectWithTag("Points").GetComponent<PointsScript>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveToPoint();
        soldierFacing();
        moveToNextPoint();
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
