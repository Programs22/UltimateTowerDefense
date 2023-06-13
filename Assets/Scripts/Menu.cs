using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audio;
    public GameObject ui;

    private static bool audioIsPlaying = false;

    void Awake()
    {
        if (audioIsPlaying)
            return;

        audio.Play();
        audioIsPlaying = true;    
    }

    public void Play()
    {
        WaveSpawner.enemiesAlive = 0;
        SceneManager.LoadScene("Main Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Help()
    {
        ui.SetActive(true);
    }
}