using UnityEngine;

public class SlimeLiquit : MonoBehaviour
{
    public float attractRange = 3f; // ban kinh hut
    public float moveSpeed = 5f;    // toc do bay ve
    public float healAmount = 10f;  // luong hoi mau
    private Transform player;

    void Start()
    {
        // Tim player trong scene
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attractRange)
        {
            // Bay ve phia player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}

