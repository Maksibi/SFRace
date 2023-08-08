using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class MPRaceInputController : MonoBehaviourPunCallbacks//, IDependency<CarInputControl>, IDependency<RaceStateTracker>
{
    private MPRaceStateTracker raceStateTracker;
    private MPCarInput control;
    private PhotonView view;

    private void Awake()
    {
        control = GetComponent<MPCarInput>();
        raceStateTracker = FindObjectOfType<MPRaceStateTracker>();
        view = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceFinished;

        control.enabled = false;

        base.OnEnable();
    }

    public override void OnDisable()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceFinished;

        base.OnDisable();
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (!view.IsMine) return;

        int stateIndex = (int)PhotonNetwork.CurrentRoom.CustomProperties[MPRaceStateTracker.RACE_STATE_KEY];

        if (stateIndex != 2)
            control.enabled = false;
        else
            control.enabled = true;
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
