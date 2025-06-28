using UnityEngine;

public class Ghost : Enemy
{
    [Header("Invisibility Settings")]
    [SerializeField] private float invisibleDuration = 2f;
    [SerializeField] private float visibleDuration = 4f;

    private bool isInvisible = false;
    private float invTimer = 0f;
    private float visTimer = 0f;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5f;

    [Header("Distance Settings")]
    [SerializeField] private float preferredDistance = 3f;

    [Header("Fire Position")]
    [SerializeField] private Transform firePos;


    private Collider2D[] allColliders;
    private SpriteRenderer[] allRenderers;

    protected override void Start()
    {
        base.Start();
        allColliders = GetComponents<Collider2D>();
        allRenderers = GetComponents<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (isDead || player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer >= visionRange && !SawPlayer)
        {
            Wander();
        }
        else
        {
            SawPlayer = true;
            if (CanSeePlayer())
            {
                MaintainDistanceAndAttack();
            }
        }

        HandleInvisibility();
    }

    private void HandleInvisibility()
    {
        if (isInvisible)
        {
            invTimer += Time.deltaTime;
            if (invTimer >= invisibleDuration)
            {
                isInvisible = false;
                invTimer = 0f;
                animator.SetTrigger("Appear");
            }
        }
        else
        {
            visTimer += Time.deltaTime;
            if (visTimer >= visibleDuration)
            {
                isInvisible = true;
                visTimer = 0f;
                animator.SetTrigger("Disappear");
            }
        }
    }

    private void Invisible()
    {
        foreach (var col in allColliders)
            col.enabled = false;
        foreach (var rend in allRenderers)
            rend.enabled = false;
        Hpbar?.SetActive(false);
    }

    private void Visible()
    {
        foreach (var col in allColliders)
            col.enabled = true;
        foreach (var rend in allRenderers)
            rend.enabled = true;
        Hpbar?.SetActive(true);
    }

    private void MaintainDistanceAndAttack()
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

        attackTimer += Time.deltaTime;

        if (!isInvisible && attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            animator.SetBool("isAttacking", true);
        }
    }

    public void PerformAttack()
    {
        if (isInvisible || isDead)
            return;

        AttackInCircle();
        animator.SetBool("isAttacking", false);
    }

    private void AttackInCircle()
    {
        int numberOfProjectiles = Random.Range(5, 9);
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 projectileDir = new Vector2(projectileDirX, projectileDirY).normalized;

            GameObject projectile = Instantiate(projectilePrefab, firePos.position, Quaternion.identity);

            EnemyProjectile ep = projectile.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                ep.SetDirection(projectileDir, projectileSpeed);
            }

            angle += angleStep;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.takeDamage(enterDamage);
            }
        }
    }

    public override void takeDamage(float damage)
    {
        if (isInvisible)
            return;
        base.takeDamage(damage);
    }
}
