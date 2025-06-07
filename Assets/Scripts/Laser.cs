using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private float damage = 10f;

    private Transform target;
    private Collider2D col;
    private BossLevel2 boss;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        boss = GetComponentInParent<BossLevel2>();
    }

    private void Update()
    {
        if (target == null) return;

        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void EnableCollider() 
    {
        if (col != null)
        {
            col.enabled = true;
        }
    }

    public void DisableCollider() 
    {
        if (col != null)
        {
            col.enabled = false;
        }
    }

    public void EndLaser()
    {
        if (boss != null)
        {
            boss.DisableLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.takeDamage(damage);
            }
        }
    }
}
