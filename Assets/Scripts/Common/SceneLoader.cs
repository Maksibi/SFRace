using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneLoader : MonoBehaviour
{
    private const string MainMenuSceneName = "MainMenu";

    public void LoadMainMenu()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
