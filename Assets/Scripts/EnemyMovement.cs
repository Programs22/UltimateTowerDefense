using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoint.waypoints[0];
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoint.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        ++waypointIndex;
        target = Waypoint.waypoints[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.lives -= enemy.livesAffected;
        --WaveSpawner.enemiesAlive;
        Destroy(gameObject);
    }
}
