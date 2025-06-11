using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText; 
    public string[] sentences;
    public float textSpeed = 0.05f;

    private int index = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeSentence());
        //sentences = new string[] { "Khá lắm nhóc con ! Ngươi dám đặt chân vào lăng mộ của nhà vua sao ? Hãy chờ bị nghiền nát đi" };
    }

    IEnumerator TypeSentence()
    {
        dialogueText.text = "";
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(2f);
        StartDisappearing();
    }

    void StartDisappearing()
    {
        animator.SetTrigger("Die"); 
        dialoguePanel.SetActive(false); 
    }

    public void DestroyNPC()
    {
        Destroy(gameObject);
    }
}
