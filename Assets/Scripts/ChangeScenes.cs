using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ChangeScenes : MonoBehaviour
{
    private int sceneNumber;

    void Update()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Hero") 
        {
            GetSceneNumber();

            if(sceneNumber==0)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("music");

                if (obj)
                {
                    Destroy(obj);
                }
            }

            SceneManager.LoadScene(sceneNumber);
        }
    }

    void GetSceneNumber()
    {
        if (sceneNumber < 5) sceneNumber++;
        else
        {
            sceneNumber = 0;
            //System.Random rnd = new System.Random();
            //sceneNumber = rnd.Next(1, 4);
            //if (sceneNumber == SceneManager.GetActiveScene().buildIndex)  sceneNumber = rnd.Next(1, 4);
        }
    }
}
