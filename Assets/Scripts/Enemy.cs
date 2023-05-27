using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float health;
    public int reward = 25;
    public GameObject deathEffect;
    public float startSpeed = 10f;
    public float startHealth = 150f;
    public Image healthBar;
    public int livesAffected = 1;

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        PlayerStats.money += reward;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        --WaveSpawner.enemiesAlive;
        Destroy(gameObject);
    }

    public void Slow(float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
    }
}
