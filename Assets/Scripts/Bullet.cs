using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public GameObject bulletImpact;
    public float explosionRadius = 0f;
    public int damage = 50;

    private Transform target;

    public void Seek(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distancePerFrame)
        {
            TargetHit();
            return;
        }

        transform.Translate(direction.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    void TargetHit()
    {
        GameObject particles = (GameObject)Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(particles, 2f);

        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Enemy component = enemy.GetComponent<Enemy>();
        
        if (component != null)
            component.TakeDamage(damage);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
