using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private GameObject slimeLiquit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.takeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.takeDamage(stayDamage);
            }
        }
    }

    protected override void Die()
    {
        if(slimeLiquit != null)
        {
            GameObject dropAttribute = Instantiate(slimeLiquit, transform.position, Quaternion.identity);
            Destroy(dropAttribute, 5f);
        }
        base.Die();
    }
}
