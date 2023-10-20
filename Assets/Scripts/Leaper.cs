using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaper : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D body;

    private void Start()
    {
        body = player.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Hero")
        {
            player.GetComponent<Hero>().hitLeaper = true;
        }
    }
}
