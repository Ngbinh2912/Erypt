using UnityEngine;

public class Potion : MonoBehaviour
{
    public float attractRange = 5f; 
    public float moveSpeed = 5f;   
    public float healAmount = 90f;  
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
                playerScript.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
