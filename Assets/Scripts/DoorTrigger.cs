using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public Collider2D exitDoorCollider; 
    public SpriteRenderer exitDoorRenderer; 

    private SpriteRenderer spriteRenderer; 
    private Collider2D doorCollider;
    private bool triggered = false;
    private bool playerHasPassed = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();

        spriteRenderer.enabled = false;
        doorCollider.isTrigger = true;

        if (exitDoorCollider != null)
            exitDoorCollider.isTrigger = true;

        if (exitDoorRenderer != null)
            exitDoorRenderer.enabled = false;


        if (enemySpawner != null)
            enemySpawner.onAllEnemiesDefeated += UnlockDoors;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            playerHasPassed = true;


            if (enemySpawner != null)
                enemySpawner.ActivateAllEnemies();


            doorCollider.isTrigger = false;
            spriteRenderer.enabled = true;


            if (exitDoorCollider != null)
                exitDoorCollider.isTrigger = false;

            if (exitDoorRenderer != null)
                exitDoorRenderer.enabled = true;
        }
    }

    public void UnlockDoors()
    {
        if (playerHasPassed)
        {
            doorCollider.isTrigger = true;
            spriteRenderer.enabled = false;

            if (exitDoorCollider != null)
                exitDoorCollider.isTrigger = true;

            if (exitDoorRenderer != null)
                exitDoorRenderer.enabled = false;
        }
    }
}
