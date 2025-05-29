using UnityEngine;

public class LifeTimeDestroyer : MonoBehaviour
{

    public float lifeTime = 5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] GameObject bloodAnimation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.takeDamage(damage);
                GameObject blood = Instantiate(bloodAnimation, transform.position, Quaternion.identity);
                Destroy(blood, 1f);
            }

            Destroy(gameObject);
        }
    }
}
