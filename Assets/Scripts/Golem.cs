using UnityEngine;

public class Golem : Enemy
{
    [SerializeField] private Collider2D weaponCollider;
    //sat thuong
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Player player = collision.GetComponent<Player>();

    //        if(player != null)
    //        {
    //            player.takeDamage(enterDamage);
    //        }
    //    }
    //}

}
