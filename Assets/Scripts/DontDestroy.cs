using UnityEngine;
using UnityEngine.SceneManagement; 

public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

