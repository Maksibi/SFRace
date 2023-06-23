using UnityEngine;
using UnityEngine.Events;
using Zenject;

public enum RaceState
{
    Preparation,
    Countdown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour
{
    #region events
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackpointPassed;
    public event UnityAction<int> LapCompleted;
    #endregion
    [SerializeField] private int lapsToComplete;
    [SerializeField] private Timer countdownTimer;

    private TrackpointCircuit trackpointCircuit;
    public Timer CountdownTimer => countdownTimer;

    private RaceState state;
    public RaceState State => state;

    private void OnEnable()
    {
        trackpointCircuit.TrackpointTriggered += OnTrackpointTriggered;
        trackpointCircuit.LapCompleted += OnLapCompleted;
        countdownTimer.Finished += OnCountdownTimerFinished;
    }
    private void OnDisable()
    {
        trackpointCircuit.TrackpointTriggered -= OnTrackpointTriggered;
        trackpointCircuit.LapCompleted -= OnLapCompleted;
        countdownTimer.Finished -= OnCountdownTimerFinished;
    }
    private void Start()
    {
        StartState(RaceState.Preparation);

        countdownTimer.enabled = false;
    }
    #region Private API
    private void StartState(RaceState state)
    {
        this.state = state;
    }
    private void OnCountdownTimerFinished()
    {
        StartRace();
    }
    private void OnTrackpointTriggered(TrackPoint trackpoint)
    {
        TrackpointPassed?.Invoke(trackpoint);
    }
    private void OnLapCompleted(int lapAmount)
    {
        if (trackpointCircuit.TrackType == TrackType.Sprint)
        {
            FinishRace();
        }
        if (trackpointCircuit.TrackType == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete) FinishRace();
            else FinishLap(lapAmount);
        }
    }
    #endregion
    #region Public API
    public void LaunchCountdownTimer()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.Countdown);

        countdownTimer.enabled = true;

        PreparationStarted?.Invoke();
    }
    public void StartRace()
    {
        if (state != RaceState.Countdown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }
    public void FinishRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }
    public void FinishLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

    [Inject]
    public void Construct(TrackpointCircuit trackpoint)
    {
        this.trackpointCircuit = trackpoint;
    }
    #endregion
}
