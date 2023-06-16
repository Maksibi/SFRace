using UnityEngine;

public class RaceInputController : MonoBehaviour
{
    [SerializeField] private CarInputControl control;
    [SerializeField] private RaceStateTracker raceStateTracker;

    private void OnEnable()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceFinished;

        control.enabled = false;
    }
    private void OnDisable()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceFinished;
    }
    private void OnRaceStarted()
    {
        control.enabled = true;
    }
    private void OnRaceFinished()
    {
        control.Stop();
        control.enabled = false;
    }
}
