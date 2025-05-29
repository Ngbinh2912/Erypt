using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected float enemyMovementSpeed = 3f;
    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;

    [Header("Damage Settings")]
    [SerializeField] protected float stayDamage = 0.5f;
    [SerializeField] protected float enterDamage = 10f;

    [Header("Attack Settings")]
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected float attackCooldown = 1.5f;
    [SerializeField] protected float visionRange = 5f; // tam nhin

    [Header("Health Bar")]
    [SerializeField] private Image hpBar;
    public GameObject Hpbar;

    protected Player player;
    private float attackTimer = 0f;
    private Animator animator;

    private bool SawPlayer = false;
    // lang thang
    private Vector2 spawnPosition;
    private Vector2 wanderTarget;
    [Header("Enemy State")]
    [SerializeField] protected float wanderRadius = 2f;
    [SerializeField] protected float wanderDelay = 2f;
    private float wanderTimer = 0f;

    public event Action<Enemy> OnEnemyDied;

    private bool isDead = false;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
        currentHp = maxHp;
        spawnPosition = transform.position;
        PickNewWanderTarget();

        if (hpBar == null && transform.Find("Hpbar/Hp") != null)
        {
            hpBar = transform.Find("Hpbar/Hp").GetComponent<Image>();
        }

        UpdateHpBar();
    }

    protected virtual void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer >= visionRange && !SawPlayer)
        {
            Wander();
        }
        else
        {
            SawPlayer = true;
            MoveToPlayer();
        }
    }

    protected void MoveToPlayer()
    {
        if(isDead) return;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMovementSpeed * Time.deltaTime);
        FlipEnemy();
        AttackPlayer();
    }

    protected void AttackPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        attackTimer += Time.deltaTime;

        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            animator.SetBool("isAttack", true);
            attackTimer = 0f;
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }

    protected void FlipEnemy()
    {
        if (player == null) return;

        float scaleX = player.transform.position.x > transform.position.x ? -1f : 1f;
        transform.localScale = new Vector3(scaleX, 1f, 1f);
    }

    protected void Wander()
    {
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= wanderDelay)
        {
            PickNewWanderTarget();
            wanderTimer = 0f;
        }

        transform.position = Vector2.MoveTowards(transform.position, wanderTarget, (enemyMovementSpeed * 0.5f) * Time.deltaTime);
    }

    private void PickNewWanderTarget()
    {
        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * wanderRadius;
        wanderTarget = spawnPosition + randomOffset;
    }

    public virtual void takeDamage(float damage)
    {
        SawPlayer = true;
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        UpdateHpBar();

        if (currentHp <= 0)
        {
            isDead = true;
            Hpbar?.SetActive(false);
            animator.SetTrigger("Die");
        }
    }

    protected virtual void Die()
    {
        OnEnemyDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null && maxHp > 0)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }
}
