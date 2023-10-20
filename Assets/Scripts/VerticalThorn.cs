using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class VerticalThorn : MonoBehaviour
{
    public float speed = 3f;
    bool thornMove = true;
    float minY;
    float maxY;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        minY = transform.position.y - 6f;
        maxY = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y > maxY)
        {
            thornMove = false;
        }
        else if (transform.position.y < minY)
        {
            thornMove = true;
        }


        if (thornMove)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

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
