using UnityEngine;
using TMPro;

public class MPUICountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private RaceStateTracker raceStateTracker;

    private void Awake()
    {
        raceStateTracker = FindObjectOfType<RaceStateTracker>();
    }

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
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0");

        if (text.text == "0") text.text = "Go!";
    }
}
