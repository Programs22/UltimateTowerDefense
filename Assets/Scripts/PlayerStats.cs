using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public static int lives;
    public static int rounds;

    public int startMoney = 400;
    public int startLives = 20;

    void Start()
    {
        money = startMoney;
        lives = startLives;
        rounds = 0;
    }
}
