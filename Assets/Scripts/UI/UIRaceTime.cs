using TMPro;
using UnityEngine;
using Zenject;

public class UIRaceTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private RaceStateTracker _tracker;
    private RaceTImeTracker _timeTracker;

    [Inject]
    public void Construct(RaceStateTracker tracker, RaceTImeTracker timeTracker)
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
