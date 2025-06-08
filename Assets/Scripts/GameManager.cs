using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseGame;
    [SerializeField] private GameObject winGame;
 
    void Start()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        pauseGame.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        mainMenu.SetActive(false);
        pauseGame.SetActive(false);
        winGame.SetActive(false);

        Time.timeScale = 0f;
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
