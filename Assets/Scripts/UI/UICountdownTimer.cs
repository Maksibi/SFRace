using UnityEngine;
using TMPro;

public class UICountdownTimer : MonoBehaviour
{
    [SerializeField] private RaceStateTracker raceStateTracker;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Timer timer;

    private void OnEnable()
    {
        raceStateTracker.PreparationStarted += OnRacePreparationStarted;
        raceStateTracker.Started += OnStarted;

        text.enabled = false;
    }
    private void OnDisable()
    {
        raceStateTracker.PreparationStarted -= OnRacePreparationStarted;
        raceStateTracker.Started -= OnStarted;
    }
    private void OnRacePreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }
    private void OnStarted()
    {
        text.enabled = false;
        enabled = false;
    }
    private void Update()
    {
        text.text = timer.Value.ToString("F0");

        if (text.text == "0") text.text = "Go!";
    }
}
