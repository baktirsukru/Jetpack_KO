using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f; // Oyun durdurulur
        mainMenuUI.SetActive(true);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f; // Oyun devam eder
        mainMenuUI.SetActive(false);
    }
}
