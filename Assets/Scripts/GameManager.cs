using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject lostPanel;

    public bool isWin;
    public bool isLost;

    private void Awake()
    {
        if (Instance == false)
            Instance = this;
        else
            Destroy(gameObject);

        Time.timeScale = 1;
    }


    public void WinLevel()
    {
        isWin = true;
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LostLevel()
    {
        isLost = true;
        lostPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadLevel(int level)
    {
        string levelString = "Level_" + level;
        SceneManager.LoadScene(levelString);
    }

    public void ReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif
        }
    }

    public bool GetIsPlay()
    {
        if (isWin == true || isLost == true)
            return false;
        else return true;
    }
}
