using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class DoorTrigger : MonoBehaviour
{
    public EnemySpawner enemySpawner;

    private bool triggered = false;
    private BoxCollider2D doorCollider;
    private SpriteRenderer doorRenderer;

    private Color transparentColor = new Color(1f, 1f, 1f, 0f); // cua trong suot
    private Color visibleColor = Color.white; // cua hien hinh

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorRenderer = GetComponent<SpriteRenderer>();

        // Dat cua o che do trong suot luc dau
        if (doorRenderer != null)
        {
            doorRenderer.color = transparentColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Khi nguoi choi cham vao cua lan dau
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            enemySpawner.ActivateAllEnemies();
            Debug.Log("Enemies activated!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Khi nguoi choi da di qua hoan toan
        if (triggered && other.CompareTag("Player"))
        {
            if (doorCollider != null)
                doorCollider.isTrigger = false;

            if (doorRenderer != null)
                doorRenderer.color = visibleColor;

            Debug.Log("Door closed after player passed through.");
        }
    }
}
