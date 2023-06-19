using UnityEngine;
using Zenject;

public class RaceTImeTracker : MonoBehaviour
{
    private RaceStateTracker _tracker;
    [Inject]
    public void Construct(RaceStateTracker tracker) => _tracker = tracker;

    private float currentTime;
    public float CurrentTime => currentTime;

    private void OnEnable()
    {
        _tracker.Started += OnRaceStarted;
        _tracker.Completed += OnRaceFinished;
    }
    private void OnDisable()
    {
        _tracker.Started += OnRaceStarted;
        _tracker.Completed += OnRaceFinished;
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
    }
    private void OnRaceStarted()
    {
        enabled = true;
        currentTime = 0;
    }
    private void OnRaceFinished()
    {
        enabled = false;
    }
}