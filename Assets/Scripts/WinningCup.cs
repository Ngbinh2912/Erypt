using UnityEngine;

public class WinningCup : MonoBehaviour
{
    public float attractRange = 3f;
    public float moveSpeed = 5f;

    private Transform player;
    private string HpbarName = "Hpbar";
    private Transform HpbarTarget;
    private Animator animator;

    private enum State { Idle, MoveToPlayer, MoveToHpbar, Arrived }
    private State currentState = State.Idle;

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        switch (currentState)
        {
            case State.Idle:
                float distance = Vector2.Distance(transform.position, player.position);
                if (distance <= attractRange)
                {
                    currentState = State.MoveToPlayer;
                }
                break;

            case State.MoveToPlayer:
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                break;

            case State.MoveToHpbar:
                if (HpbarTarget != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, HpbarTarget.position, moveSpeed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, HpbarTarget.position) < 0.05f)
                    {
                        transform.SetParent(player);
                        currentState = State.Arrived;
                        animator.SetTrigger("Yeah");
                    }
                }
                break;

            case State.Arrived:

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayWinningSound();
            Transform found = other.transform.Find(HpbarName);
            if (found != null)
            {
                found.gameObject.SetActive(false);
                HpbarTarget = found;
                currentState = State.MoveToHpbar;
            }
        }
    }

    private void nextScene()
    {
        gameUI.Instance.WinGame();
        AudioManager.Instance.StopMusic();
    }
}
