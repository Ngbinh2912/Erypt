using System.Runtime.CompilerServices;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPre;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedBullet = 15f;
    [SerializeField] private float speedRingBullet = 10f;
    [SerializeField] private float hpValue = 50f;
    [SerializeField] private GameObject miniEnemy;
    [SerializeField] private float skillCoolDown = 2f;
    [SerializeField] private Vector3 PortalPos;
    [SerializeField] private GameObject Portal;

    private float nextSkillTime = 0f;
    private Animator animator;

    private void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        PortalPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange * 1.5f && Time.time >= nextSkillTime && CanSeePlayer())
        {
            UseSkill();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.takeDamage(enterDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.takeDamage(stayDamage);
        }
    }

    private void MagicBullet()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPre, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedBullet);

        }
    }

    private void MagicBulletRing()
    {
        const int bulletCount = 18;
        float angleStep = 360 / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            GameObject bullet = Instantiate(bulletPre, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedRingBullet);
        }
    }

    private void Heal(float hpAmount)
    {
        currentHp = Mathf.Min(currentHp + hpAmount, maxHp);
        UpdateHpBar();
    }

    private void SummonMiniEnemy()
    {
        Heal(hpValue);
        Instantiate(miniEnemy, transform.position, Quaternion.identity);
    }

    private void ChooseSkill()
    {
        int randomSkill = Random.Range(0, 3);
        switch (randomSkill)
        {
            case 0:
                animator.SetTrigger("MagicBullet");
                break;
            case 1:
                animator.SetTrigger("MagicBulletRing");
                break;
            case 2:
                animator.SetTrigger("Summon");
                break;
        }
    }

    private void UseSkill()
    {
        nextSkillTime = Time.time + skillCoolDown;
        ChooseSkill();
    }

    protected override void Die()
    {
        if (Portal != null)
        {
            GameObject dropAttribute = Instantiate(Portal, PortalPos, Quaternion.identity);
        }
        base.Die();
    }
}
