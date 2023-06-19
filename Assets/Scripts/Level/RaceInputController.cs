using UnityEngine;
using Zenject;

public class RaceInputController : MonoBehaviour//, IDependency<CarInputControl>, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    private CarInputControl control;

    [Inject]
    public void Construct (CarInputControl control, RaceStateTracker raceStateTracker)
    {
        this.control = control;
        this.raceStateTracker = raceStateTracker;
    }
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
