using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    private MusicManager musicManager;


    void Start()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        musicManager = MusicManager.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (isPaused && Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI?.SetActive(true);

        musicManager?.PlayMusic(musicManager.pauseMusic);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI?.SetActive(false);

        musicManager?.PlayMusic(musicManager.gameMusic);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainPrincipal");
    }

}
