using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text rounds;

    void OnEnable()
    {
        rounds.text = PlayerStats.rounds.ToString();
    }

    public void Retry()
    {
        WaveSpawner.enemiesAlive = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
