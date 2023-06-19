using UnityEngine;
using TMPro;
using Zenject;

public class UICountdownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private RaceStateTracker raceStateTracker;

    [Inject]
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

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
