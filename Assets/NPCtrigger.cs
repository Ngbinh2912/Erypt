using UnityEngine;

public class NPCtrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered NPC");

            NPCDialogue dialogue = GetComponent<NPCDialogue>();
            if (dialogue != null)
            {
                dialogue.ShowDialogue();
                hasTriggered = true;
            }
            else
            {
                Debug.LogError("Không tìm thấy NPCDialogue trên GameObject này!");
            }
        }
    }
}
