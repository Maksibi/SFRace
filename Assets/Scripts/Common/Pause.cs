using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChanged;

    private bool isPaused;
    public bool IsPaused => isPaused;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (isPaused) UnPause();
        else TurnPause();
    }

    public void TurnPause()
    {
        if (isPaused) return;

        Time.timeScale = 0;
        isPaused = true;

        PauseStateChanged?.Invoke(isPaused);
    }

    public void UnPause()
    {
        if (!isPaused) return;

        Time.timeScale = 1;
        isPaused = false;

        PauseStateChanged?.Invoke(isPaused);
    }
}
