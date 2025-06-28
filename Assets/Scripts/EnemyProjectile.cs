using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private GameObject MiniGolem;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); 
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
            SpawnEnemy();
            Destroy(gameObject);
        }
        else if (!other.isTrigger) 
        {
            SpawnEnemy();
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction, float speed)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * speed;
    }

    private void SpawnEnemy()
    {
        if (MiniGolem != null && Random.value <= 0.5f)
        {
            GameObject dropAttribute = Instantiate(MiniGolem, transform.position, Quaternion.identity);
        }
    }
}
