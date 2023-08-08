using TMPro;
using UnityEngine;

public class MPUIRaceTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private RaceStateTracker _tracker;
    private RaceTImeTracker _timeTracker;

    private void Awake()
    {
        _tracker = FindObjectOfType<RaceStateTracker>();
        _timeTracker = FindObjectOfType<RaceTImeTracker>();
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
