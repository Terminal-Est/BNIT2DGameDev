
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    bool gameHasEnded = false;
    public float endDelay = 2f;

    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public Transform carTransform;


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

    void LoadGameOverMenu()
    {
      if (PlayerStats.vehicleDamage >= 100)
        SceneManager.LoadScene("GameOverCarDestroyed");
      else if (PlayerStats.fuel <= 0)
        SceneManager.LoadScene("GameOverCarFuel");
      else if (PlayerStats.playerHealth <= 0)
        SceneManager.LoadScene("GameOverPlayer");
    }
}
