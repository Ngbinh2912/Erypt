using UnityEngine;
using UnityEngine.SceneManagement;
public class gameUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void StartGame()
    {
        gameManager.StartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinuGame()
    {
        gameManager.ResumeGame(); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
