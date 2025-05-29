using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMovementSpeed = 3f;
    protected Player player;
    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;
    [SerializeField] private Image hpBar;
    public GameObject Hpbar;

    // sat thuong
    [SerializeField] protected float stayDamage = 0.5f;
    [SerializeField] protected float enterDamage = 10f;

    // animation
    Animator animator;
    [SerializeField] protected float attackRange = 1f;
    private float attackTimer = 0f;
    [SerializeField] private float attackCooldown = 1.5f;

    // them bien kiem soat kich hoat
    protected bool isActive = false;

    //su kien khi quai chet
    public event Action<Enemy> OnEnemyDied;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
        currentHp = maxHp;
        if (hpBar == null)
        {
            hpBar = transform.Find("Hpbar/Hp").GetComponent<Image>();
        }
        updateHpBar();
    }

    protected virtual void Update()
    {
        // chi hoat dong khi isActive = true
        if (!isActive) return;

        moveToPlayer();
    }

    protected void moveToPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMovementSpeed * Time.deltaTime);
            flipEnemy();
            attackPlayer();
        }
    }

    protected void attackPlayer()
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

    protected void flipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x > transform.position.x ? -1 : 1, 1, 1);
        }
    }

    public virtual void takeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        updateHpBar();
        if (currentHp <= 0)
        {
            Hpbar.SetActive(false);
            isActive = false;
            animator.SetTrigger("Die");
        }
    }

    protected virtual void Die()
    {
        OnEnemyDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected void updateHpBar()
    {
        if (hpBar != null && maxHp > 0)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    // goi de kich hoat quai
    public virtual void ActivateEnemy()
    {
        isActive = true;
    }
}
