using UnityEngine;
using Zenject;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject buttons;

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
        
        settingsPanel.SetActive(false);

        buttons.SetActive(true);
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
        buttons.SetActive(true);
        settingsPanel.SetActive(false);
        pause.UnPause();
    }
}
