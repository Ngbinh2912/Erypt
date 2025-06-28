using UnityEngine;
using UnityEngine.SceneManagement;
public class gameUI : MonoBehaviour
{
    public static gameUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinuGame()
    {
        GameManager.Instance.ResumeGame(); 
    }

    public void WinGame()
    {
        GameManager.Instance.WinGame();
        SceneManager.LoadScene("Level_1");
    }

    public void MainMenu()
    {
        GameManager.Instance.MainMenu();
        SceneManager.LoadScene("Level_1");
    }
}
