using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelecter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Menu selection method, fill in the button on click with these methods
    public void GoToDriveScene()
    {
        SceneManager.LoadScene("TopDown");
    }
    // Place holder method, add you button for this scene
    public void GoToOtherScene()
    {

    }

}
