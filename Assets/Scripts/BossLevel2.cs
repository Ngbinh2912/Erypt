using UnityEngine;
using System.Collections;

public class BossLevel2 : Enemy
{
    [SerializeField] private GameObject HandBullet;
    [SerializeField] private Transform FirePos;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserSpawnPoint;
    private GameObject currentLaser;
    private bool isFiringLaser = false;

    [SerializeField] private Collider2D NormalHand;
    [SerializeField] private Transform FirePos2;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 5f;

    private Coroutine healingCoroutine;
    private bool isHealing = false;
    [SerializeField] private float healAmount = 20f;
    [SerializeField] private float duration = 2f;

    private bool isInvulnerable = false;

    protected override void Start()
    {
        base.Start();
        if (NormalHand != null)
        {
            NormalHand.enabled = false; 
        }
    }

    protected override void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (SawPlayer || (distanceToPlayer <= visionRange && CanSeePlayer()))
        {
            SawPlayer = true;
            FlipEnemy();

            if(!isFiringLaser && !isHealing)
            {
                MoveToPlayer();
            }

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;
                ChooseSkill();
            }
        }
    }


    // Normal Hand Attack
    private void NormalAttack()
    {
        animator.SetTrigger("NormalAttack");
    }

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

    private void AttackInCircle()
    {
        int numberOfProjectiles = Random.Range(6, 11);
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 projectileDir = new Vector2(projectileDirX, projectileDirY).normalized;

            GameObject projectile = Instantiate(projectilePrefab, FirePos2.position, Quaternion.identity);

            EnemyProjectile ep = projectile.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                ep.SetDirection(projectileDir, projectileSpeed);
            }

            angle += angleStep;
        }
    }

    //Ban tay
    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void ShootAtPlayer()
    {
        if (HandBullet == null || FirePos == null || player == null) return;

        Vector2 direction = (player.transform.position - FirePos.position).normalized;

        GameObject bullet = Instantiate(HandBullet, FirePos.position, Quaternion.identity);

        BossHand bulletScript = bullet.GetComponent<BossHand>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //Ban Laser
    private void Laser()
    {
        animator.SetTrigger("Laser");
    }

    public void FireLaser()
    {
        if (laserPrefab == null || laserSpawnPoint == null || player == null || isFiringLaser) return;

        isFiringLaser = true;

        currentLaser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity, transform);

        Vector2 direction = player.transform.position - currentLaser.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentLaser.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void DisableLaser()
    {
        if (currentLaser != null)
        {
            Destroy(currentLaser);
            currentLaser = null;
        }

        isFiringLaser = false;
    }

    //Hoi mau
    private void Heal()
    {
        animator.SetTrigger("Heal");
    }

    public void StartHealing()
    {
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
        }
        healingCoroutine = StartCoroutine(HealOverTime(healAmount, duration));
    }

    private IEnumerator HealOverTime(float totalHeal, float duration)
    {
        isHealing = true;

        float healed = 0f;
        float rate = totalHeal / duration;

        while (healed < totalHeal && currentHp < maxHp)
        {
            float delta = rate * Time.deltaTime;
            currentHp = Mathf.Min(currentHp + delta, maxHp);
            healed += delta;

            UpdateHpBar();
            yield return null;
        }

        isHealing = false; 
        healingCoroutine = null;
    }

    //khang sat thuong
    private void Immune()
    {
        animator.SetTrigger("Immune");
        StartCoroutine(TempInvulnerability(2.3f));
    }

    public void EnableInvulnerability() => isInvulnerable = true;
    public void DisableInvulnerability() => isInvulnerable = false;

    private IEnumerator TempInvulnerability(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }

    public override void takeDamage(float damage)
    {
        if (isInvulnerable || isDead) return;

        base.takeDamage(damage);
    }

    private void ChooseSkill()
    {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill)
        {
            case 0:
                NormalAttack();
                break;
            case 1:
                Attack();
                break;
            case 2:
                Laser();
                break;
            case 3:
                Heal();
                break;
            case 4:
                Immune();
                break;

        }
    }

    protected override void Die()
    {
        foreach (var col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }

        this.enabled = false;
    }
}
