using UnityEngine;
using Zenject;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private Pause pause;

    [Inject]
    public void Construct(Pause obj) => pause = obj;

    private void OnEnable()
    {
        pause.PauseStateChanged += OnPauseStateChanged;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pause.ChangePauseState();
        }
    }

    private void OnDisable()
    {
        pause.PauseStateChanged -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool isPause)
    {
        pausePanel.SetActive(isPause);
    }

    public void UnPause()
    {
        pause.UnPause();
    }
}
