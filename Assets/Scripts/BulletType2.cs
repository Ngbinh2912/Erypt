using UnityEngine;

public class BulletType2 : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 12f;
    public float lifeTime = 3f;

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
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
