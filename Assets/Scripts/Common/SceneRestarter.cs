using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRestarter : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.F5)) 
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
