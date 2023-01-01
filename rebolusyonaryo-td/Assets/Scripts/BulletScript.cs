using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void Start()
    {
        float force = 1000f;
        GameObject parent = transform.parent.gameObject;
        var parentScript = parent.gameObject.GetComponent<PinoydefendersScript>();
        Vector3 direction =
            parentScript.enemySoldier.transform.position
            - parentScript.bulletSpawnPoint.transform.position;

        parentScript.bullet.gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(direction.x, direction.y).normalized * force;
    }

    void Update() { }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "EnemySoldiers")
        {
            GameObject parent = transform.parent.gameObject;
            parent.gameObject.GetComponent<PinoydefendersScript>().decreaseHealth();
            Destroy(gameObject);
        }
    }
}
