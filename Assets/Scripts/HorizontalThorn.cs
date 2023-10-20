using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class HorizontalThorn : MonoBehaviour
{
    public float speed = 3f;
    bool thornMove = true;
    float minX;
    float maxX;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        minX = transform.position.x;
        maxX = transform.position.x + 6f;
    }

    void Update()
    {
        if (transform.position.x > maxX)
        {
            thornMove = false;
        }
        else if (transform.position.x < minX)
        {
            thornMove = true;
        }


        if (thornMove)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

        }
    }

    private IEnumerator coroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Hero")
        {
            Hero.animator.SetInteger("state", 4);
            Debug.Log("Death!!!!!!!!!!!!!!");

            coroutine = ExecuteAfterTime(1.5f);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator ExecuteAfterTime(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);

        GameObject obj = GameObject.FindGameObjectWithTag("music");

        if (obj)
        {
            Destroy(obj);
        }

        SceneManager.LoadScene(0);
    }
}
