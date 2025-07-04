﻿using UnityEngine;

public class Golem : Enemy
{
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private GameObject PowerUp;

    protected override void Start()
    {
        base.Start();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; // TẮT collider ngay khi bắt đầu
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
                player.takeDamage(stayDamage);
            }
        }
    }

    protected override void Die()
    {
        if (PowerUp != null && Random.value <= 0.15f)
        {
            GameObject dropAttribute = Instantiate(PowerUp, transform.position, Quaternion.identity);
            Destroy(dropAttribute, 7f);
        }
        base.Die();
    }
}
