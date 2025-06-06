using UnityEngine;

public class Slime2 : Enemy
{
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private GameObject PowerUp;

    protected override void Start()
    {
        base.Start();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
    }

    public void DisableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && weaponCollider.enabled)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
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
            if (player != null)
            {
                player.takeDamage(stayDamage);
            }
        }
    }

    protected override void Die()
    {
        if (PowerUp != null && Random.value <= 0.08f)
        {
            GameObject dropAttribute = Instantiate(PowerUp, transform.position, Quaternion.identity);
            Destroy(dropAttribute, 7f);
        }
        base.Die();
    }
}
