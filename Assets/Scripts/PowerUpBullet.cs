using UnityEngine;

public class PowerUpBullet : MonoBehaviour
{
    public float attractRange = 3f;       // ban kinh hut
    public float moveSpeed = 5f;          // toc do bay ve player
    public int extraBulletCount = 1;      // so dan tang

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
            // bay ve phia player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LongRangeWeapon weapon = other.GetComponentInChildren<LongRangeWeapon>();
            if (weapon != null)
            {
                weapon.IncreaseBulletCount(extraBulletCount);
            }

            Destroy(gameObject); // huy vat pham sau khi dung
        }
    }
}
