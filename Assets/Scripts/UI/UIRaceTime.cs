using TMPro;
using UnityEngine;
using Zenject;

public class UIRaceTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private RaceStateTracker _tracker;
    private RaceTimeTracker _timeTracker;

    [Inject]
    public void Construct(RaceStateTracker tracker, RaceTimeTracker timeTracker)
    {
        _tracker = tracker;
        _timeTracker = timeTracker;
    }
    private void OnEnable()
    {
        _tracker.Started += OnRaceStarted;

        text.enabled = false;
    }
    private void OnDisable()
    {
        _tracker.Started -= OnRaceStarted;
    }
    private void Update()
    {
        text.text = StringTIme.SecondToTimeString(_timeTracker.CurrentTime);
    }
    private void OnRaceStarted()
    {
        text.enabled = true;
        enabled = true;
    }
}
