using UnityEngine;

public class Ghost : Enemy
{
    [Header("Invisibility Settings")]
    [SerializeField] private float invisibleDuration = 2f;  // Thoi gian ghost an
    [SerializeField] private float visibleDuration = 4f;    // Thoi gian ghost hien thi

    private bool isInvisible = false;   // Trang thai ghost an
    private float invTimer = 0f;
    private float visTimer = 0f;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;   // Prefab dan
    [SerializeField] private float projectileSpeed = 5f;      // Toc do dan

    [Header("Distance Settings")]
    [SerializeField] private float preferredDistance = 3f;    // Khoang cach uu tien giua ghost va player

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
            MaintainDistanceAndAttack();
        }

        HandleInvisibility();
    }

    private void HandleInvisibility()
    {
        if (isInvisible)
        {
            // Ghost dang an
            invTimer += Time.deltaTime;
            if (invTimer >= invisibleDuration)
            {
                // Ket thuc thoi gian an -> ghost hien thi
                isInvisible = false;
                invTimer = 0f;
                // Khi ghost hien thi, se chay hoat an Appear roi chuyen sang Idle
                animator.SetTrigger("Appear");
            }
        }
        else
        {
            // Ghost dang hien thi (dang Idle)
            visTimer += Time.deltaTime;
            // Khi het thoi gian hien thi va cooldown, ghost chuyen sang trang thai an
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
            AttackInCircle();
        }
    }

    private void AttackInCircle()
    {
        int numberOfProjectiles = Random.Range(5, 11); 
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 projectileDir = new Vector2(projectileDirX, projectileDirY).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = projectileDir * projectileSpeed;

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
