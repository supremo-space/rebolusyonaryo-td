using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject parent;

    void Start()
    {
        float force = 1000f;
        parent = transform.parent.gameObject;
        var parentScript = parent.gameObject.GetComponent<PinoydefendersScript>();

        if (parentScript.name == "PinoyRevolver(Clone)")
        {
            if (parentScript.bulletForRevolver[0].gameObject == this.gameObject)
            {
                Vector3 direction =
                    parentScript.enemySoldier.transform.position
                    - parentScript.bulletSpawnPointForRevolver[0].transform.position;
                parentScript.bulletForRevolver[0].gameObject.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(direction.x, direction.y).normalized * force;
            }
            else
            {
                Vector3 direction =
                    parentScript.enemySoldier.transform.position
                    - parentScript.bulletSpawnPointForRevolver[1].transform.position;
                parentScript.bulletForRevolver[1].gameObject.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(direction.x, direction.y).normalized * force;
            }
        }
        else
        {
            Vector3 direction =
                parentScript.enemySoldier.transform.position
                - parentScript.bulletSpawnPoint.transform.position;
            parentScript.bullet.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(direction.x, direction.y).normalized * force;
        }
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
