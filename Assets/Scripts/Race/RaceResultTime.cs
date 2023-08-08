using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class RaceResultTime : MonoBehaviour
{
    public static string SaveMark = "PLAYER_BEST_TIME";

    public event UnityAction ResultUpdated;

    [SerializeField] private float goldTime;//, silverTime, bronzeTime;
    public float GoldTime => goldTime;
    //public float SilverTime => silverTime;
    //public float BronzeTime => bronzeTime;

    private float _playerRecordTime, _currentTime;
    public float PlayerRecordTime => _playerRecordTime;
    public float CurrentTime => _currentTime;

    private RaceTImeTracker timeTracker;
    private RaceStateTracker stateTracker;

    [Inject]
    public void Construct(RaceTImeTracker timeTracker, RaceStateTracker stateTracker)
    {
        this.timeTracker = timeTracker;
        this.stateTracker = stateTracker;
    }

    private void OnEnable()
    {
        stateTracker.Completed += OnRaceFinished;
    }

    private void OnDisable()
    {
        stateTracker.Completed -= OnRaceFinished;
    }

    private void Awake()
    {
        Load();
    }

    private void OnRaceFinished()
    {
        float absoluteRecord = GetAbsoluteRecord();

        _currentTime = timeTracker.CurrentTime;

        Debug.Log("Record" + absoluteRecord);
        Debug.Log("Result.Current" + CurrentTime);
        Debug.Log("Tracker.Current" + timeTracker.CurrentTime);

        if (timeTracker.CurrentTime < absoluteRecord || _playerRecordTime == 0)
        {
            _playerRecordTime = timeTracker.CurrentTime;

            Save();
        }

        ResultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if (_playerRecordTime < goldTime && _playerRecordTime != 0)
        {
            return _playerRecordTime;
        }
        else return goldTime;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, _playerRecordTime);
    }

    private void Load()
    {
        _playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }
}