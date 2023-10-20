using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] private AudioSource sound;

    public void Pause()
    {
        sound.Play();

        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        sound.Play();

        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        sound.Play();

        Application.Quit();
    }
}
