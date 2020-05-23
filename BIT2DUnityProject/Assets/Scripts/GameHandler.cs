
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool gameHasEnded = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverCarDestroyedUI;
    public GameObject gameOverCarNoFuelUI;
    public GameObject GameOverPlayerDeadUI;
    public GameObject missionCompleteUI;
    public float endDelay = 1f;
    private float timer = 3f;

    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public Transform carTransform;

    void Update(){
      Scene currentScene = SceneManager.GetActiveScene();
      string sceneName = currentScene.name;
      if (sceneName == "Transition")
        TransitionTimer();
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        if (GameIsPaused)
          ResumeGame();
        else
          PauseGame();
      }
    }

    void TransitionTimer()
    {
      timer -= Time.deltaTime;
      if (timer <= 0f)
        SceneManager.LoadScene("Level1Player");
    }

    public void ResumeGame()
    {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
    }

    public void PauseGame()
    {
      pauseMenuUI.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level1Player")
          cameraFollow.Setup(() => playerTransform.position);
        else if (sceneName == "Level1Car")
          cameraFollow.Setup(() => carTransform.position);

    }

    public void GameOver()
    {
      if (gameHasEnded == false)
      {
        gameHasEnded = true;
        Invoke("LoadGameOverMenu", endDelay);
        // Use invoke to bring up game over menu.
      }
    }

    public void LoadMissionCompleteMenu()
    {
      PlayerStats.cash += 10000;
      missionCompleteUI.SetActive(true);
      Time.timeScale = 0f;
    }

    void LoadGameOverMenu()
    {
      if (PlayerStats.vehicleDamage >= 100)
      {
        gameOverCarDestroyedUI.SetActive(true);
        Time.timeScale = 0f;
        gameHasEnded = true;
      }
      else if (PlayerStats.fuel <= 0)
      {
        gameOverCarNoFuelUI.SetActive(true);
        Time.timeScale = 0f;
        gameHasEnded = true;
      }
      else if (PlayerStats.playerHealth <= 0)
      {
        GameOverPlayerDeadUI.SetActive(true);
        Time.timeScale = 0f;
        gameHasEnded = true;
      }
    }
}
