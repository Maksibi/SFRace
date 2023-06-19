using TMPro;
using UnityEngine;
using Zenject;

public class UIRaceRecordTime : MonoBehaviour
{
    [SerializeField] private GameObject timePanel, goldPanel, playerPanel;

    [SerializeField] private TextMeshProUGUI goldRecordTime;
    [SerializeField] private TextMeshProUGUI playerRecordTime;

    private RaceStateTracker _stateTracker;
    private RaceResultTime _resultTime;

    [Inject]
    public void Construct(RaceStateTracker stateTracker, RaceResultTime resultTime)
    {
        _stateTracker = stateTracker;
        _resultTime = resultTime;
    }

    private void OnEnable()
    {
        _stateTracker.Started += OnRaceStarted;
        _stateTracker.Completed += OnRaceFinished;

        goldPanel.SetActive(false);
        playerPanel.SetActive(false);
    }

    private void OnDisable()
    {
        _stateTracker.Started -= OnRaceStarted;
        _stateTracker.Completed -= OnRaceFinished;
    }

    private void OnRaceStarted()
    {
        if (_resultTime.PlayerRecordTime > _resultTime.GoldTime || _resultTime.PlayerRecordTime == 0)
        {
            goldPanel.SetActive(true);
            goldRecordTime.text = "Gold Time: " + StringTIme.SecondToTimeString(_resultTime.GoldTime);
        }
        if (_resultTime.PlayerRecordTime != 0)
        {
            playerPanel.SetActive(true);
            playerRecordTime.text = "Player Record: " + StringTIme.SecondToTimeString(_resultTime.PlayerRecordTime);
        }
    }

    private void OnRaceFinished()
    {
        //timePanel.SetActive(false);
    }
}