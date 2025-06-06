using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float attractRange = 3f;       // ban kinh hut
    public float moveSpeed = 5f;          // toc do bay ve player
    public float speedMultiplier = 2f;    // toc do tang len bao nhieu lan
    public float duration = 5f;           // thoi gian duy tri tang toc

    private Transform player;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attractRange)
        {
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
                playerScript.ApplySpeedBoost(speedMultiplier, duration);
            }

            Destroy(gameObject);
        }
    }
}
