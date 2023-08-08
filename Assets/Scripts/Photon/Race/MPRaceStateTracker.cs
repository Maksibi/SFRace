using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public enum MPRaceState
{
    Preparation,
    Countdown,
    Race,
    Passed
}

public class MPRaceStateTracker : MonoBehaviourPunCallbacks
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

    [SerializeField] private MPRaceState state;
    public MPRaceState State => state;

    public const string RACE_STATE_KEY = "RaceState";

    private void Awake()
    {
        trackpointCircuit = FindObjectOfType<TrackpointCircuit>();
    }

    public override void OnEnable()
    {
        trackpointCircuit.TrackpointTriggered += OnTrackpointTriggered;
        trackpointCircuit.LapCompleted += OnLapCompleted;
        countdownTimer.Finished += OnCountdownTimerFinished;

        base.OnEnable();
    }

    public override void OnDisable()
    {
        trackpointCircuit.TrackpointTriggered -= OnTrackpointTriggered;
        trackpointCircuit.LapCompleted -= OnLapCompleted;
        countdownTimer.Finished -= OnCountdownTimerFinished;

        base.OnDisable();
    }

    private void Start()
    {
        StartState(MPRaceState.Preparation);

        countdownTimer.enabled = false;
    }

    #region Private API
    private void StartState(MPRaceState state)
    {
        this.state = state;
        
        Hashtable hash = new Hashtable { { RACE_STATE_KEY, (int)state } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
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
            FinishRace();

        if (trackpointCircuit.TrackType == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                FinishRace();
            else
                FinishLap(lapAmount);
        }
    }
    #endregion
    #region Public API
    public void LaunchCountdownTimer()
    {
        if (state != MPRaceState.Preparation)
            return;

        StartState(MPRaceState.Countdown);

        countdownTimer.enabled = true;

        PreparationStarted?.Invoke();
    }
    public void StartRace()
    {
        if (state != MPRaceState.Countdown)
            return;

        StartState(MPRaceState.Race);

        Started?.Invoke();
    }
    public void FinishRace()
    {
        if (state != MPRaceState.Race)
            return;

        StartState(MPRaceState.Passed);

        Completed?.Invoke();
    }
    public void FinishLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }
    #endregion
}
