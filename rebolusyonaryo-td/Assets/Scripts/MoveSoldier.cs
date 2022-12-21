using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MoveSoldier : MonoBehaviour
{
    // public GameObject baseLine;
    // private float speed = 100f;
    public AIPath aiPath;

    Vector2 face;

    void Start() { }

    void Update()
    {
        // transform.position = Vector2.MoveTowards(
        //     transform.position,
        //     baseLine.transform.position,
        //     speed * Time.deltaTime
        // );

        face = aiPath.desiredVelocity;
        transform.right = face;
    }
}
