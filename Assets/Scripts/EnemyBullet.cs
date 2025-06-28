using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 movementDirection;
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        if (movementDirection == Vector3.zero)
        {
            return;
        }

        transform.position += movementDirection * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 direction)
    {
        movementDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.takeDamage(15f);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}

