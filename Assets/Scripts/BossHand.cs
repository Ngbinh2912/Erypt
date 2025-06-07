using UnityEngine;

public class BossHand : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 5f;
    public float lifeTime = 3f;

    public GameObject MiniEx;
    public GameObject Explosion;
    public Transform ExPos;

    private Vector2 moveDirection;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.takeDamage(damage);
            }
            if (MiniEx != null)
            {
                GameObject dropAttribute = Instantiate(MiniEx, ExPos.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (Explosion != null)
            {
                GameObject dropAttribute = Instantiate(Explosion, ExPos.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
