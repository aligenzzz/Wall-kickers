using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Thorns : MonoBehaviour
{
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
