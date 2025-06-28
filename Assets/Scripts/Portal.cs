using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;
    private void nextLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayDefaultMusic();
            nextLevel();
        }
    }
}
