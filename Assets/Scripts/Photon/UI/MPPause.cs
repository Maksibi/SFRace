using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MPPause : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChanged;

    private bool isPaused;
    public bool IsPaused => isPaused;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (isPaused) UnPause();
        else TurnPause();

        Cursor.lockState = isPaused? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void TurnPause()
    {
        if (isPaused) return;

        isPaused = true;

        PauseStateChanged?.Invoke(isPaused);
    }

    public void UnPause()
    {
        if (!isPaused) return;

        isPaused = false;

        PauseStateChanged?.Invoke(isPaused);
    }
}
