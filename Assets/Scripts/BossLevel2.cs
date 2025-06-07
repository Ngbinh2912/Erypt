using UnityEngine;

public class BossLevel2 : Enemy
{
    [SerializeField] private GameObject HandBullet;
    [SerializeField] private GameObject Laser;
    [SerializeField] private Collider2D NormalHand;

    //[SerializeField] private float speedHand = 10f;

    protected override void Start()
    {
        base.Start();
        if (NormalHand != null)
        {
            NormalHand.enabled = false; 
        }
    }

    // Normal Hand Attack
    public void EnableNormalHand()
    {
        if (NormalHand != null)
        {
            NormalHand.enabled = true;
        }
    }

    public void DisableNormalHand()
    {
        if (NormalHand != null)
        {
            NormalHand.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && NormalHand.enabled)
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.takeDamage(enterDamage);
            }
        }
    }
}
