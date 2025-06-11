using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Settings")]
    [TextArea]
    public string message;
    public float typingSpeed = 0.05f;
    public float displayTime = 1f;

    private Animator animator;
    private Coroutine typingCoroutine;

    void Start()
    {
        dialogueBox.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void ShowDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueBox.SetActive(true);
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        dialogueText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(displayTime);

        dialogueBox.SetActive(false);

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}