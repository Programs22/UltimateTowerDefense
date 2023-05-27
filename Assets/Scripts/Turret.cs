using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform rotationPart;
    public float turretSpeed = 10f;
    public float fireRate = 1f;
    public GameObject bullet;
    public Transform firePoint;
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem effect;
    public Light light;
    public int damageOverTime = 20;
    public float slowPercentage = 0.3f;

    private Transform target;
    private float fireCountdown = 0f;
    private Enemy enemy;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    effect.Stop();
                    light.enabled = false;
                }
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletInstance = (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet bulletObject = bulletInstance.GetComponent<Bullet>();

        if (bulletObject != null)
            bulletObject.Seek(target);
    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * turretSpeed).eulerAngles;
        rotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            effect.Play();
            light.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - target.position;
        
        effect.transform.position = target.position + direction.normalized;
        effect.transform.rotation = Quaternion.LookRotation(direction);

        enemy.TakeDamage(damageOverTime * Time.deltaTime);
        enemy.Slow(slowPercentage);
    }
}
