using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

  public void PlayGame()
  {
    SceneManager.LoadScene("Garage");
  }

  public void Mission1()
  {
    SceneManager.LoadScene("Level1Car");
  }

  public void Mission2()
  {
    SceneManager.LoadScene("Level1Car");
  }

  public void QuitToMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void Quit()
  {
    Debug.Log("QUIT!");
    Application.Quit();
  }

}
