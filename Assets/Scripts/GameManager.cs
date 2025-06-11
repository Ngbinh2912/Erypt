using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseGame;
    [SerializeField] private GameObject winGame;

    public static GameManager Instance;

    public float savedHp = 150f;
    public int savedBulletCount = 1;

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

    void Start()
    {
        MainMenu();
        AudioManager.Instance.StopMusic();
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        pauseGame.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 0f;
        savedHp = 150f;
        savedBulletCount = 1;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        mainMenu.SetActive(false);
        pauseGame.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 0f;
        AudioManager.Instance.PlayGameOverMusic();
    }

    public void PauseGame()
    {
        pauseGame.SetActive(true);
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        winGame.SetActive(true);
        mainMenu.SetActive(false);
        pauseGame.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 0f;

    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        pauseGame.SetActive(false);
        gameOver.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 1f;
        AudioManager.Instance.PlayDefaultMusic();
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        pauseGame.SetActive(false);
        gameOver.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 1f;
    }

}
