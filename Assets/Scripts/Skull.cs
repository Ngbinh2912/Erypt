using UnityEngine;

public class Skull : Enemy
{
    public GameObject bulletNormalPrefab;
    public GameObject bulletStrongPrefab;
    public Transform firePos;



    [SerializeField] private float preferredDistance = 6f;
    [SerializeField] private GameObject PowerUp;

    private bool isEvolving = false;
    private bool evolved = false;

    protected override void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (SawPlayer || (distanceToPlayer <= visionRange && CanSeePlayer()))
        {
            SawPlayer = true;

            if (!isEvolving)
            {
                KeepDistanceFromPlayer();
                AttackLogic();
            }
        }
        else
        {
            Wander();
        }
    }

    void KeepDistanceFromPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > preferredDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMovementSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer < preferredDistance - 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -enemyMovementSpeed * Time.deltaTime);
        }

        FlipEnemy();
    }


    void AttackLogic()
    {
        attackTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            attackTimer = 0f;

            if (!evolved)
                FireNormal();
            else
                FireStrong();
        }
    }

    void FireNormal()
    {
        int bulletCount = 3;
        float spreadAngle = 30f;

        Vector2 directionToPlayer = (player.transform.position - firePos.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (spreadAngle / (bulletCount - 1)) * i;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletNormalPrefab, firePos.position, Quaternion.identity);
            bullet.GetComponent<BulletType1>().SetDirection(dir);
            bullet.transform.right = dir;
        }
    }


    void FireStrong()
    {
        int bulletCount = 5;
        float spreadAngle = 45f;

        Vector2 directionToPlayer = (player.transform.position - firePos.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (spreadAngle / (bulletCount - 1)) * i;
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletStrongPrefab, firePos.position, Quaternion.identity);
            bullet.GetComponent<BulletType2>().SetDirection(dir);
            bullet.transform.right = dir;
        }
    }



    public override void takeDamage(float damage)
    {
        if (isEvolving) return;

        base.takeDamage(damage);

        if (!evolved && currentHp / maxHp < 0.5f)
        {
            Evolve();
        }
    }

    void Evolve()
    {
        isEvolving = true;
        animator.SetTrigger("Glow");
    }

    public void FinishEvolve()
    {
        isEvolving = false;
        evolved = true;

        animator.SetBool("Idle2", true); 
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
