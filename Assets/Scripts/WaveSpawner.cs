using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float countdownTimer = 20f;
    public Text countdownText;
    public static int enemiesAlive = 0;
    public Wave[] waves;

    private float countdown;
    private int waveIndex = 0;
    private static int waveRepeat = 1;
    private bool waveStarted;

    void Start()
    {
        waveStarted = false;
        countdown = countdownTimer;
    }

    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }
        else if (waveStarted)
        {
            return;
        }

        if (countdown <= 0f)
        {
            if (waveIndex == waves.Length)
            {
                waveIndex = 0;
                ++waveRepeat;
            }

            waveStarted = true;
            StartCoroutine(SpawnWave());
            countdown = countdownTimer;
            return;
        }

        waveStarted = false;
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        countdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        ++PlayerStats.rounds;
        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count * waveRepeat; ++i)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        
        ++waveIndex;
        waveStarted = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        ++enemiesAlive;
    }
}
