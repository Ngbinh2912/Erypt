using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemyMovementSpeed = 3f;
    protected Player player;
    [SerializeField] protected float maxHp = 50f;
    protected float currentHp;
    [SerializeField] private Image hpBar;

    // sat thuong
    [SerializeField] protected float stayDamage = 0.5f;
    [SerializeField] protected float enterDamage = 10f;

    // animation
    Animator animator;
    [SerializeField] protected float attackRange = 1f;

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
        moveToPlayer();
    }

    protected void moveToPlayer()
    {
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMovementSpeed * Time.deltaTime);
            flipEnemy();
            attackPlayer();
        }
    }

    protected void attackPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= attackRange)
        {
            animator.SetBool("isAttack",true);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }

    protected void flipEnemy()
    {
        if(player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x > transform.position.x ? -1 : 1, 1, 1);
        }
    }

    public virtual void takeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        updateHpBar();
        if(currentHp <= 0)
        {
            animator.SetBool("isDying", true);
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void updateHpBar()
    {
        if(hpBar != null && maxHp > 0)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

}