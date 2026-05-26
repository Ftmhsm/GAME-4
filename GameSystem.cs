using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSystem : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("UI")]
    public GameObject pausePanel;
    public TextMeshProUGUI scoreText;

    [Header("Score")]
    public int score = 0;

    bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f;

        // LOAD POSISI PLAYER
        float x = PlayerPrefs.GetFloat("PlayerX", player.position.x);
        float y = PlayerPrefs.GetFloat("PlayerY", player.position.y);

        player.position = new Vector2(x, y);

        // LOAD SCORE
        score = PlayerPrefs.GetInt("Score", 0);

        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // TAMBAH SCORE
    public void AddScore(int amount)
    {
        score += amount;

        UpdateUI();
    }

    // UPDATE UI SCORE
    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
    }

    // SAVE GAME
    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);

        PlayerPrefs.SetInt("Score", score);

        PlayerPrefs.Save();

        Debug.Log("GAME SAVED");
    }

    // PAUSE GAME
    public void PauseGame()
    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;
    }

    // RESUME GAME
    public void ResumeGame()
    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;
    }

    // RESTART GAME
    public void RestartGame()
    {
        Time.timeScale = 1f;

        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("Score");

        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}