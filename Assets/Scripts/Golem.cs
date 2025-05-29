using UnityEngine;

public class Golem : Enemy
{
    [SerializeField] private Collider2D weaponCollider;

    protected override void Start()
    {
        base.Start();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; // TẮT collider ngay khi bắt đầu
        }
    }

    // Hàm được gọi từ Animation Event khi bắt đầu ra đòn
    public void EnableWeaponCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
    }

    // Hàm được gọi từ Animation Event khi kết thúc đòn
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
                player.takeDamage(stayDamage);
            }
        }
    }
}
