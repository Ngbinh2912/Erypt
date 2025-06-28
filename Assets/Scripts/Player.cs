using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private float dashSpeed = 20f;
    private float originalSpeed;
    private float speedBoostAmount = 0f;
    private Coroutine speedBoostCoroutine;

    private TrailRenderer trail;

    private float dashDuration = 0.2f;
    private bool isDashing = false;
    public float dashCooldown; // Cooldown 
    private float lastDashTime = -Mathf.Infinity;


    public GameObject dashEffectPrefab;
    public float dashDelaySeconds;
    private Coroutine dashEffectCoroutine;

    [SerializeField] protected float maxHp = 150f;
    protected float currentHp;
    [SerializeField] private Image hpBar;

    private Rigidbody2D rb;

    public SpriteRenderer characterSR;
    Animator animator;
    public Vector3 moveInput;

    private bool isInvincible = false;

    //thong tin layer
    private int playerLayer = 7;
    private int enemyLayer = 8;

    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.enabled = false;

        currentHp = maxHp;

        if (hpBar == null)
        {
            hpBar = transform.Find("Hpbar/Hp").GetComponent<Image>();
        }

        currentHp = GameManager.Instance.savedHp;
        updateHpBar();
    }

    private void Update()
    {
        // Di chuyen
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput * speed * Time.deltaTime;

        // Set speed cho animator
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            Dash();
        }

        // Quay dau
        if (moveInput.x != 0)
        {
            characterSR.flipX = moveInput.x < 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }
    }

    private void Dash()
    {
        isDashing = true;
        isInvincible = true;

        animator.SetTrigger("Dash");

        rb.linearVelocity = moveInput * dashSpeed;

        //bo qua va cham giua Player va Enemy
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        Invoke(nameof(EndDash), dashDuration);

        StartDashEffect();

        lastDashTime = Time.time;
    }

    private void EndDash()
    {
        isDashing = false;
        isInvincible = false;

        rb.linearVelocity = Vector2.zero;

        //bat lai va cham giua Player va Enemy
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);

        StopDashEffect();
    }

    public void ApplySpeedBoost(float addedSpeed, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
            speed -= speedBoostAmount; 
        }
        AudioManager.Instance.PlayBuffSound();

        speedBoostAmount = addedSpeed;
        speed += speedBoostAmount;
        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(duration));
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        if (trail != null) trail.enabled = true;

        yield return new WaitForSeconds(duration);

        speed -= speedBoostAmount;
        speedBoostAmount = 0f;

        if (trail != null) trail.enabled = false;

        speedBoostCoroutine = null;
    }

    public void takeDamage(float damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        updateHpBar();

        GameManager.Instance.savedHp = currentHp;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        updateHpBar();

        GameManager.Instance.savedHp = currentHp;
    }

    private void Die()
    {
        AudioManager.Instance.PlayDefeatSound();
        GameManager.Instance.GameOver();
    }

    protected private void updateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    void StopDashEffect()
    {
        if (dashEffectCoroutine != null)
            StopCoroutine(dashEffectCoroutine);
    }

    void StartDashEffect()
    {
        if (dashEffectCoroutine != null)
            StopCoroutine(dashEffectCoroutine);

        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
    }

    IEnumerator DashEffectCoroutine()
    {
        while (true)
        {
            GameObject ghost = Instantiate(dashEffectPrefab, transform.position, transform.rotation);
            Sprite currentSprite = characterSR.sprite;
            ghost.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite;

            Destroy(ghost, 0.5f);
            yield return new WaitForSeconds(dashDelaySeconds);
        }
    }
}
