using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameEnded;
    public GameObject gameOverUI;

    void Start()
    {
        isGameEnded = false;
    }

    void Update()
    {
        if (isGameEnded)
            return;

        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isGameEnded = true;
        gameOverUI.SetActive(true);
    }
}
