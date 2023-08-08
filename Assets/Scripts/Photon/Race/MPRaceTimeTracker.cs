using UnityEngine;

public class MPRaceTimeTracker : MonoBehaviour
{
    private MPRaceStateTracker _tracker;

    private float currentTime;
    public float CurrentTime => currentTime;

    private void Awake()
    {
        _tracker = FindObjectOfType<MPRaceStateTracker>();
    }

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